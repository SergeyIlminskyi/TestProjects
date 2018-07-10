using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SWAG.Data
{
    [Serializable]
    public class OperationEntity
        : HistoryEntity, IHistoryEntity, IEntity
    {
        private Double[] _value;

        [Column(Order = 101)]
        [Required]
        public OperationType Type { get; set; }

        [Column(Order = 102)]
        [Required]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public String Serialized
        {
            get
            {
                return (_value?.Length ?? 0) > 0 ? 
                    String.Join(';', _value) : String.Empty;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _value = value.Split(';', StringSplitOptions.RemoveEmptyEntries).Select(v =>
                    {
                        if (Double.TryParse(v, out Double result))
                        {
                            return result;
                        }

                        throw new InvalidCastException();
                    })?.ToArray() ?? new Double[0];
                }
                else
                {
                    _value = new Double[0];
                }
            }
        }

        [NotMapped]
        public Double[] Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value ?? new Double[0];
            }
        }

        [Column(Order = 103)]
        [Required]
        public Double Result { get; set; }

        public OperationEntity()
            : base()
        { }
    }
}
