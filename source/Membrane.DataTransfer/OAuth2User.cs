
using System;
using System.Collections.Generic;
using Membrane.Foundation.DataTransfer;

namespace Membrane.DataTransfer
{
	public class OAuth2User
		: DataTransferObject
	{
		/// <summary>
		/// The ID.
		/// </summary>
		public Guid ID { get; set; }

		/// <summary>
		/// The Identifier.
		/// </summary>
		public virtual string Identifier { get; set; }

		/// <summary>
		/// The Password.
		/// </summary>
		public virtual string Password { get; set; }

		/// <summary>
		/// The Scopes.
		/// </summary>
		public IList<string> Scopes { get; set; }
	}
}
