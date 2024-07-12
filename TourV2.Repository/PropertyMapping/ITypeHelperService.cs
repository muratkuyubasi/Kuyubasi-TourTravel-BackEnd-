namespace TourV2.Repository
{
    public interface ITypeHelperService
    {
        bool TypeHasProperties<T>(string fields);
    }
}
