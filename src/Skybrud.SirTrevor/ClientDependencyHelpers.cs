using System.IO;
using System.Web;
using ClientDependency.Core.Controls;

namespace Skybrud.SirTrevor {
    
    internal static class ClientDependencyHelpers {

        public static void AddJavaScript(string url) {
            var abstractContext = new HttpContextWrapper(HttpContext.Current);
            var instance = ClientDependencyLoader.GetInstance(abstractContext);
            instance.RegisterDependency(100, url, ClientDependency.Core.ClientDependencyType.Javascript);
        }
        
        public static void AddJavascriptFolder(string folderPath) {
            var httpContext = HttpContext.Current;
            var systemRootPath = httpContext.Server.MapPath("~/");
            var folderMappedPath = httpContext.Server.MapPath(folderPath);
            var abstractContext = new HttpContextWrapper(HttpContext.Current);
            var instance = ClientDependencyLoader.GetInstance(abstractContext);

            if (folderMappedPath.StartsWith(systemRootPath)) {
                var files = Directory.GetFiles(folderMappedPath, "*.js", SearchOption.TopDirectoryOnly);
                foreach (var file in files) {
                    var absoluteFilePath = "~/" + file.Substring(systemRootPath.Length).Replace("\\", "/");
                    instance.RegisterDependency(100, absoluteFilePath, ClientDependency.Core.ClientDependencyType.Javascript);
                }
            }
        }

        public static void AddCssFolder(string folderPath) {
            var httpContext = HttpContext.Current;
            var systemRootPath = httpContext.Server.MapPath("~/");
            var folderMappedPath = httpContext.Server.MapPath(folderPath);
            var abstractContext = new HttpContextWrapper(HttpContext.Current);
            var instance = ClientDependencyLoader.GetInstance(abstractContext);
            if (folderMappedPath.StartsWith(systemRootPath)) {
                var files = Directory.GetFiles(folderMappedPath, "*.css", SearchOption.TopDirectoryOnly);
                foreach (var file in files) {
                    var absoluteFilePath = "~/" + file.Substring(systemRootPath.Length).Replace("\\", "/");
                    instance.RegisterDependency(100, absoluteFilePath, ClientDependency.Core.ClientDependencyType.Css);
                }
            }
        }

        public static void AddStyleSheet(string url) {
            var abstractContext = new HttpContextWrapper(HttpContext.Current);
            var instance = ClientDependencyLoader.GetInstance(abstractContext);
            instance.RegisterDependency(100, url, ClientDependency.Core.ClientDependencyType.Css);
        }
        
    }

}