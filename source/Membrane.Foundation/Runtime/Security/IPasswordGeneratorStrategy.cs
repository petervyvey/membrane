
using System;

namespace Membrane.Foundation.Runtime.Security
{
	/// <summary>
	/// Defines the API for password generation strategies.
	/// </summary>
	public interface IPasswordGenerationStrategy
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="length"></param>
		/// <returns></returns>
		string Generate(int length);
	}
}
