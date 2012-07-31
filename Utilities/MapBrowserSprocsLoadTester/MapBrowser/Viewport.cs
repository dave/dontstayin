using System;

namespace MapBrowserSprocsLoadTester.MapBrowser
{
	class Viewport
	{
		public event Action<Viewport> Changed;
		public Viewport(double south, double west, ZoomLevel zoomLevel)
		{
			South = south;
			West = west;
			ZoomLevel = zoomLevel;
			this.ZoomLevel.Changed += (z) => { if (this.Changed != null) this.Changed(this); };
		}


		private double south;
		internal double South
		{
			get { return this.south; }
			private set
			{
				if (value < -45) return;
				if (value > 80) return;
				this.south = value;
			}
		}

		private double west;
		internal double West
		{
			get { return this.west; }
			private set
			{
				if (value < -180)
				{
					this.west = value + 360;
				}
				else if (value > 180)
				{
					this.west = value - 360;
				}
				else this.west = value;
			}
		}

		internal ZoomLevel ZoomLevel { get; set; }
		internal void MoveAmount(double numberOfViewportsNorth, double numberOfViewportsEast)
		{
			South -= numberOfViewportsNorth * ZoomLevel.ViewportHeight;
			West -= numberOfViewportsEast * ZoomLevel.ViewportWidth;
			if (this.Changed != null) this.Changed(this);
		}
		internal double North
		{
			get { return South + ZoomLevel.ViewportHeight; }
		}
		internal double East
		{
			get { return West + ZoomLevel.ViewportWidth; }
		}

		internal void SetPosition(double centerLat, double centreLon, int zoomLevel)
		{
			this.ZoomLevel.Value = zoomLevel;
			this.West = centreLon - this.ZoomLevel.ViewportWidth / 2;
			this.South = centerLat - ZoomLevel.ViewportHeight / 2;
			if (this.Changed != null) this.Changed(this);
		}
	}
}
