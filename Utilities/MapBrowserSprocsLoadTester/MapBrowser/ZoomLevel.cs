using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapBrowserSprocsLoadTester.MapBrowser
{
	class ZoomLevel
	{
		public event Action<ZoomLevel> Changed;
		public ZoomLevel(int initialValue)
		{
			this.value = initialValue;
			if (this.Changed != null) this.Changed(this);
		}

		internal int value;
		internal int Value
		{
			get
			{
				return value;
			}
			set
			{
				if (value < 6) return;
				if (value > 17) return;

				this.value = value;
			}
		}

		private double[] viewportHeights = new double[]
		{
			0,
			0,
			0,
			0,
			0,
			0,
			4.0958302815689,
			2.0458772060719,
			1.0237602386505,
			0.512531017597702,
			0.256303610381401,
			0.128226316191899,
			0.0641227739264991,
			0.0320561244036028,
			0.0160288513690006,
			0.00801431292119759,
			0.00400714001629865,
			0.00200361111539848
		};

		private double[] viewportWidths = new double[]
		{
			0,
			0,
			0,
			0,
			0,
			0,
			12.744140625,
			6.3720703125,
			3.18603515625,
			1.593017578125,
			0.7965087890625,
			0.39825439453125,
			0.199127197265625,
			0.0995635986328123,
			0.0497817993164062,
			0.0248908996582022,
			0.012445449829101,
			0.006222724914551
		};

		internal double ViewportHeight
		{
			get { return this.viewportHeights[Value]; }
		}
		internal double ViewportWidth
		{
			get { return this.viewportWidths[Value]; }
		}
	}
}
