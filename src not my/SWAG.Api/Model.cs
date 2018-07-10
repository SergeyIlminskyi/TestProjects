using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Reflection;

namespace SWAG
{
    [Serializable]
    public abstract class Model
        : IModel
    {
        [JsonProperty("id", Order = 103)]
        public Guid Id { get; set; }

        [JsonProperty("created", Order = 104, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CreatedOn { get; set; }

        public Model()
            : base()
        {
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                DefaultValueAttribute defaultValueAttr = property.GetCustomAttribute<DefaultValueAttribute>();

                if (defaultValueAttr != null)
                {
                    property.SetValue(this, defaultValueAttr.Value);
                }
            }
        }
    }
}
