using System;

namespace Bobs
{
	/// <summary>
	/// Links an event to one or many brands
	/// </summary>
	[Serializable] 
	public partial class EventBrand
	{

		#region simple members
		/// <summary>
		/// Link to Event table
		/// </summary>
		public override int EventK
		{
			get { return (int)this[EventBrand.Columns.EventK]; }
			set { this._event = null; this[EventBrand.Columns.EventK] = value; }
		}
		/// <summary>
		/// Link to the Brand table
		/// </summary>
		public override int BrandK
		{
			get { return (int)this[EventBrand.Columns.BrandK]; }
			set { this.brand = null; this[EventBrand.Columns.BrandK] = value; }
		}
		#endregion

		#region Links to Bobs
		#region Event
		public Event Event
		{
			get
			{
				if (this._event==null)
					this._event = new Event(this.EventK);
				return this._event;
			}
		}
		Event _event;
		#endregion
		#region Brand
		public Brand Brand
		{
			get
			{
				if (this.brand==null)
					this.brand = new Brand(this.BrandK);
				return this.brand;
			}
		}
		Brand brand;
		#endregion
		#endregion
		
	}
}
