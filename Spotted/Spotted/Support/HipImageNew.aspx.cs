using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Spotted.Support
{
	public partial class HipImageNew : System.Web.UI.Page
	{
		Random HipRandom;
		private void Page_Load(object sender, System.EventArgs e)
		{
			HipRandom = new Random();

			Bitmap b;
			int width = 150;
			int height = 50;
			string text1 = Cambro.Misc.Utility.Decrypt(Request.QueryString[0]);
			string text = "";
			try
			{
				text = text1.Split('|')[0].ToUpper();
			}
			catch { }

			if (text.Length != 5)
				throw new Exception();

			if (HipRandom.Next(2) == 1)
				b = Type1(text, width, height);
			else
				b = Type2(text, width, height);

			Response.Clear();
			Response.ContentType = "image/jpeg";
			b.Save(this.Response.OutputStream, ImageFormat.Jpeg);
			b.Dispose();


		}

		public Bitmap Type1(string text, int width, int height)
		{
			// Create a new 32-bit bitmap image.
			Bitmap bitmap = new Bitmap(
			width,
			height,
			PixelFormat.Format32bppArgb);

			// Create a graphics object for drawing.
			Graphics g = Graphics.FromImage(bitmap);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			Rectangle rect = new Rectangle(0, 0, width, height);

			// Fill in the background.
			HatchBrush hatchBrush = new HatchBrush(
			HatchStyle.SmallConfetti,
			Color.LightGray,
			Color.White);
			g.FillRectangle(hatchBrush, rect);

			// Set up the text font.
			SizeF size;
			float fontSize = rect.Height + 1;
			Font font;
			// Adjust the font size until the text fits within the image.
			do
			{
				fontSize--;
				font = new Font(
				System.Drawing.FontFamily.GenericSansSerif.Name,
				fontSize,
				FontStyle.Bold);
				size = g.MeasureString(text, font);
			} while (size.Width > rect.Width);

			// Set up the text format.
			StringFormat format = new StringFormat();
			format.Alignment = StringAlignment.Center;
			format.LineAlignment = StringAlignment.Center;

			// Create a path using the text and warp it randomly.
			GraphicsPath path = new GraphicsPath();
			path.AddString(
			text,
			font.FontFamily,
			(int)font.Style,
			font.Size, rect,
			format);
			float v = 4F;
			PointF[] points =
			{
				new PointF(
				this.HipRandom.Next(rect.Width) / v,
				this.HipRandom.Next(rect.Height) / v),
				new PointF(
				rect.Width - this.HipRandom.Next(rect.Width) / v,
				this.HipRandom.Next(rect.Height) / v),
				new PointF(
				this.HipRandom.Next(rect.Width) / v,
				rect.Height - this.HipRandom.Next(rect.Height) / v),
				new PointF(
				rect.Width - this.HipRandom.Next(rect.Width) / v,
				rect.Height - this.HipRandom.Next(rect.Height) / v)
			};
			Matrix matrix = new Matrix();
			matrix.Translate(0F, 0F);
			path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);

			// Draw the text.
			hatchBrush = new HatchBrush(
			HatchStyle.LargeConfetti,
			Color.LightGray,
			Color.DarkGray);
			g.FillPath(hatchBrush, path);

			// Add some random noise.
			int m = Math.Max(rect.Width, rect.Height);
			for (int i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
			{
				int x = this.HipRandom.Next(rect.Width);
				int y = this.HipRandom.Next(rect.Height);
				int w = this.HipRandom.Next(m / 50);
				int h = this.HipRandom.Next(m / 50);
				g.FillEllipse(hatchBrush, x, y, w, h);
			}

			// Clean up.
			font.Dispose();
			hatchBrush.Dispose();
			g.Dispose();

			// Set the image.
			return bitmap;
		}

		public Bitmap Type2(string text, int width, int height)
		{
			//Create instance of bitmap object
			Bitmap objBitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

			//Create instance of graphics object
			Graphics objGraphics = Graphics.FromImage(objBitmap);
			objGraphics.SmoothingMode = SmoothingMode.AntiAlias;
			Rectangle objRect = new Rectangle(0, 0, width, height);

			//Fill the background in a light gray pattern
			HatchBrush objHatchBrush = new HatchBrush(HatchStyle.DiagonalCross, Color.LightGray, Color.White);
			objGraphics.FillRectangle(objHatchBrush, objRect);

			//Determine the appropriate font size
			SizeF objSize;
			float flFontSize = objRect.Height + 1;
			Font objFont;
			do	//Decrease font size until text fits within the space
			{
				flFontSize--;
				objFont = new Font(System.Drawing.FontFamily.GenericSansSerif.Name, flFontSize, FontStyle.Bold);
				objSize = objGraphics.MeasureString(text, objFont);
			} while (objSize.Width > objRect.Width);

			//Format the text
			StringFormat objStringFormat = new StringFormat();
			objStringFormat.Alignment = StringAlignment.Center;
			objStringFormat.LineAlignment = StringAlignment.Center;

			//Create a path using the text and randomly warp it
			GraphicsPath objGraphicsPath = new GraphicsPath();
			objGraphicsPath.AddString(text, objFont.FontFamily, (int)objFont.Style, objFont.Size, objRect, objStringFormat);
			float flV = 4F;

			//Create a parallelogram for the text to draw into
			PointF[] arrPoints =
			{
				new PointF(HipRandom.Next(objRect.Width) / flV, HipRandom.Next(objRect.Height) / flV),
				new PointF(objRect.Width - HipRandom.Next(objRect.Width) / flV, HipRandom.Next(objRect.Height) / flV),
				new PointF(HipRandom.Next(objRect.Width) / flV, objRect.Height - HipRandom.Next(objRect.Height) / flV),
				new PointF(objRect.Width - HipRandom.Next(objRect.Width) / flV, objRect.Height - HipRandom.Next(objRect.Height) / flV)
			};

			//Create the warped parallelogram for the text
			Matrix objMatrix = new Matrix();
			objMatrix.Translate(0F, 0F);
			objGraphicsPath.Warp(arrPoints, objRect, objMatrix, WarpMode.Perspective, 0F);

			//Add the text to the shape
			objHatchBrush = new HatchBrush(HatchStyle.LargeConfetti, Color.DarkGray, Color.Black);
			objGraphics.FillPath(objHatchBrush, objGraphicsPath);

			//Add some random noise
			int intMax = Math.Max(objRect.Width, objRect.Height);
			for (int i = 0; i < (int)(objRect.Width * objRect.Height / 30F); i++)
			{
				int x = HipRandom.Next(objRect.Width);
				int y = HipRandom.Next(objRect.Height);
				int w = HipRandom.Next(intMax / 15);
				int h = HipRandom.Next(intMax / 70);
				objGraphics.FillEllipse(objHatchBrush, x, y, w, h);
			}

			//Release memory
			objFont.Dispose();
			objHatchBrush.Dispose();
			objGraphics.Dispose();

			//Set the public property to the 
			return objBitmap;
		}
	}
}
