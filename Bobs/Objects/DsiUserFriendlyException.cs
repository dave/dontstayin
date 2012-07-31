using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs
{
	public class DsiUserFriendlyException : Exception
	{
		public DsiUserFriendlyException(string exceptionText) : base(exceptionText) { }
		public DsiUserFriendlyException(string exceptionText, Exception innerException) : base(exceptionText, innerException) { }
	}
}
