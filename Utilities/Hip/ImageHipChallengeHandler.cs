// Stephen Toub
// stoub@microsoft.com

using System;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Specialized;

namespace Msdn.Web.UI.WebControls
{
	/// <summary>Handles requests for dynamic images from the ImageHipChallenge control.</summary>
	public class ImageHipChallengeHandler : BaseHipChallengeHandler, IHttpHandler
	{
		/// <summary>Maximum width of an image to generate.</summary>
		private const int MAX_IMAGE_WIDTH = 600;
		/// <summary>Maximum height of an image to generate.</summary>
		private const int MAX_IMAGE_HEIGHT = 600;

		/// <summary>Gets whether this handler is reusable.</summary>
		/// <remarks>This handler is not thread-safe (uses non thread-safe member variables), so it is not reusable.</remarks>
		public bool IsReusable { get { return false; } }

		/// <summary>Processes the image request and generates the appropriate image.</summary>
		/// <param name="context">The current HttpContext.</param>
		public void ProcessRequest(HttpContext context)
		{
			// Retrieve query parameters and challenge text
			NameValueCollection queryString = context.Request.QueryString;
			int width = Convert.ToInt32(queryString[ImageHipChallenge.WIDTH_KEY]);
			if (width <= 0 || width > MAX_IMAGE_WIDTH) throw new ArgumentOutOfRangeException(ImageHipChallenge.WIDTH_KEY);
			int height = Convert.ToInt32(queryString[ImageHipChallenge.HEIGHT_KEY]);
			if (height <= 0 || height > MAX_IMAGE_HEIGHT) throw new ArgumentOutOfRangeException(ImageHipChallenge.HEIGHT_KEY);
			string text = HipChallenge.GetChallengeText(new Guid(queryString[ImageHipChallenge.ID_KEY]));

			if (text != null)
			{
				// We successfully retrieved the information, so generate the image and send it to the client.
				HttpResponse resp = context.Response;
				resp.Clear();
				resp.ContentType = "img/jpeg";
				using(Bitmap bmp = GenerateImage(text, new Size(width, height)))
				{
					bmp.Save(resp.OutputStream, ImageFormat.Jpeg);
				}
			}
		}

		/// <summary>Generates the challenge image.</summary>
		/// <param name="text">The text to be rendered into the image.</param>
		/// <param name="size">The size of the image to generate.</param>
		/// <returns>A dynamically-generated challenge image.</returns>
		public Bitmap GenerateImage(string text, Size size)
		{

			    
   
		}
		
		public static bool Smooth(Bitmap b, int nWeight /* default to 1 */)
		{
			ConvMatrix m = new ConvMatrix();
			m.SetAll(1);
			m.Pixel = nWeight;
			m.Factor = nWeight + 8;

			return Conv3x3(b, m);
		}
		public class ConvMatrix
		{
			public int TopLeft = 0, TopMid = 0, TopRight = 0;
			public int MidLeft = 0, Pixel = 1, MidRight = 0;
			public int BottomLeft = 0, BottomMid = 0, BottomRight = 0;
			public int Factor = 1;
			public int Offset = 0;
			public void SetAll(int nVal)
			{
				TopLeft = TopMid = TopRight = MidLeft = Pixel = MidRight =
						  BottomLeft = BottomMid = BottomRight = nVal;
			}
		}
		public static bool Conv3x3(Bitmap b, ConvMatrix m)
		{
			// Avoid divide by zero errors
			if (0 == m.Factor)
				return false; Bitmap

			// GDI+ still lies to us - the return format is BGR, NOT RGB. 
			bSrc = (Bitmap)b.Clone();
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
								ImageLockMode.ReadWrite,
								PixelFormat.Format24bppRgb);
			BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height),
							   ImageLockMode.ReadWrite,
							   PixelFormat.Format24bppRgb);
			int stride = bmData.Stride;
			int stride2 = stride * 2;

			System.IntPtr Scan0 = bmData.Scan0;
			System.IntPtr SrcScan0 = bmSrc.Scan0;

			unsafe
			{
				byte* p = (byte*)(void*)Scan0;
				byte* pSrc = (byte*)(void*)SrcScan0;
				int nOffset = stride - b.Width * 3;
				int nWidth = b.Width - 2;
				int nHeight = b.Height - 2;

				int nPixel;

				for (int y = 0; y < nHeight; ++y)
				{
					for (int x = 0; x < nWidth; ++x)
					{
						nPixel = ((((pSrc[2] * m.TopLeft) +
							(pSrc[5] * m.TopMid) +
							(pSrc[8] * m.TopRight) +
							(pSrc[2 + stride] * m.MidLeft) +
							(pSrc[5 + stride] * m.Pixel) +
							(pSrc[8 + stride] * m.MidRight) +
							(pSrc[2 + stride2] * m.BottomLeft) +
							(pSrc[5 + stride2] * m.BottomMid) +
							(pSrc[8 + stride2] * m.BottomRight))
							/ m.Factor) + m.Offset);

						if (nPixel < 0) nPixel = 0;
						if (nPixel > 255) nPixel = 255;
						p[5 + stride] = (byte)nPixel;

						nPixel = ((((pSrc[1] * m.TopLeft) +
							(pSrc[4] * m.TopMid) +
							(pSrc[7] * m.TopRight) +
							(pSrc[1 + stride] * m.MidLeft) +
							(pSrc[4 + stride] * m.Pixel) +
							(pSrc[7 + stride] * m.MidRight) +
							(pSrc[1 + stride2] * m.BottomLeft) +
							(pSrc[4 + stride2] * m.BottomMid) +
							(pSrc[7 + stride2] * m.BottomRight))
							/ m.Factor) + m.Offset);

						if (nPixel < 0) nPixel = 0;
						if (nPixel > 255) nPixel = 255;
						p[4 + stride] = (byte)nPixel;

						nPixel = ((((pSrc[0] * m.TopLeft) +
									   (pSrc[3] * m.TopMid) +
									   (pSrc[6] * m.TopRight) +
									   (pSrc[0 + stride] * m.MidLeft) +
									   (pSrc[3 + stride] * m.Pixel) +
									   (pSrc[6 + stride] * m.MidRight) +
									   (pSrc[0 + stride2] * m.BottomLeft) +
									   (pSrc[3 + stride2] * m.BottomMid) +
									   (pSrc[6 + stride2] * m.BottomRight))
							/ m.Factor) + m.Offset);

						if (nPixel < 0) nPixel = 0;
						if (nPixel > 255) nPixel = 255;
						p[3 + stride] = (byte)nPixel;

						p += 3;
						pSrc += 3;
					}

					p += nOffset;
					pSrc += nOffset;
				}
			}

			b.UnlockBits(bmData);
			bSrc.UnlockBits(bmSrc);
			return true;
		}

		/// <summary>Distorts the image.</summary>
		/// <param name="b">The image to be transformed.</param>
		/// <param name="distortion">An amount of distortion.</param>
		private static void DistortImage(Bitmap b, double distortion)
		{
			int width = b.Width, height = b.Height;

			// Copy the image so that we're always using the original for source color
			using (Bitmap copy = (Bitmap)b.Clone())
			{
				// Iterate over every pixel
				for (int y = 0; y < height; y++)
				{
					for (int x = 0; x < width; x++)
					{
						// Adds a simple wave
						int newX = (int)(x + (distortion * Math.Sin(Math.PI * y / 64.0)));
						int newY = (int)(y + (distortion * Math.Cos(Math.PI * x / 64.0)));
						if (newX < 0 || newX >= width) newX = 0;
						if (newY < 0 || newY >= height) newY = 0;
						b.SetPixel(x, y, copy.GetPixel(newX, newY));
					}
				}
			}
		}

		/// <summary>List of fonts that can be used for rendering text.</summary>
		/// <remarks>This list can be changed to include any families available on the current system.</remarks>
		private static FontFamily [] _families = { 
													 new FontFamily("Times New Roman"),
													 new FontFamily("Georgia"),
													 new FontFamily("Arial"),
													 new FontFamily("Comic Sans MS")
												 };
	}
}
