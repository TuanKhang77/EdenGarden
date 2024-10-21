using EdenGarden_API.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace EdenGarden_API.Repository
{
    public class CommonRepository : ICommonRepository
    {
        private string _connectString;
        public CommonRepository(IConfiguration configuration)
        {
            _connectString = configuration.GetConnectionString("DefaultConnection");
            //LogFolderFilePath = configuration["Config:LogFolderFilePath"];

        }
        public CommonRepository(string connectString)
        {
            _connectString = connectString;
        }

        public List<T> GetListBySqlQuery<T>(string sql, object obj = null)
        {
            List<T> items;
            using (var connection = new SqlConnection(_connectString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                try
                {
                    command.CommandType = CommandType.Text;
                    if (obj != null)
                    {

                        foreach (PropertyInfo p in obj.GetType().GetProperties())
                        {
                            var val = p.GetValue(obj, null);
                            command.Parameters.AddWithValue("@" + p.Name, (val == null || val.ToString().Length == 0) ? DBNull.Value : val);
                        }
                    }
                    var reader = command.ExecuteReader();
                    items = reader.MapToList<T>();
                    reader.Dispose();
                    reader.Close();
                }
                catch (SqlException exception)
                {
                    throw;
                }
                finally
                {
                    command.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
            }
            return items;
        }
        public T GetObjectBySqlQuery<T>(string sql, object obj = null)
        {
            var items = new List<T>();
            using (var connection = new SqlConnection(_connectString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                try
                {
                    if (obj != null)
                    {

                        foreach (PropertyInfo p in obj.GetType().GetProperties())
                        {
                            var val = p.GetValue(obj, null);
                            command.Parameters.AddWithValue("@" + p.Name, (val == null || val.ToString().Length == 0) ? DBNull.Value : val);
                        }
                    }
                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader();
                    items = reader.MapToList<T>();

                    reader.Dispose();
                    reader.Close();
                }
                catch (SqlException exception)
                {
                    throw;
                }
                finally
                {
                    command.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
            }
            if (items.Any()) return items.FirstOrDefault();
            return default(T);
        }
        public Tuple<IList<T1>, IList<T2>> GetListBySqlQuery<T1, T2>(string sql, object obj = null)
        {
            Tuple<IList<T1>, IList<T2>> items;
            using (var connection = new SqlConnection(_connectString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                try
                {
                    command.CommandType = CommandType.Text;
                    if (obj != null)
                    {

                        foreach (PropertyInfo p in obj.GetType().GetProperties())
                        {
                            var val = p.GetValue(obj, null);
                            command.Parameters.AddWithValue("@" + p.Name, (val == null || val.ToString().Length == 0) ? DBNull.Value : val);
                        }
                    }
                    var reader = command.ExecuteReader();

                    var instanse1 = reader.MapToList<T1>();
                    reader.NextResult();
                    var instanse2 = reader.MapToList<T2>();

                    reader.Dispose();
                    reader.Close();
                    items = new Tuple<IList<T1>, IList<T2>>(instanse1, instanse2);
                }
                catch (SqlException exception)
                {
                    throw;
                }
                finally
                {
                    command.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
            }
            return items;
        }

        public List<T> GetListByStore<T>(string storeName, object obj = null)
        {
            var items = new List<T>();
            using (var connection = new SqlConnection(_connectString))
            using (var command = new SqlCommand(storeName, connection))
            {
                connection.Open();
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (obj != null)
                    {
                        foreach (PropertyInfo p in obj.GetType().GetProperties())
                        {
                            var val = p.GetValue(obj, null);
                            command.Parameters.AddWithValue("@" + p.Name, (val == null || val.ToString().Length == 0) ? DBNull.Value : val);
                        }
                    }

                    var reader = command.ExecuteReader();
                    items = reader.MapToList<T>();
                    reader.Dispose();
                    reader.Close();
                }
                catch (Exception e)
                {
                    throw;
                }
                finally
                {
                    command.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
            }
            return items;
        }
        public Tuple<IList<T1>, IList<T2>> GetListByStore<T1, T2>(string storeName, object obj = null)
        {
            Tuple<IList<T1>, IList<T2>> items;
            using (var connection = new SqlConnection(_connectString))
            using (var command = new SqlCommand(storeName, connection))
            {
                connection.Open();
                try
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (obj != null)
                    {
                        foreach (PropertyInfo p in obj.GetType().GetProperties())
                        {
                            var val = p.GetValue(obj, null);
                            command.Parameters.AddWithValue("@" + p.Name, (val == null || val.ToString().Length == 0) ? DBNull.Value : val);
                        }
                    }
                    var reader = command.ExecuteReader();

                    var instanse1 = reader.MapToList<T1>();
                    reader.NextResult();
                    var instanse2 = reader.MapToList<T2>();

                    reader.Dispose();
                    reader.Close();
                    items = new Tuple<IList<T1>, IList<T2>>(instanse1, instanse2);

                }
                catch (Exception e)
                {
                    throw;
                }
                finally
                {
                    command.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
            }
            return items;
        }
        public T GetObjectByStore<T>(string storeName, object obj)
        {
            var items = GetListByStore<T>(storeName, obj);
            return items.Any() ? items.FirstOrDefault() : default(T);
        }

        public bool ExcuteSqlQuery(string sql, object obj)
        {
            int rowsAffected = 0;
            using (var connection = new SqlConnection(_connectString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                try
                {
                    command.CommandType = CommandType.Text;
                    foreach (PropertyInfo p in obj.GetType().GetProperties())
                    {
                        var val = p.GetValue(obj, null);
                        command.Parameters.AddWithValue("@" + p.Name, (val == null || val.ToString().Length == 0) ? DBNull.Value : val);
                    }
                    rowsAffected = command.ExecuteNonQuery();

                }
                catch (SqlException exception)
                {
                    throw;
                }
                finally
                {
                    command.Dispose();
                    connection.Dispose();
                    connection.Close();
                }
            }
            return rowsAffected != 0;
        }

    }
}