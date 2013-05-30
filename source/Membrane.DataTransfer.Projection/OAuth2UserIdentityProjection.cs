
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Membrane.Foundation.DataTransfer;
using Membrane.Foundation.Extension;

namespace Membrane.DataTransfer.Projection
{
    /// <summary>
	/// The <see cref="OAuth2UserIdentity"/> projection.
    /// </summary>
	public static class OAuth2UserIdentityProjection
    {

		public static DataTransfer.OAuth2UserIdentity ToDataTransferValue(this Domain.Entity.OAuth2UserIdentity entity, params PropertyDirective[] directives)
        {
			DataTransfer.OAuth2UserIdentity value =
				new DataTransfer.OAuth2UserIdentity
                {
                    ID = entity.ID,
					User = entity.User.ToDataTransferValue(),
					Identity = entity.Identity.ToDataTransferValue(),

                    __references = entity.__references,
                    __extensions = entity.__extensions
                };

            return value;
        }

		public static Domain.Entity.OAuth2UserIdentity ToEntity(this DataTransfer.OAuth2UserIdentity value)
        {
			Domain.Entity.OAuth2UserIdentity entity = null;

            if (!value.IsDefault())
            {
				entity = new Domain.Entity.OAuth2UserIdentity
				{
					ID = value.ID,
					User = value.User.ToEntity(),
					Identity = value.Identity.ToEntity(),

					__references = value.__references,
					__extensions = value.__extensions
				};
            }

            return entity;
        }


		public static Domain.Entity.OAuth2UserIdentity Merge(Domain.Entity.OAuth2UserIdentity entity, dynamic data)
        {
            Func<ExpandoObject> _data = () => data;

            // Project the properties.
            entity
               .Project(x => x.ID, _data)
               .Project(x => x.User, _data)
               .Project(x => x.Identity, _data)
               .Project(x => x.CreationTimestamp, _data, () => DateTime.Parse(data.CreationTimestamp))
               .Project(x => x.ValidFrom, _data, () => DateTime.Parse(data.ValidFrom))
               .Project(x => x.ValidUntil, _data, () => DateTime.Parse(data.ValidUntil))
                ;

            return entity;
        }


		public static IList<DataTransfer.OAuth2UserIdentity> ToDataTransferSet(this IList<Domain.Entity.OAuth2UserIdentity> entities, params PropertyDirective[] directives)
        {
			IList<DataTransfer.OAuth2UserIdentity> set = entities.AsParallel().Select(entity => entity.ToDataTransferValue(directives)).ToList();
            return set;
        }

		public static IList<Domain.Entity.OAuth2UserIdentity> ToEntitySet(this IList<DataTransfer.OAuth2UserIdentity> values)
        {
			IList<Domain.Entity.OAuth2UserIdentity> set = values.AsParallel().Select(value => value.ToEntity()).ToList();
            return set;
        }

    }
}
