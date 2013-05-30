
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Membrane.Foundation.Extension
{
	/// <summary>
	/// Extension methods to handle extra compression functionality on strings and arrays of bytes.
	/// </summary>
	public static class CompressionExtension
	{
		/// <summary>
		/// Converts a string value to an array of bytes.
		/// </summary>
		/// <param name="value">The string value.</param>
		/// <returns>An array of bytes representing the string value.</returns>
		public static byte[] ToByteArray(this string value)
		{
			return Encoding.ASCII.GetBytes(value);
		}

		/// <summary>
		/// Converts an array of bytes to a string value.
		/// </summary>
		/// <param name="value">The array of bytes representing the string value.</param>
		/// <returns>A string value.</returns>
		public static string FromByteArray(this byte[] value)
		{
			return Encoding.ASCII.GetString(value);
		}

		/// <summary>
		/// Encode a string value to a BASE64 representation.
		/// </summary>
		/// <param name="toEncode">The string to encode.</param>
		/// <returns>The BASE64 encoded representation.</returns>
		public static string EncodeToBase64(this string toEncode)
		{

			byte[] toEncodeAsBytes = ASCIIEncoding.ASCII.GetBytes(toEncode);
			string base64Value = Convert.ToBase64String(toEncodeAsBytes);

			return base64Value;
		}

		/// <summary>
		/// Decodes a BASE64 encoded string to a normal representation.
		/// </summary>
		/// <param name="encodedData">The encoded data.</param>
		/// <returns>The normal string.</returns>
		public static string DecodeFromBase64(this string encodedData)
		{
			byte[] encodedDataAsBytes = Convert.FromBase64String(encodedData);
			string value = ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

			return value;
		}

		/// <summary>
		/// Converts a string to an array of bytes.
		/// </summary>
        /// <param name="base64Value">The string value.</param>
		/// <returns>The array of bytes.</returns>
		public static byte[] ToBase64Bytes(this string base64Value)
		{
			return (Convert.FromBase64String(base64Value));
		}

		/// <summary>
		/// Converts an array of bytes to a string.
		/// </summary>
		/// <param name="bytes">The array of bytes.</param>
		/// <returns>The string value.</returns>
		public static string ToBase64String(this byte[] bytes)
		{
			return (Convert.ToBase64String(bytes));
		}

		/// <summary>
		/// Compresses an array of bytes.
		/// </summary>
		/// <param name="bytes">The array of bytes to compress.</param>
		/// <returns>The compressed array of bytes.</returns>
		public static byte[] Compress(this byte[] bytes)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				using (GZipStream compressedStream = new GZipStream(stream, CompressionMode.Compress, true))
				{
					compressedStream.Write(bytes, 0, bytes.Length);
					compressedStream.Close();
				}

				return stream.ToArray();
			}
		}

		/// <summary>
		/// Decompresses an array of bytes.
		/// </summary>
		/// <param name="bytes">The array of bytes to decompress.</param>
		/// <returns>The decompressed array of bytes.</returns>
		public static byte[] Decompress(this byte[] bytes)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				stream.Write(bytes, 0, bytes.Length);
				stream.Position = 0;

				using (GZipStream compressedStream = new GZipStream(stream, CompressionMode.Decompress, true))
				{
					using (MemoryStream output = new MemoryStream())
					{
						byte[] buffer = new byte[32*1024];
						int count = compressedStream.Read(buffer, 0, buffer.Length);
						while (count > 0)
						{
							output.Write(buffer, 0, count);
							count = compressedStream.Read(buffer, 0, buffer.Length);
						}

						compressedStream.Close();
						return output.ToArray();
					}
				}
			}
		}
	}
}
