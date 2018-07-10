using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SWAG
{
    [Serializable]
    public class OperationModel
        : HistoryModel, IHistoryModel, IModel
    {
        [Required]
        [DefaultValue(OperationType.None)]
        [JsonProperty("type", Order = 101)]
        public OperationType Type { get; set; }

        [Required(AllowEmptyStrings = false)]
        [JsonProperty("values", Order = 102)]
        public Double[] Value { get; set; }

        [JsonProperty("result", Order = 103)]
        public Double Result { get; set; }

        public OperationModel()
            : base()
        { }
    }
}
