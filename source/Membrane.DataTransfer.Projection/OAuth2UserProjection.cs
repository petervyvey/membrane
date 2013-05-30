
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Membrane.Foundation.DataTransfer;
using Membrane.Foundation.Extension;

namespace Membrane.DataTransfer.Projection
{
    /// <summary>
	/// The <see cref="OAuth2User"/> projection.
    /// </summary>
	public static class OAuth2UserProjection
    {

		public static DataTransfer.OAuth2User ToDataTransferValue(this Domain.Entity.OAuth2User entity, params PropertyDirective[] directives)
        {
			DataTransfer.OAuth2User value =
				new DataTransfer.OAuth2User
                {
                    ID = entity.ID,
					Identifier = entity.Identifier,
					Scopes = entity.UserScopes.Select(x => x.Scope.Code).ToList(),

                    __references = entity.__references,
                    __extensions = entity.__extensions
                };

            return value;
        }

		public static Domain.Entity.OAuth2User ToEntity(this DataTransfer.OAuth2User value)
        {
			Domain.Entity.OAuth2User entity = null;

            if (!value.IsDefault())
            {
				entity = new Domain.Entity.OAuth2User
				{
					ID = value.ID,
					Identifier = value.Identifier,
					Password = value.Password,

					__references = value.__references,
					__extensions = value.__extensions
				};
            }

            return entity;
        }


		public static Domain.Entity.OAuth2User Merge(Domain.Entity.OAuth2User entity, dynamic data)
        {
            Func<ExpandoObject> _data = () => data;

            // Project the properties.
            entity
               .Project(x => x.ID, _data)
               .Project(x => x.Identifier, _data)
               .Project(x => x.Password, _data)
               .Project(x => x.CreationTimestamp, _data, () => DateTime.Parse(data.CreationTimestamp))
               .Project(x => x.ValidFrom, _data, () => DateTime.Parse(data.ValidFrom))
               .Project(x => x.ValidUntil, _data, () => DateTime.Parse(data.ValidUntil))
                ;

            return entity;
        }


		public static IList<DataTransfer.OAuth2User> ToDataTransferSet(this IList<Domain.Entity.OAuth2User> entities, params PropertyDirective[] directives)
        {
			IList<DataTransfer.OAuth2User> set = entities.AsParallel().Select(entity => entity.ToDataTransferValue(directives)).ToList();
            return set;
        }

		public static IList<Domain.Entity.OAuth2User> ToEntitySet(this IList<DataTransfer.OAuth2User> values)
        {
			IList<Domain.Entity.OAuth2User> set = values.AsParallel().Select(value => value.ToEntity()).ToList();
            return set;
        }

    }
}
