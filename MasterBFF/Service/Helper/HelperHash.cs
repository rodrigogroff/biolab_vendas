using System.Collections;

namespace Master.Service.Helper
{
    public class HelperHash
    {
        public Hashtable hashUnique = null;

        public bool NotInHash(string myHash, string prefix = "")
        {
            if (myHash == null)
            {
                return false;
            }

            if (hashUnique == null)
            {
                hashUnique = new Hashtable();
            }

            if (hashUnique[prefix + myHash] == null)
            {
                hashUnique[prefix + myHash] = true;
                return true;
            }

            return false;
        }

        public void HashClear()
        {
            if (hashUnique != null)
            {
                hashUnique.Clear();
            }
        }
    }
}
