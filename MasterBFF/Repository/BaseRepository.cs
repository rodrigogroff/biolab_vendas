using Npgsql;
using System.Collections;

namespace Master.Repository
{
    public class BaseRepository
    {
        public NpgsqlConnection db;
        public Hashtable cache = null;

        public void EnableCache()
        {
            cache = new Hashtable();
        }

        public void Dispose()
        {
            if (cache != null)
            {
                cache.Clear();
                cache = null;
            }
        }
    }
}
