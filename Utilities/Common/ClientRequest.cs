using System;
#region ClientAttribute
namespace Common
{
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class ClientAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ClientAttribute"/> class.
		/// </summary>
		public ClientAttribute() { }
	}
}
#endregion
