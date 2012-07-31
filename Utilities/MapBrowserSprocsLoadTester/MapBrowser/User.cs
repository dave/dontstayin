using System;
using System.Threading;

namespace MapBrowserSprocsLoadTester.MapBrowser
{
	class User 
	{
		private Random random;
		private readonly IViewportBehaviour viewportBehaviour;
		private Viewport viewport;
		public long NextUpdateTime;

		public User(Random random, ViewportFactory factory, IViewportBehaviour viewportBehaviour)
		{
			this.random = random;
			this.viewportBehaviour = viewportBehaviour;
			viewport = factory.Create(51.65211086156918 + random.NextDouble(), 0.251312255859375 + random.NextDouble());
		}



		private void MoveViewport()
		{
			viewportBehaviour.Move(viewport);
			//viewport.MoveAmount(
			//    2 * (random.NextDouble() - 0.5),
			//    2 * (random.NextDouble() - 0.5)
			//    );
		}




		public void Update(long milliseconds, int timeBetweenRequestsInMs)
		{
			this.NextUpdateTime = milliseconds + random.Next(timeBetweenRequestsInMs);
			this.MoveViewport();
		}
	}
}
