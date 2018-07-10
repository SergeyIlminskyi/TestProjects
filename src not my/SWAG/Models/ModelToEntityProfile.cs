using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using SWAG.Data;
using System;
using System.Linq;

namespace SWAG.Models
{
    public class ModelToEntityProfile
        : Profile
    {
        public ModelToEntityProfile()
            : base()
        {
            CreateMap<OperationModel, OperationEntity>()
                .HistoryEntryIgnoreMap()
                .ForMember(destination => destination.Serialized, options => options.Ignore())
                .ForMember(destination => destination.Result,
                    options => options.ResolveUsing<ResultResolver>())
                ;
        }
    }

    public class ResultResolver
        : IValueResolver<OperationModel, OperationEntity, Double>
    {
        public Double Resolve(OperationModel source, OperationEntity destination, Double member, ResolutionContext context)
        {
            if ((source.Value?.Length ?? 0) == 0)
            {
                return 0D;
            }

            if (source.Value.Length == 1)
            {
                return source.Value[0];
            }

            switch (source.Type)
            {
                case OperationType.Addition:
                    return source.Value.Sum();

                case OperationType.Subtraction:
                    return source.Value.Aggregate((x, y) => x - y);

                case OperationType.Multiplication:
                    return source.Value.Aggregate((x, y) => x * y);

                case OperationType.Division:
                    if (source.Value.IndexOf(0D) > 0)
                    {
                        throw new DivideByZeroException();
                    }

                    return source.Value.Aggregate((x, y) => x / y);

                case OperationType.Exponentiation:
                    return source.Value.Aggregate((x, y) => Math.Pow(x, y));

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
