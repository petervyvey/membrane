
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Membrane.Foundation.DataTransfer;
using Membrane.Foundation.Extension;

namespace Membrane.DataTransfer.Projection
{
    /// <summary>
	/// The <see cref="Identity"/> projection.
    /// </summary>
	public static class IdentityProjection
    {

		public static DataTransfer.Identity ToDataTransferValue(this Domain.Entity.Identity entity, params PropertyDirective[] directives)
        {
			DataTransfer.Identity value =
				new DataTransfer.Identity
                {
                    ID = entity.ID,
					Name = entity.Name,
					FirstName=entity.FirstName,
					Locale = entity.Locale,

                    __references = entity.__references,
                    __extensions = entity.__extensions
                };

            return value;
        }

		public static Domain.Entity.Identity ToEntity(this DataTransfer.Identity value)
        {
			Domain.Entity.Identity entity = null;

            if (!value.IsDefault())
            {
				entity = new Domain.Entity.Identity
                {
                    ID = value.ID,
					Name = value.Name,
					FirstName = value.FirstName,
					Locale = value.Locale,

                    __references = value.__references,
                    __extensions = value.__extensions
                };
            }

            return entity;
        }


		public static Domain.Entity.Identity Merge(Domain.Entity.Identity entity, dynamic data)
        {
            Func<ExpandoObject> _data = () => data;

            // Project the properties.
            entity
               .Project(x => x.ID, _data)
               .Project(x => x.Name, _data)
               .Project(x => x.FirstName, _data)
               .Project(x => x.CreationTimestamp, _data, () => DateTime.Parse(data.CreationTimestamp))
                ;

            return entity;
        }


		public static IList<DataTransfer.Identity> ToDataTransferSet(this IList<Domain.Entity.Identity> entities, params PropertyDirective[] directives)
        {
			IList<DataTransfer.Identity> set = entities.AsParallel().Select(entity => entity.ToDataTransferValue(directives)).ToList();
            return set;
        }

		public static IList<Domain.Entity.Identity> ToEntitySet(this IList<DataTransfer.Identity> values)
        {
			IList<Domain.Entity.Identity> set = values.AsParallel().Select(value => value.ToEntity()).ToList();
            return set;
        }

    }
}
