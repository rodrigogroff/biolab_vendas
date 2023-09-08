using System;

namespace Master.Entity.Dto.Infra.RegTime
{
    public class RegTimeData
    {
        public string id { get; set; }
        public string label { get; set; }
        public DateTime dtStart { get; set; }
        public DateTime? dtEnd { get; set; }
        public int milis { get; set; }
    }
}
