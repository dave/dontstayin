using System;

namespace MapBrowserSprocsLoadTester
{
	internal interface IListener<T>
	{
		void OnChanged(T t);
	}
}
