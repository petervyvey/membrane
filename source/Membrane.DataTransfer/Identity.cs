
using System;
using Membrane.Foundation.DataTransfer;

namespace Membrane.DataTransfer
{
	public class Identity
		: DataTransferObject
	{
		/// <summary>
		/// The ID.
		/// </summary>
		public Guid ID { get; set; }

		/// <summary>
		/// The Name.
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		/// The FirstName.
		/// </summary>
		public virtual string FirstName { get; set; }

		/// <summary>
		/// The Locale.
		/// </summary>
		public virtual string Locale { get; set; }
	}
}
