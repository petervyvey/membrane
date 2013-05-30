
namespace Membrane.Web.Data
{
    public sealed class ServiceDataResponse
        : ServiceResponse
    {

        public ServiceDataResponse(string version, ResponseStatus status, object result)
            : base(version, status)
        {
            this.Result = result;
        }

        public ServiceDataResponse(string version, string endPoint, ResponseStatus status, object result)
            : base(version, endPoint, status)
        {
            this.Result = result;
        }

        public object Result { get; private set; }
    }
}