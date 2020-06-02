using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ToDo.Interfaces;

namespace ToDo.Services
{
    public class ImageService : IImages
    {
        public Dictionary<int, string> ImagesPaths { get; }

        public ImageService()
        {
            const string path = "wwwroot\\images";

            var pathLength = path.Split('\\').FirstOrDefault().Length + 1;
            var images = Directory.GetFiles(path);
            ImagesPaths = images.Select(filePath => filePath.Substring(pathLength, filePath.Length - pathLength))
                                .Where(filePath => Regex.IsMatch(filePath, @"^.+\.(jpg)|(png)$"))
                                .Select(filePath => filePath.Replace('\\', '/'))
                                .Select((filePath, number) => (filePath, number + 1))
                                .ToDictionary(key => key.Item2, value => value.filePath);
            ImagesPaths[0] = string.Empty;
        }
    }
}