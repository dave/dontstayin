namespace MapBrowserSprocsLoadTester.MapBrowser
{
	class StationaryViewportBehaviour : IViewportBehaviour
	{
		public void Move(Viewport viewport)
		{
			viewport.MoveAmount(0, 0);
		}
	}
}
