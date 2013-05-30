
using System;
using Membrane.Foundation.DataTransfer;

namespace Membrane.DataTransfer
{
	public class OAuth2UserIdentity
		: DataTransferObject
	{
		/// <summary>
		/// The ID.
		/// </summary>
		public Guid ID { get; set; }

		/// <summary>
		/// The Identifier.
		/// </summary>
		public virtual OAuth2User User { get; set; }

		/// <summary>
		/// The Secret.
		/// </summary>
		public virtual Identity Identity { get; set; }

	}
}
