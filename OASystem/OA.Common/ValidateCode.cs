using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace OA.Common
{
    public class ValidateCode
    {
        public ValidateCode()
        {
        }
        /// <summary>
        /// Max Length of captcha.
        /// </summary>
        public int MaxLength
        {
            get { return 10; }
        }
        /// <summary>
        /// mini length of caotcha.
        /// </summary>
        public int MinLength
        {
            get { return 1; }
        }
        /// <summary>
        /// Generate captcha.
        /// </summary>
        /// <param name="length">set lenght of captcha.</param>
        /// <returns></returns>
        public string CreateValidateCode(int length)
        {
            int[] randMembers = new int[length];
            int[] validateNums = new int[length];
            string validateNumberStr = "";

            // generate original seek.
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            Random seekRand = new Random(seekSeek);
            int beginSeek = (int)seekRand.Next(0, Int32.MaxValue - length * 10000);
            int[] seeks = new int[length];
            for (int i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }
            // generate random number.
            for (int i = 0; i < length; i++)
            {
                Random rand = new Random(seeks[i]);
                int pownum = 1 * (int)Math.Pow(10, length);
                randMembers[i] = rand.Next(pownum, Int32.MaxValue);
            }
            // select random number.
            for (int i = 0; i < length; i++)
            {
                string numStr = randMembers[i].ToString();
                int numLength = numStr.Length;
                Random rand = new Random();
                int numPosition = rand.Next(0, numLength - 1);
                validateNums[i] = Int32.Parse(numStr.Substring(numPosition, 1));
            }
            // generate final captcha.
            for (int i = 0; i < length; i++)
            {
                validateNumberStr += validateNums[i].ToString();
            }
            return validateNumberStr;
        }





        /// <summary>
        /// Create Validate Graphic
        /// </summary>
        /// <param name="validateCode"></param>
        /// <param name="context"></param>
        public void CreateValidateGraphic(string validateCode, HttpContext context)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 12.0), 22);
            Graphics g = Graphics.FromImage(image);
            try
            {
                // instance random object.
                Random random = new Random();

                // clear background picture.
                g.Clear(Color.White);

                // draw interference lines
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                 Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);

                // draw interference points.
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                // draw frame of captcha.
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                // save captcha picture data.
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);

                // Output image stream
                context.Response.Clear();
                context.Response.ContentType = "image/jpeg";
                context.Response.BinaryWrite(stream.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }





        /// <summary>
        /// get Width of captcha picture.
        /// </summary>
        /// <param name="validateNumLength">validate Num Lengt</param>
        /// <returns></returns>
        public static int GetImageWidth(int validateNumLength)
        {
            return (int)(validateNumLength * 12.0);
        }





        /// <summary>
        /// Get height of Image.
        /// </summary>
        /// <returns></returns>
        public static double GetImageHeight()
        {
            return 22.5;
        }



        //C# MVC 
        /// <summary>
        /// create cpatcha.
        /// </summary>
        /// <param name="containsPage"></param>
        /// <param name="validateNum"></param>
        public byte[] CreateValidateGraphic(string validateCode)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 12.0), 22);
            Graphics g = Graphics.FromImage(image);
            try
            {
                // instance of random object.
                Random random = new Random();
                // clear background of picture.
                g.Clear(Color.White);
                // draw interference Lines.
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                 Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);
                //draw interference points.
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                // draw frame of captcha.
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                // save captcha picture.
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                // output image stream.
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}
