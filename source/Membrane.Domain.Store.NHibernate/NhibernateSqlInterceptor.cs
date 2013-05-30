
using NHibernate;

namespace Membrane.Domain.Store
{
	/// <summary>
	/// The NHibernate <see cref="IInterceptor"/> that intercepts the executed SQL statement.
	/// </summary>
	public class NhibernateSqlInterceptor 
		: EmptyInterceptor, IInterceptor
	{
		/// <summary>
		/// The prepared SQL statement is copied to <see cref="NHibernateSql.LastSqlStatement"/>.
		/// </summary>
		/// <param name="sqlStatement">The <see cref="NHibernate.SqlCommand.SqlString"/> to be prepared.</param>
		/// <returns>A <see cref="NHibernate.SqlCommand.SqlString"/>.</returns>
		NHibernate.SqlCommand.SqlString IInterceptor.OnPrepareStatement(NHibernate.SqlCommand.SqlString sqlStatement)
		{
			NHibernateSql.LastSqlStatement = sqlStatement.ToString();
			return sqlStatement;
		}
	}
}
