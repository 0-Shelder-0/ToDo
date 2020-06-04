using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ToDo.Interfaces;

namespace ToDo.Services
{
    public class ImageKeeperService : IImageKeeper
    {
        public List<string> ImagePaths { get; }
        public List<string> ThumbnailImagePaths { get; }

        public ImageKeeperService()
        {
            const string path = "wwwroot\\images";
            var pathLength = path.Split('\\').FirstOrDefault().Length + 1;
            var files = Directory.GetFiles(path);

            ImagePaths = GetImagePaths(files, pathLength, str => !str.Contains("min"));
            ThumbnailImagePaths = GetImagePaths(files, pathLength, str => str.Contains("min"));
        }

        private List<string> GetImagePaths(IEnumerable<string> files, int pathLength,
                                           Func<string, bool> predicate)
        {
            var imagePaths = new List<string>();
            imagePaths.AddRange(files.Select(filePath => filePath.Substring(pathLength, filePath.Length - pathLength))
                                     .Where(filePath => Regex.IsMatch(filePath, @"^.+\.(jpg|png)$"))
                                     .Where(predicate)
                                     .Select(filePath => filePath.Replace('\\', '/')));
            return imagePaths;
        }
    }
}