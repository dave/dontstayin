namespace MapBrowserSprocsLoadTester
{
	internal interface IResultLogger<T>
	{
		void Log(T t, long milliseconds, int rowCount);
	}
}
