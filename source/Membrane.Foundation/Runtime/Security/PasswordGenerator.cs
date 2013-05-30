
using System;

namespace Membrane.Foundation.Runtime.Security
{
	/// <summary>
	/// Wrapper for password generation strategies.
	/// </summary>
	/// <typeparam name="T">The password strategy type to use to generate a password.</typeparam>
	public static class PasswordGenerator<T>
		where T : IPasswordGenerationStrategy, new()
	{
		/// <summary>
		/// Generates a random password of the exact length.
		/// </summary>
		/// <param name="length">Exact password length.</param>
		/// <returns>
		/// Randomly generated password.
		/// </returns>
		public static string Generate(int length)
		{
			T passwordStrategy = new T();

			return passwordStrategy.Generate(length);
		}
	}
}
