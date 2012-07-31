namespace MapBrowserSprocsLoadTester.MapBrowser
{
	class BabyStepsViewportBehaviour : IViewportBehaviour
	{
		public void Move(Viewport viewport)
		{
			viewport.MoveAmount(0.0001, 0.0001);
		}
	}
}
