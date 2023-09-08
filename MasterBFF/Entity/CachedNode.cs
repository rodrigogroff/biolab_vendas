using System;

namespace Master.Entity
{
    public class CachedNode
    {
        public string tag { get; set; }
        public string route { get; set; }
        public DateTime expires { get; set; }
        public object? input { get; set; }
    }
}
