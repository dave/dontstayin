using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
namespace Bobs
{
	[TestFixture]
	public class InvoiceItemTests
	{
		[Test]
		public void SetTotal_Test()
		{
			InvoiceItem ii = new InvoiceItem();
			ii.SetTotal(5.00m);
			
		}
	}
}
