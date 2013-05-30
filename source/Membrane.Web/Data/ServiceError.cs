
using System;

namespace Membrane.Web.Data
{
	public sealed class ServiceError
	{
		public ServiceError(Exception exception)
		{
			this.Timestamp = DateTime.UtcNow;
            this.Type = exception.GetType().Name;
			this.Message = exception.Message;
		}

		public DateTime Timestamp { get; private set; }

        public string Type { get; set; }

		public string Message { get; set; }
	}
}