using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataInterface
{
	public interface IUnitOfWork
	{
		void Submit();
	}
}
