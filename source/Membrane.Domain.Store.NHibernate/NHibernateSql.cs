
namespace Membrane.Domain.Store
{
	/// <summary>
	/// Static class to hold the last executed SQL statement in an NHibernate session.
	/// </summary>
	public static class NHibernateSql
	{
		/// <summary>
		/// The last executed statement.
		/// </summary>
		public static string LastSqlStatement { get; set; }
	}
}
