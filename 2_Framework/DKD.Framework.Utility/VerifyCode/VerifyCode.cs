using System;
using System.Drawing;

namespace DKD.Framework.Utility.VerifyCode
{
    /// <summary>
    /// 验证码核心类
    /// </summary>
    public class VerifyCode
    {

        private Color backgroundColor = Color.White;
        private Color chaosColor = Color.LightGray;
        private string codeSerial = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,j,k,m,n,p,q,r,s,t,u,v,w,x,y";
        private Color[] colors = new Color[] { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
        private bool enableChaos = false;
        private bool enableTwistImage = false;
        private string[] fonts = new string[] { "Arial", "Georgia" };
        private int fontSize = 40;
        private int length = 6;
        private int padding = 4;
        private const double PI = 3.1415926535897931;
        private const double PI2 = 6.2831853071795862;
        private TwistImageDegree twistDegree = TwistImageDegree.Normal;

        public Bitmap CreateImageCode(out string outCode)
        {
            int num6;
            int[] code = this.CreateVerifyCode();
            string outNum = "";
            Random fuhao = new Random();
            int i = fuhao.Next(2);
            if (i == 0)
            {
                outCode = (code[0] + code[1]).ToString();
                outNum = code[0].ToString() + "+" + code[1].ToString() + "=?";
            }
            else
            {
                outCode = (code[0] - code[1]).ToString();
                outNum = code[0].ToString() + "-" + code[1].ToString() + "=?";
            }
            
            int fontSize = this.FontSize;
            int num2 = fontSize + (this.Padding / 4);
            int width = ((outNum.Length * num2) + 4) + (this.Padding * 2);
            int height = (fontSize * 2) + this.Padding;
            Bitmap image = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.Clear(this.BackgroundColor);
            Random random = new Random();
            if (this.EnableChaos)
            {
                Pen pen = new Pen(this.ChaosColor, 0f);
                int num5 = this.Length * 10;
                for (num6 = 0; num6 < num5; num6++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    graphics.DrawRectangle(pen, x, y, 1, 1);
                }
            }
            int num9 = 0;
            int num10 = 0;
            int num11 = 1;
            int num12 = 1;
            int num13 = (height - this.FontSize) - (this.Padding * 2);
            int num14 = num13 / 4;
            num11 = num14;
            num12 = num14 * 2;
            for (num6 = 0; num6 < outNum.Length; num6++)
            {
                int index = random.Next(this.Colors.Length - 1);
                int num16 = random.Next(this.Fonts.Length - 1);
                Font font = new Font(this.Fonts[num16], (float)fontSize, FontStyle.Bold);
                Brush brush = new SolidBrush(this.Colors[index]);
                if ((num6 % 2) == 1)
                {
                    num10 = num12;
                }
                else
                {
                    num10 = num11;
                }
                num9 = num6 * num2;
                graphics.DrawString(outNum.Substring(num6, 1), font, brush, (float)num9, (float)num10);
            }
            graphics.DrawRectangle(new Pen(Color.Gainsboro, 0f), 0, 0, image.Width - 1, image.Height - 1);
            graphics.Dispose();
            if (!this.EnableTwistImage)
            {
                return image;
            }
            if (this.TwistDegree == TwistImageDegree.Normal)
            {
                return TwistImage(image, false, 3.0, 4.0);
            }
            return TwistImage(image, true, 8.0, 4.0);
        }

        private int[] CreateVerifyCode()
        {
            Random random = new Random();
            int i = random.Next(0,100);
            int i2 = random.Next(0, 100);
            int[] code = new int[]{i,i2};
            return code;
        }

        public static Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            Bitmap image = new Bitmap(srcBmp.Width, srcBmp.Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, image.Width, image.Height);
            graphics.Dispose();
            double num = bXDir ? ((double)image.Height) : ((double)image.Width);
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    double a = 0.0;
                    a = bXDir ? ((6.2831853071795862 * j) / num) : ((6.2831853071795862 * i) / num);
                    a += dPhase;
                    double num5 = Math.Sin(a);
                    int x = 0;
                    int y = 0;
                    x = bXDir ? (i + ((int)(num5 * dMultValue))) : i;
                    y = bXDir ? j : (j + ((int)(num5 * dMultValue)));
                    Color pixel = srcBmp.GetPixel(i, j);
                    if ((((x >= 0) && (x < image.Width)) && (y >= 0)) && (y < image.Height))
                    {
                        image.SetPixel(x, y, pixel);
                    }
                }
            }
            return image;
        }

        public Color BackgroundColor
        {
            get
            {
                return this.backgroundColor;
            }
            set
            {
                this.backgroundColor = value;
            }
        }

        public Color ChaosColor
        {
            get
            {
                return this.chaosColor;
            }
            set
            {
                this.chaosColor = value;
            }
        }

        public string CodeSerial
        {
            get
            {
                return this.codeSerial;
            }
            set
            {
                this.codeSerial = value;
            }
        }

        public Color[] Colors
        {
            get
            {
                return this.colors;
            }
            set
            {
                this.colors = value;
            }
        }

        public bool EnableChaos
        {
            get
            {
                return this.enableChaos;
            }
            set
            {
                this.enableChaos = value;
            }
        }

        public bool EnableTwistImage
        {
            get
            {
                return this.enableTwistImage;
            }
            set
            {
                this.enableTwistImage = value;
            }
        }

        public string[] Fonts
        {
            get
            {
                return this.fonts;
            }
            set
            {
                this.fonts = value;
            }
        }

        public int FontSize
        {
            get
            {
                return this.fontSize;
            }
            set
            {
                this.fontSize = value;
            }
        }

        public int Length
        {
            get
            {
                return this.length;
            }
            set
            {
                this.length = value;
            }
        }

        public int Padding
        {
            get
            {
                return this.padding;
            }
            set
            {
                this.padding = value;
            }
        }

        public TwistImageDegree TwistDegree
        {
            get
            {
                return this.twistDegree;
            }
            set
            {
                this.twistDegree = value;
            }
        }

        public enum TwistImageDegree
        {
            Normal,
            High
        }
    }
}
