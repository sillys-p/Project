using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace BarCodeDemo
{
    class BarCodeHelper
    {
        /// <summary>
        /// 生成一维条形码
        /// </summary>
        /// <param name="txt">内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>bitmap对象</returns>
        public static Bitmap GenerateBarCode(string txt, int width, int height)
        {

            BarcodeWriter br = new BarcodeWriter();
            br.Format = BarcodeFormat.CODE_39;
            EncodingOptions options = new EncodingOptions() {Width = width ,Height=height,Margin = 2};
            br.Options = options;
            Bitmap map = br.Write(txt);
            return map;
        }
        public static Bitmap GenerateQRCode(string txt, int width,int height)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions option = new QrCodeEncodingOptions()
            {
                DisableECI = true,
                CharacterSet = "UTF-8",//设置内容编码
                Width = width,
                Height = height,
                Margin = 1//设置二维码的边距
            };
            writer.Options = option;
            Bitmap map = writer.Write(txt);
            return map;
           
        }

        public static Bitmap GenerateQRCodeWithLogo(string txt,int width,int height,Bitmap logo)
        {
            MultiFormatWriter writer = new MultiFormatWriter();
            Dictionary<EncodeHintType, object> hint = new Dictionary<EncodeHintType, object>();
            hint.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
            hint.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            //生成二维码
            BitMatrix bm = writer.encode(txt, BarcodeFormat.QR_CODE, width, height, hint);
            BarcodeWriter Bwriter = new BarcodeWriter();
            Bitmap map = Bwriter.Write(bm);
            //获取二维码实际尺寸
            int[] rectangle = bm.getEnclosingRectangle();
            //计算插入图片的大小和位置
            int middleW = Math.Min((int)(rectangle[2] / 3.5), logo.Width);
            int middleH = Math.Min((int)(rectangle[3]/3.5),logo.Height);
            int middleL = (map.Width - middleW) / 2;
            int middleT = (map.Height - middleH) / 2;
            Bitmap bmping = new Bitmap(map.Width, map.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmping))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(map, 0, 0, width, height);
            }
            //将二维码插入图片
            Graphics myGraphic = Graphics.FromImage(bmping);
            //白底
            myGraphic.FillRectangle(Brushes.White, middleL, middleT, middleW, middleH);
            myGraphic.DrawImage(logo, middleL, middleT, middleW, middleH);
            return bmping;

        }
    }
}
