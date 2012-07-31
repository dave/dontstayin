using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
	public delegate T Getter<T>();
	public delegate T Getter<T, U>(U u);

	public delegate void Action();
}
