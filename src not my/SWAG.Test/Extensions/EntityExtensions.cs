using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace SWAG.Test
{
    public static class EntityExtensions
    {
        public static StringContent ToStringContent(this IModel entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return new StringContent(JsonConvert.SerializeObject(entity),
                Encoding.UTF8, "application/json");
        }

        public static StringContent ToStringContent(this IEnumerable<IModel> entities)
        {
            if ((entities?.Count() ?? 0) == 0)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            return new StringContent(JsonConvert.SerializeObject(entities),
                Encoding.UTF8, "application/json");
        }
    }
}
