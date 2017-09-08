using System;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing;

namespace Kalendar.Utility.Common
{
    /// <summary>
    /// 图片处理公共类
    /// </summary>
    public class ImageParser
    {	
		private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Property

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>The file path.</value>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the file path save to.
        /// </summary>
        /// <value>
        /// The file path save to.
        /// </value>
        public string FilePathSaveTo { get; set; }

        /// <summary>
        /// Gets or sets the width of the thumbnail.
        /// </summary>
        /// <value>The width of the thumbnail.</value>
        public int ThumbnailWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the thumbnail.
        /// </summary>
        /// <value>The height of the thumbnail.</value>
        public int ThumbnailHeight { get; set; }

        private int _jpegQuality = 80;
        /// <summary>
        /// Gets or sets the JPEG quality.
        /// </summary>
        /// <value>
        /// The JPEG quality.
        /// </value>
        public int JpegQuality
        {
            get { return _jpegQuality; }
            set { _jpegQuality = value; }
        }

        /// <summary>
        /// Gets or sets the cropper left.
        /// </summary>
        /// <value>
        /// The cropper left.
        /// </value>
        public int CropperLeft { get; set; }

        /// <summary>
        /// Gets or sets the cropper top.
        /// </summary>
        /// <value>
        /// The cropper top.
        /// </value>
        public int CropperTop { get; set; }

        /// <summary>
        /// Gets or sets the width of the cropper.
        /// </summary>
        /// <value>
        /// The width of the cropper.
        /// </value>
        public int CropperWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the cropper.
        /// </summary>
        /// <value>
        /// The height of the cropper.
        /// </value>
        public int CropperHeight { get; set; }

        #endregion

        #region 构造函数及未使用函数

        /// <summary>
        /// 图像处理
        /// </summary>
        /// <param name="imageFile"></param>
        public ImageParser(string imageFile)
        {
            CropperLeft = 0;
            FilePath = imageFile;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageParser"/> class.
        /// </summary>
        /// <param name="imageFile">The image file.</param>
        /// <param name="savePath">The save path.</param>
        /// <param name="thumbnailWidth">Width of the thumbnail.</param>
        /// <param name="thumbnailHeight">Height of the thumbnail.</param>
        public ImageParser(string imageFile,string savePath,int thumbnailWidth,int thumbnailHeight)
        {
            CropperLeft = 0;
            FilePath = imageFile;
            FilePathSaveTo = savePath;
            ThumbnailWidth = thumbnailWidth;
            ThumbnailHeight = thumbnailHeight;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageParser"/> class.
        /// </summary>
        /// <param name="imageFile">The image file.</param>
        /// <param name="savePath">The save path.</param>
        /// <param name="thumbnailWidth">Width of the thumbnail.</param>
        /// <param name="thumbnailHeight">Height of the thumbnail.</param>
        /// <param name="cropperLeft">The cropper left.</param>
        /// <param name="cropperTop">The cropper top.</param>
        /// <param name="cropperWidth">Width of the cropper.</param>
        /// <param name="cropperHeight">Height of the cropper.</param>
        public ImageParser(
            string imageFile, 
            string savePath, 
            int thumbnailWidth, 
            int thumbnailHeight,
            int cropperLeft,
            int cropperTop,
            int cropperWidth,
            int cropperHeight)
        {
            CropperLeft = 0;
            FilePath = imageFile;
            FilePathSaveTo = savePath;
            ThumbnailWidth = thumbnailWidth;
            ThumbnailHeight = thumbnailHeight;
            CropperLeft = cropperLeft;
            CropperTop = cropperTop;
            CropperWidth = cropperWidth;
            CropperHeight = cropperHeight;
        }

        /// <summary>
        /// Gets or sets the image stream.
        /// </summary>
        /// <value>The image stream.</value>
        public Stream ImageStream { get; set; }

        /// <summary>
        /// Drawings this instance.
        /// </summary>
        public void Drawing()
        {

            var outStream = new MemoryStream();
            Image oImage = System.Drawing.Image.FromFile(FilePath, true);
            //源图
            //画板
            var oGraphics = System.Drawing.Graphics.FromImage(oImage);
            oImage.Save(outStream, System.Drawing.Imaging.ImageFormat.Jpeg);

            oImage.Dispose();
            oGraphics.Dispose();
        }

        //public void DrawingText(MemoryStream ms) {
        //    System.Drawing.Image oImage = System.Drawing.Image.FromStream(ms);

        //}

        #endregion

        #region CreateThumbnail

        /// <summary>
        /// Thumbnails the callback.
        /// </summary>
        /// <returns></returns>
        public bool ThumbnailCallback()
        {
            return false;
        }

        /// <summary>
        /// Thumbnails this instance.
        /// 无参数，则缩略图为指定原图等比尺寸
        /// </summary>
        /// <returns></returns>
        public bool CreateThumbnail()
        {
            return CreateThumbnail(true);
        }

        /// <summary>
        /// Creates the thumbnail.
        /// </summary>
        /// <param name="maintainAspect">if set to <c>true</c> [maintain aspect].指定画布尺寸</param>
        /// <returns></returns>
        public bool CreateThumbnail(bool maintainAspect)
        {
			var fromImage = System.Drawing.Image.FromFile(FilePath);

            /*clone image*/

            var originImage = new Bitmap(fromImage.Width, fromImage.Height);
            using (Graphics gClone = Graphics.FromImage(originImage))
            {
                gClone.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                gClone.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                gClone.CompositingQuality = CompositingQuality.HighQuality;
                gClone.FillRectangle(Brushes.White, 0, 0, originImage.Width, originImage.Height);
                gClone.DrawImage(
                    fromImage,
                    0,
                    0,
                    originImage.Width,
                    originImage.Height
                    );
            }

            /*clone end*/

            Bitmap ret;
            bool result;
            try
            {
                int widthOutput,heightOutput;
                int widthOrigin, heightOrigin;

                if ((CropperWidth == 0) && (CropperHeight == 0))
                {
                    widthOrigin = originImage.Width;
                    heightOrigin = originImage.Height;
                }else
                {
                    widthOrigin = CropperWidth;
                    heightOrigin = CropperHeight;
                }

                decimal rate = (decimal)widthOrigin / heightOrigin;

                if ((decimal)ThumbnailWidth / ThumbnailHeight > rate)
                {
                    heightOutput = ThumbnailHeight;
                    widthOutput = (int)(widthOrigin * ((decimal)ThumbnailHeight / heightOrigin));
                }
                else
                {
                    widthOutput = ThumbnailWidth;
                    heightOutput = (int)(heightOrigin * ((decimal)ThumbnailWidth / widthOrigin));
                }

				decimal scale = (decimal)widthOutput / widthOrigin;
                //decimal scale = (decimal)ThumbnailWidth / widthOrigin;

                var widthOutputTmp = (int)(originImage.Width * scale);
                var heightOutputTmp = (int)(originImage.Height * scale);

                var myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                var thumbImage = originImage.GetThumbnailImage(widthOutputTmp, heightOutputTmp, myCallback, IntPtr.Zero);

                ret = maintainAspect ? new Bitmap(widthOutput, heightOutput) : new Bitmap(ThumbnailWidth, ThumbnailHeight);

                int xOutput = -(int) (CropperLeft*scale);
                int yOutput = -(int) (CropperTop*scale);
                
                xOutput = xOutput == 0 ? (ret.Width - widthOutput)/2 : xOutput;
                yOutput = yOutput == 0 ? (ret.Height - heightOutput)/2 : yOutput;

                using (Graphics g = Graphics.FromImage(ret))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.FillRectangle(Brushes.White, 0, 0, ret.Width, ret.Height);
                    g.DrawImage(
                        thumbImage,
                        xOutput,
                        yOutput,
                        widthOutputTmp,
                        heightOutputTmp);
                }

                var encoderParams = new System.Drawing.Imaging.EncoderParameters();
                var quality = new long[1];
                quality[0] = JpegQuality;
                var encoderParam = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality,
                                                                               quality);
                encoderParams.Param[0] = encoderParam;

                var arrayICI =System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
                System.Drawing.Imaging.ImageCodecInfo jpegICI = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICI = arrayICI[x];
                        break;
                    }
                }

                ret.Save(FilePathSaveTo, jpegICI, encoderParams);
                thumbImage.Dispose();
                result = true;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                result = false;
            }finally
            {
                originImage.Dispose();
            }

            return result;
        }

        #endregion
    }
}