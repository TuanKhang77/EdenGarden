namespace EdenGarden_API.Repository.Interfaces
{
    public interface ICommonRepository
    {
        List<T> GetListBySqlQuery<T>(string sql, object obj = null);
        T GetObjectBySqlQuery<T>(string sql, object obj = null);
        Tuple<IList<T1>, IList<T2>> GetListBySqlQuery<T1, T2>(string sql, object obj = null);

        List<T> GetListByStore<T>(string storeName, object obj = null);
        Tuple<IList<T1>, IList<T2>> GetListByStore<T1, T2>(string storeName, object obj = null);
        T GetObjectByStore<T>(string storeName, object obj);
        bool ExcuteSqlQuery(string sql, object item);
    }
}