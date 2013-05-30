
using System;
using System.Transactions;

namespace Membrane.Infrastructure
{
    /// <summary>
    /// The system constants.
    /// </summary>
    public static class DomainConstant
    {
        /// <summary>
        /// The system default date template.
        /// </summary>
        public const string DEFAULT_DATE_TEMPLATE = "yyyy-MM-dd";

        /// <summary>
        /// The system default start date/time value.
        /// </summary>
        public static DateTime DEFAULT_START_DATE_TIME = DateTime.Parse("1900-01-01");

        /// <summary>
        /// The system default stop date/time value.
        /// </summary>
        public static DateTime DEFAULT_STOP_DATE_TIME = DateTime.Parse("2100-01-01");

        /// <summary>
        /// The <see cref="TransactionScopeOption"/>.Suppress placeholder.
        /// </summary>
        public static TransactionScopeOption TRANSACTION_SCOPE_OPTION_SUPPRESS = TransactionScopeOption.Suppress;

        /// <summary>
        /// The <see cref="TransactionScopeOption"/>.Required placeholder.
        /// </summary>
        public static TransactionScopeOption TRANSACTION_SCOPE_OPTION_REQUIRED = TransactionScopeOption.Suppress;

        /// <summary>
        /// The <see cref="TransactionScopeOption"/>.RequiresNew placeholder.
        /// </summary>
        public static TransactionScopeOption TRANSACTION_SCOPE_OPTION_REQUIRES_NEW = TransactionScopeOption.Suppress;
    }
}
