using System;
using System.Collections.Generic;
using System.Text;

namespace SpottedLibrary
{
	public interface IRequiresValidation
	{
		bool IsValid { get; }
	}
}
