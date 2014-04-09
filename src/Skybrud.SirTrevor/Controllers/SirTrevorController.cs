using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace Skybrud.SirTrevor.Controllers {
    
    public class SirTrevorController : Controller {

        protected IMediaService MediaService {
            get { return ApplicationContext.Current.Services.MediaService; }
        }

        /// <summary>
        /// Ensures that we have a media folder, and that we use the same folder even if the user
        /// decides to rename it. 
        /// </summary>
        protected IMedia EnsureMediaFolder() {

            // Define the GUID for our folder
            Guid guid = Guid.Parse("904ec7da-7532-4f0f-9486-605402b2fc41");
            
            // Search for our special folder
            IMedia folder = MediaService.GetRootMedia().FirstOrDefault(x => x.Key == guid);

            // Create the folder if it doesn't exist
            if (folder == null) { 
                folder = MediaService.CreateMedia("SirTrevor Uploaded Images", -1, "Folder");
                if (folder == null) throw new Exception("WTF?");
                folder.Key = Guid.Parse("904ec7da-7532-4f0f-9486-605402b2fc41");
                MediaService.Save(folder);
            }
            
            return folder;

        }

        public ActionResult UploadImage() {

            // Are we hosted?
            if (ApplicationContext.Current == null) return Content("");

            // Are we logged into Umbraco?
            if (umbraco.BusinessLogic.User.GetCurrent() == null) return Content("");
            
            // Grab the filename and a reference to the uploaded file
            string fileName = Request.Form["attachment[name]"];
            HttpPostedFileBase file = Request.Files["attachment[file]"];

            // Get or create the media folder
            IMedia parentFolder = EnsureMediaFolder();

            // Create a media item for the file
            string mediaName = Path.GetFileNameWithoutExtension(fileName);
            IMedia newFile = MediaService.CreateMedia(mediaName, parentFolder.Id, "Image");
            MediaService.Save(newFile);

            // Set file property
            newFile.SetValue("umbracoFile", file);
            MediaService.Save(newFile);

            // Get the media URL
            string mediaUrl = newFile.GetValue<string>("umbracoFile");

            // Return the media URL
            return Content(String.Format("{{ \"file\": {{ \"url\": \"{0}\" }} }}", mediaUrl), "application/json");

        }

    }

}