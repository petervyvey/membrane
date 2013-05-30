
using System;
using System.Text;

namespace Membrane.Foundation.Runtime.Security
{
	/// <summary>
	/// Generates passwords that are pronounceable. Original Java code from
	/// http://www.multicians.org/thvv/gpw.html. This class is free to use without restrictions.
	/// </summary>
	/// <example>
	/// // Phonetical password example
	/// PronounceablePasswordGenerator generator = new PronounceablePasswordGenerator();
	/// int numPasswords = 5;
	/// passwordLength = 10;
	/// ArrayList arrayList = generator.Generate(numPassword,passwordLength.);
	/// 
	/// for (int i=0;i &lt; arrayList.Count;i++)
	/// {
	/// 	Console.Console.WriteLine(arrayList[i]);
	/// }
	/// </example>
	public partial class PronounceablePasswordStrategy
		: IPasswordGenerationStrategy
	{
		#region - Constants & static fields -

		/// <summary>
		/// The allowed characters.
		/// </summary>
		public const string alphabet = "abcdefghijklmnopqrstuvwxyz";

		/// <summary>
		/// Initialization data.
		/// </summary>
		private static GpwData data = new GpwData();

		#endregion

		/// <summary>
		/// Generates a pronounceable password.
		/// </summary>
		/// <param name="length">The character length of the passwords.</param>
		/// <returns>A generated pronounceable password.</returns>
		public string Generate(int length)
		{
			int c1, c2, c3;
			long sum = 0;
			int nchar = 0;
			long ranno = 0;
			double pik = 0;

			StringBuilder password = new StringBuilder();
			Random random = new Random(Guid.NewGuid().GetHashCode());
			password = new StringBuilder(length);

			// random number [0,1]
			pik = random.NextDouble(); 

			// weight by sum of frequencies
			ranno = (long)(pik * data.Sigma);
			sum = 0;
			for (c1 = 0; c1 < 26; c1++)
			{
				for (c2 = 0; c2 < 26; c2++)
				{
					for (c3 = 0; c3 < 26; c3++)
					{
						sum += data.get_Renamed(c1, c2, c3);
						if (sum > ranno)
						{
							password.Append(alphabet[c1]);
							password.Append(alphabet[c2]);
							password.Append(alphabet[c3]);

							// Found start. Break all 3 loops.
							c1 = 26;
							c2 = 26;
							c3 = 26;
						}
					}
				}
			}

			// Now do a random walk.
			nchar = 3;
			while (nchar < length)
			{
				c1 = alphabet.IndexOf((System.Char)password[nchar - 2]);
				c2 = alphabet.IndexOf((System.Char)password[nchar - 1]);
				sum = 0;

				for (c3 = 0; c3 < 26; c3++)
				{
					sum += data.get_Renamed(c1, c2, c3);
				}

				// exit while loop
				if (sum == 0) break;

				pik = random.NextDouble();
				ranno = (long)(pik * sum);
				sum = 0;
				for (c3 = 0; c3 < 26; c3++)
				{
					sum += data.get_Renamed(c1, c2, c3);
					if (sum > ranno)
					{
						password.Append(alphabet[c3]);

						// break for loop
						c3 = 26; 
					}
				}
				nchar++;
			} 

			return password.ToString();
		}
	}
}