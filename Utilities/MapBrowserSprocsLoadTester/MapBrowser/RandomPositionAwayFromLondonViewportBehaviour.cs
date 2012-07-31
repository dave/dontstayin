using System;
namespace MapBrowserSprocsLoadTester.MapBrowser
{
	class RandomPositionAwayFromLondonViewportBehaviour : IViewportBehaviour
	{
		private Random random = new Random(1);
		public void Move(Viewport viewport)
		{
            //London: N: 51.507460346127864, E: -0.1009368896484375, S: 51.49143089340988, W: -0.15071868896484375
			viewport.SetPosition(51.5 + 20 * (random.NextDouble() - 0.5), -0.1 + 20 * (random.NextDouble() - 0.5),
			                     viewport.ZoomLevel.Value);
		}
	}
}
