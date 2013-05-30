
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Membrane.Foundation.DataTransfer;
using Membrane.Foundation.Extension;

namespace Membrane.DataTransfer.Projection
{
    /// <summary>
	/// The <see cref="DemoValue"/> projection.
    /// </summary>
    public static class DemoValueProjection
    {

        public static DataTransfer.DemoValue ToDataTransferValue(this Domain.Entity.DemoValue entity, params PropertyDirective[] directives)
        {
            DataTransfer.DemoValue value =
                new DataTransfer.DemoValue
                {
                    ID = entity.ID,
                    Code = entity.Code,
                    Description = entity.Description,
                    Value = entity.Value,
                    CreationTimestamp = entity.CreationTimestamp,
                    ValidFrom = entity.ValidFrom,
                    ValidUntil = entity.ValidUntil,

                    __references = entity.__references,
                    __extensions = entity.__extensions
                };

            return value;
        }

        public static Domain.Entity.DemoValue ToEntity(this DataTransfer.DemoValue value)
        {
            Domain.Entity.DemoValue entity = null;

            if (!value.IsDefault())
            {
                entity = new Domain.Entity.DemoValue
                {
                    ID = value.ID,
                    Code = value.Code,
                    Description = value.Description,
                    Value = value.Value,
                    CreationTimestamp = value.CreationTimestamp,
                    ValidFrom = value.ValidFrom,
                    ValidUntil = value.ValidUntil,

                    __references = value.__references,
                    __extensions = value.__extensions
                };
            }

            return entity;
        }


        public static Domain.Entity.DemoValue Merge(Domain.Entity.DemoValue entity, dynamic data)
        {
            Func<ExpandoObject> _data = () => data;

            // Project the properties.
            entity
               .Project(x => x.ID, _data)
               .Project(x => x.Code, _data)
               .Project(x => x.Description, _data)
               .Project(x => x.Value, _data)
               .Project(x => x.CreationTimestamp, _data, () => DateTime.Parse(data.CreationTimestamp))
               .Project(x => x.ValidFrom, _data, () => DateTime.Parse(data.ValidFrom))
               .Project(x => x.ValidUntil, _data, () => DateTime.Parse(data.ValidUntil))

                ;

            return entity;
        }


        public static IList<DataTransfer.DemoValue> ToDataTransferSet(this IList<Domain.Entity.DemoValue> entities, params PropertyDirective[] directives)
        {
            IList<DataTransfer.DemoValue> set = entities.AsParallel().Select(entity => entity.ToDataTransferValue(directives)).ToList();
            return set;
        }

        public static IList<Domain.Entity.DemoValue> ToEntitySet(this IList<DataTransfer.DemoValue> values)
        {
            IList<Domain.Entity.DemoValue> set = values.AsParallel().Select(value => value.ToEntity()).ToList();
            return set;
        }

    }
}
