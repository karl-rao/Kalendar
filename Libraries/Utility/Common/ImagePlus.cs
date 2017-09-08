using System;
using System.IO;
using System.Web;

namespace Kalendar.Utility.Common
{
    /// <summary>
    /// 图片处理类，调用ImageParser
    /// </summary>
    public class ImagePlus
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets the thumbnail.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public static string GetThumbnail(object image, object width, object height)
        {
	    try{
            string imagePath = HttpContext.Current.Server.MapPath(image.ToString());
            if (File.Exists(imagePath))
            {
                var imageThumbnail = image.ToString().Replace(Path.GetExtension(imagePath)+"", string.Format("_thumb_{0}_{1}.jpg", width, height));
                string imageThumbnailPath = HttpContext.Current.Server.MapPath(imageThumbnail);
                if (!File.Exists(imageThumbnail))
                {
                    GetThumbnail(imagePath, Convert.ToInt32(width), Convert.ToInt32(height), imageThumbnailPath);
                }
                return imageThumbnail;
            }}catch(Exception){}
            return "/files/skin/images/nophoto.jpg";
        }

        /// <summary>
        /// Gets the thumbnail.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="imageWidth">Width of the image.</param>
        /// <param name="imageHeight">Height of the image.</param>
        /// <param name="fileSavePath">The file save path.</param>
        public static void GetThumbnail(string filePath, int imageWidth, int imageHeight, string fileSavePath)
        {
            try
            {
                (new ImageParser(filePath, fileSavePath, imageWidth, imageHeight)).CreateThumbnail();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}
