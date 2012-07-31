namespace MapBrowserSprocsLoadTester.MapBrowser
{
	internal class ViewportFactory
	{
		private readonly IListener<Viewport> listener;
		private readonly int zoomLevel;

		public ViewportFactory(IListener<Viewport> listener, int zoomLevel)
		{
			this.listener = listener;
			this.zoomLevel = zoomLevel;
		}

		public Viewport Create(double south, double west)
		{
			Viewport viewport = new Viewport(south, west, new ZoomLevel(zoomLevel));
			viewport.Changed += v => listener.OnChanged(v);
			return viewport;
		}

	}
}
