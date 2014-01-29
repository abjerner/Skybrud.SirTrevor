using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace Sniper.Umbraco.SirTrevor
{
    public class SirTrevorController : Controller
    {
        public ActionResult UploadImage()
        {
            string url = string.Empty;
            //are we hosted?
            if (ApplicationContext.Current != null)
            {
                //are we logged into umbraco
                if (umbraco.BusinessLogic.User.GetCurrent() != null)
                {
                    //get media service
                    var mediaService = ApplicationContext.Current.Services.MediaService;

                    string fileName = Request.Form["attachment[name]"];
                    HttpPostedFileBase file = Request.Files["attachment[file]"];

                    //get "Content Images" folder
                    IMedia parentFolder = mediaService.GetRootMedia().Where(child => child.Name == "Content Images").FirstOrDefault();

                    //if folder doesn't exist
                    if (parentFolder == null)
                    {
                        //create folder
                        parentFolder = mediaService.CreateMedia("Content Images", -1, "Folder");
                        mediaService.Save(parentFolder);
                    }

                    //create item
                    string mediaName = Path.GetFileNameWithoutExtension(fileName);
                    var newFile = mediaService.CreateMedia(mediaName, parentFolder.Id, "Image");
                    //save to generate id
                    mediaService.Save(newFile);

                    //physically save new file
                    string virtualPath = string.Format("/media/{0}/{1}", newFile.Id, fileName);
                    string physicalPath = Server.MapPath("~" + virtualPath);
                    //get folder
                    string folder = Path.GetDirectoryName(physicalPath);
                    //and detail
                    DirectoryInfo folderInfo = new DirectoryInfo(folder);
                    //check exists
                    if (!folderInfo.Exists)
                    {
                        //create
                        folderInfo.Create();
                    }
                    //save file to media folder
                    file.SaveAs(physicalPath);

                    //we also need to generate filename_thumb.jpg for umbraco to show a preview
                    using (Bitmap image = new Bitmap(physicalPath))
                    {
                        int width = 100;
                        decimal ratio = (decimal)width / (decimal)image.Width;
                        int height = (int)Math.Ceiling(image.Height * ratio);
                        using (Bitmap thumbnail = new Bitmap(width, height))
                        {
                            using (Graphics g = Graphics.FromImage(thumbnail))
                            {
                                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                //todo: this could be wrong, thumbnail was not scaled correctly
                                g.DrawImage(image, new RectangleF(PointF.Empty, new SizeF(width,height)));
                            }
                            string thumbnailPath = Path.Combine(folder, string.Format("{0}_thumb.jpg", Path.GetFileNameWithoutExtension(physicalPath)));
                            SaveWithQuality(thumbnail, thumbnailPath, 80L);
                        }
                    }

                    //set file property
                    newFile.SetValue("umbracoFile", virtualPath);

                    //update media item (now with path to file)
                    mediaService.Save(newFile);

                    url = string.Format("{{ \"file\": {{ \"url\": \"{0}\" }} }}", virtualPath);
                }
            }

            return Content(url);
        }

        private void SaveWithQuality(Bitmap bitmap, string thumbnailPath, long jpgQuality)
        {
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, jpgQuality);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bitmap.Save(thumbnailPath, jpgEncoder, myEncoderParameters);
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}