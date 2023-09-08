using System;

namespace Master.Entity
{
    public class LocalNetwork
    {
        public const string Secret = "ciOiJIUzI1NiIsInR5cCI6IeyJ1bmlxdxxxWVfbmFtZSI6IjEiLCJuYmYiOjE1NTc5Mjk4ODcsImV4cCI6MTU1fhdsjhfeuyrejhdfj7333001";
        public string api { get; set; }
        public string database { get; set; }
        public string cacheLocation { get; set; }
        public int port { get; set; }
        public int node { get; set; }
    }
}
