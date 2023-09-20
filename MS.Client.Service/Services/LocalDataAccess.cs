namespace MS.Client.Service
{
    public class LocalDataAccess : ILocalDataAccess
    {
        SQLiteConnection conn = null;
        SQLiteCommand comm = null;
        SQLiteDataAdapter adapter = null;
        SQLiteTransaction trans = null;


        private void Dispose()
        {
            if (trans != null)
            {
                trans.Rollback();
                trans.Dispose();
                trans = null;
            }
            if (adapter != null)
            {
                adapter.Dispose();
                adapter = null;
            }
            if (comm != null)
            {
                comm.Dispose();
                comm = null;
            }
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
        private bool Connection()
        {
            try
            {
                if (conn == null)
                    conn = new SQLiteConnection("data source=data.db");

                conn.Open();

                return true;
            }
            catch
            {
                return false;
            }
        }



        public List<string[]> GetLocalFileInfo()
        {
            if (Connection())
            {
                try
                {
                    string sql = "select file_name,file_md5 from file_version";
                    adapter = new SQLiteDataAdapter(sql, conn);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable.AsEnumerable().Select(d => new string[] { d.Field<string>("file_name"), d.Field<string>("file_md5") }).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    Dispose();
                }
            }

            return null;
        }

        public List<string> GetIcons()
        {
            if (Connection())
            {
                try
                {
                    string sql = "select icon from icons";
                    adapter = new SQLiteDataAdapter(sql, conn);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable.AsEnumerable().Select(d => d.Field<string>("icon")).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    Dispose();
                }
            }
            return null;
        }


        public int GetClientType()
        {
            if (Connection())
            {
                try
                {
                    string sql = "select client_type from settings";
                    comm = new SQLiteCommand(sql, conn);
                    var query = comm.ExecuteScalar();

                    return query == null ? 0 : 1;//int.Parse(query.ToString());
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    Dispose();
                }
            }
            return 0;
        }
    }
}
