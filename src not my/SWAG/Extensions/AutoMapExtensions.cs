using AutoMapper;
using SWAG.Data;

namespace SWAG
{
    public static class AutoMapExtensions
    {
        public static IMappingExpression<TSource, TDestination> EntityIgnoreMap<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression)
            where TSource : IModel
            where TDestination : IEntity
        {
            return mappingExpression
                .ForMember(destination => destination.Id, options => options.Ignore())
                .ForMember(destination => destination.CreatedOn, options => options.Ignore())
                ;
        }

        public static IMappingExpression<TSource, TDestination> HistoryEntryIgnoreMap<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression)
            where TSource : IHistoryModel
            where TDestination : IHistoryEntity
        {
            return mappingExpression
                .EntityIgnoreMap()
                .ForMember(destination => destination.ModifiedOn, options => options.Ignore())
                ;
        }
    }
}
