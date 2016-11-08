using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using BLO.Objects;
using DBO.Data.Managers;
using BLO.Objects.Enums;

namespace BLO.Managers
{
    public static class ImageManager
    {
        private static Dictionary<string, string> _verifiedImages = new Dictionary<string, string>();
        private static readonly Object lockimage = new Object();
        public static string ToImageUrl(this string path, bool verifyFileExists)
        {
            if (string.IsNullOrWhiteSpace(path))
                return GetNoImage().ToImageUrl();

            if (path.Contains(" "))
                path = path.Replace(" ", "%20");
            path = PathManager.UrlCombine(AppSettings.ImageUrl, path).FromRoot();

            if (verifyFileExists)
            {
                if (_verifiedImages.ContainsKey(path))
                    return _verifiedImages[path];

                var oriFile = PathManager.PathCombine(path).FromRoot().Replace("%20", " ");
                string actualPath;
                if (File.Exists(oriFile))
                    actualPath = path;
                else
                    actualPath = PathManager.UrlCombine(AppSettings.ImageUrl, GetNoImage()).FromRoot();
                lock (lockimage)
                {
                    if (!_verifiedImages.ContainsKey(path))
                        _verifiedImages.Add(path, actualPath);
                }
                return _verifiedImages[path];
            }

            return path;
        }
        public static string GetNoImage()
        {
            return "no_image.jpg";
        }
        public static string ToImageUrl(this string path)
        {
            return path.ToImageUrl(true);
        }
        private static Dictionary<string, string> _cachedImages = new Dictionary<string, string>();
        public static void ResetImages()
        {
            _cachedImages = new Dictionary<string, string>();
        }
        public static string ToImageUrl(this string path, int width, int height)
        {
            if (string.IsNullOrWhiteSpace(path))
                return GetNoImage().ToImageUrl(width, height);

            var extension = new FileInfo(path).Extension.ToLower();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                case ".bmp":
                case ".tif":
                case ".tiff":
                case ".png":
                    break;
                default:
                    return path.ToImageUrl();
            }

            var index = path.LastIndexOf(".");
            var newpath = "{0}-{2}x{3}.{1}".FormatWith(path.Substring(0, index), path.Substring(index + 1), width, height);
            var pathKey = newpath;
            if (!_cachedImages.ContainsKey(pathKey))
            {
                newpath = PathManager.UrlCombine("cache", newpath);
                newpath = newpath.ToImageUrl(false);
                var actualFile = new FileInfo(PathManager.PathCombine(newpath.Replace("%20", " ")).FromRoot());
                if (!actualFile.Directory.Exists)
                    actualFile.Directory.Create();
                if (!actualFile.Exists)
                {
                    var oriFile = PathManager.PathCombine(path.ToImageUrl()).FromRoot().Replace("%20", " ");
                    if (File.Exists(oriFile))
                    {
                        var image = ResizeImage(Image.FromFile(oriFile), width, height);
                        image.Save(actualFile.FullName);
                    }
                    else
                        newpath = GetNoImage().ToImageUrl(width, height);
                }
                if (!_cachedImages.ContainsKey(pathKey))
                    _cachedImages.Add(pathKey, newpath);
            }

            return _cachedImages[pathKey];
        }
        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighSpeed;
                graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);

                    var ratioX = (double)destImage.Width / image.Width;
                    var ratioY = (double)destImage.Height / image.Height;
                    var ratio = ratioX; //Math.Min(ratioX, ratioY);
                    var newWidth = (int)(image.Width * ratio);
                    var newHeight = (int)(image.Height * ratio);
                    var x = (destImage.Width - newWidth) / 2;
                    var y = (destImage.Height - newHeight) / 2;

                    var destRect = new Rectangle(x, y, newWidth, newHeight);
                    //graphics.DrawImage(image, x, y, newWidth, newHeight);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static KeyValuePair<string, FileInfo> BuildImageInfo(ImageFolder folder, string subFolder, string oriFileName, bool isTemp)
        {
            oriFileName = oriFileName.Split('/').Last();
            var index = oriFileName.LastIndexOf(".");
            var fileName = oriFileName.Substring(0, index);
            fileName = HttpUtility.UrlDecode(fileName);
            var dateParts = DateTime.Now.ToString(isTemp ? "yyyy/MM/dd" : "yyyy/MM");
            if (!string.IsNullOrWhiteSpace(subFolder))
                dateParts = PathManager.UrlCombine(subFolder, dateParts);
            var extension = oriFileName.Substring(index + 1);
            FileInfo fileInfo;
            int count = 0;
            string filePath;
            do
            {
                string extra = null;
                if (count > 0)
                    extra = "({0})".FormatWith(count);
                if (isTemp)
                    filePath = "temp/{0}/{1}{3}.{2}".FormatWith(dateParts, fileName, extension, extra);
                else
                    filePath = "data/{4}/{0}/{1}{3}.{2}".FormatWith(dateParts, fileName, extension, extra, folder);
                fileInfo = new FileInfo(PathManager.PathCombine(filePath.ToImageUrl(false)).FromRoot());
                count++;
            } while (fileInfo.Exists);

            return new KeyValuePair<string, FileInfo>(filePath, fileInfo);
        }
    }
}
