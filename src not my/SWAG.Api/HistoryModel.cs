using Newtonsoft.Json;
using System;

namespace SWAG
{
    [Serializable]
    public abstract class HistoryModel
        : Model, IHistoryModel, IModel
    {
        [JsonProperty("modified", Order = 150, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ModifiedOn { get; set; }

        public HistoryModel()
            : base()
        { }
    }
}
