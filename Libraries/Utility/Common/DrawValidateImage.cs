using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace Kalendar.Utility.Common
{
	/// <summary>
    /// 绘制校验码
    /// </summary>
    public class DrawValidateImage
    {
		private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 无参构造
        /// </summary>
        public DrawValidateImage() { }
        /// <summary>
        /// 带有生成字符个数的构造
        /// </summary>
        /// <param name="charNum">验证码中包含随机字符的个数</param>
        public DrawValidateImage(int charNum)
        {
            CharNum = charNum;
        }
        /// <summary>
        /// 带有验证码图片宽度和高度的构造
        /// </summary>
        /// <param name="width">验证码图片宽度</param>
        /// <param name="height">验证码图片高度</param>
        public DrawValidateImage(int width, int height)
        {
            _width = width;
            _height = height;
        }
        /// <summary>
        /// 带有生成字符个数，验证码图片宽度和高度的构造
        /// </summary>
        /// <param name="charNum">验证码中包含随机字符的个数</param>
        /// <param name="width">验证码图片宽度</param>
        /// <param name="height">验证码图片高度</param>
        public DrawValidateImage(int charNum, int width, int height)
        {
            CharNum = charNum;
            _width = width;
            _height = height;
        }

        /// <summary>
        /// 验证码中字符个数
        /// </summary>
        int _charNum = 5; //默认字符个数为5
		/// <summary>
        /// Gets or sets the char num.
        /// </summary>
        /// <value>
        /// The char num.
        /// </value>
        public int CharNum
        {
            get { return _charNum; }
            set { _charNum = value; }
        }
        /// <summary>
        /// 字号
        /// </summary>
        int _fontSize = 20;
		/// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        /// <value>
        /// The size of the font.
        /// </value>
        public int FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; }
        }
        /// <summary>
        /// 图片宽度
        /// </summary>
        int _width = 200;
		/// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// 图片高度
        /// </summary>
        int _height = 45;
		/// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// 随机生成的字符串
        /// </summary>
        //string validStr = "";

        public string ValidStr
        {
            get { return HttpContext.Current.Session["ValidStr"].ToString(); }
            set { HttpContext.Current.Session["ValidStr"] = value; }
        }

        /// <summary>
        /// 产生指定个数的随机字符串，默认字符个数为5
        /// </summary>
        void GetValidateCode()
        {
            ValidStr = "";
            var rd = new Random(); //创建随机数对象

            //产生由 charNum 个字母或数字组成的一个字符串
            string str = "ABCDEFGHJKLMNPQRSTUVWYZ23456789";
            for (int i = 0; i < _charNum; i++)
            {
                ValidStr = ValidStr + str.Substring(rd.Next(str.Length), 1);
            }

        }

        /// <summary>
        /// 由随机字符串，随即颜色背景，和随机线条产生的Image
        /// </summary>
        /// <returns>Image</returns>
        public Image GetImgWithValidateCode()//返回 Image
        {
            //产生随机字符串
            GetValidateCode();

            //声明一个位图对象
            //声明一个绘图画面
            //创建内存流
            var memStream = new MemoryStream();

            var random = new Random();

            //由给定的需要生成字符串中字符个数 CharNum， 图片宽度 Width 和高度 Height 确定字号 FontSize，
            //确保不因字号过大而不能全部显示在图片上
            var fontWidth = (int)Math.Round(_width*1.0 / (_charNum + 2) / 1.2);
            var fontHeight = (int)Math.Round(_height / 1.2);
            //字号取二者中小者，以确保所有字符能够显示，并且字符的下半部分也能显示
            _fontSize = fontWidth <= fontHeight ? fontWidth : fontHeight;

            //创建位图对象
            var bitMap = new Bitmap(_width + FontSize, _height);
            //根据上面创建的位图对象创建绘图图面
            Graphics gph = Graphics.FromImage(bitMap);

            //设定验证码图片背景色
            gph.Clear(GetControllableColor(200));
            //产生随机干扰线条
            for (int i = 0; i < 10; i++)
            {
                var backPen = new Pen(GetControllableColor(100), 2);
                //线条起点
                int x = random.Next(_width);
                int y = random.Next(_height);
                //线条终点
                int x2 = random.Next(_width);
                int y2 = random.Next(_height);
                //划线
                gph.DrawLine(backPen, x, y, x2, y2);
            }

            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(_width);
                int y = random.Next(_height);
                var backPen = new Pen(GetControllableColor(100), 1);

                gph.DrawPie(backPen, x, y, 1, 1, 360, 360);

            }

            //定义一个含10种字体的数组
            String[] fontFamily ={ "Arial", "Verdana", "Comic Sans MS", "Impact", "Haettenschweiler",
                                   "Lucida Sans Unicode", "Garamond", "Courier New", "Book Antiqua", "Arial Narrow" };

            var sb = new SolidBrush(GetControllableColor(0));
            //通过循环,绘制每个字符,
            for (int i = 0; i < ValidStr.Length; i++)
            {
                var textFont = new Font(fontFamily[random.Next(10)], _fontSize, FontStyle.Bold);//字体随机,字号大小30,加粗

                //每次循环绘制一个字符,设置字体格式,画笔颜色,字符相对画布的X坐标,字符相对画布的Y坐标
                var space = (int)Math.Round((double)((_width - _fontSize*1.0 * (CharNum + 2)) / (CharNum+1)));
                //纵坐标
                var y = (int)Math.Round((double)((_height - _fontSize) / 3));
                gph.DrawString(ValidStr.Substring(i, 1), textFont, sb, _fontSize + i * (_fontSize + space), y);
            }
            //扭曲图片
            bitMap = TwistImage(bitMap, true, random.Next(1), random.Next(1));

            try
            {
                bitMap.Save(memStream, ImageFormat.Gif);
            }
            catch (Exception)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            //gph.Dispose();
            bitMap.Dispose();

            Image img = Image.FromStream(memStream);
            gph.DrawImage(img, 50, 20, _width, 10);

            return img;
        }
        /// <summary>
        /// 产生一种 R,G,B 均大于 colorBase 随机颜色，以确保颜色不会过深
        /// </summary>
        /// <returns>背景色</returns>
        static Color GetControllableColor(int colorBase)
        {
            if (colorBase > 200)
            {
                //System.Windows.Forms.MessageBox.Show("可控制颜色参数大于200，颜色默认位黑色");
            }
            var random = new Random();
            //确保 R,G,B 均大于 colorBase，这样才能保证背景色较浅
            Color color = Color.FromArgb(random.Next(56) + colorBase, random.Next(56) + colorBase, random.Next(56) + colorBase);
            return color;
        }

        /// <summary>
        /// 扭曲图片
        /// </summary>
        /// <param name="srcBmp"></param>
        /// <param name="bXDir"></param>
        /// <param name="dMultValue"></param>
        /// <param name="dPhase"></param>
        /// <returns></returns>
        static Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            int leftMargin = 0;
            int rightMargin = 0;
            int topMargin = 0;
            int bottomMargin = 0;
            //float PI = 3.14159265358979f;
            float PI2 = 6.28318530717959f;
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            double dBaseAxisLen = bXDir ? Convert.ToDouble(destBmp.Height) : Convert.ToDouble(destBmp.Width);
            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = bXDir ? PI2 * Convert.ToDouble(j) / dBaseAxisLen : PI2 * Convert.ToDouble(i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    //取得当前点的颜色
                    int nOldX = bXDir ? i + Convert.ToInt32(dy * dMultValue) : i;
                    int nOldY = bXDir ? j : j + Convert.ToInt32(dy * dMultValue);
                    var color = srcBmp.GetPixel(i, j);
                    if (nOldX >= leftMargin && nOldX < destBmp.Width - rightMargin && nOldY >= bottomMargin && nOldY < destBmp.Height - topMargin)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            return destBmp;
        }

        /// <summary>
        /// 判断验证码是否正确
        /// </summary>
        /// <param name="inputValCode">待判断的验证码</param>
        /// <returns>正确返回 true,错误返回 false</returns>
        public bool IsRight(string inputValCode)
        {
            if (ValidStr.ToUpper().Equals(inputValCode.ToUpper()))//无论输入大小写都转换为大些判断
            {
                return true;
            }
            return false;
        }
    }
}
