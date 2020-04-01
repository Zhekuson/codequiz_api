using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository.Classes
{
    public class TagStatsRepository
    {
        IEnumerable<KeyValuePair<string, double>> GetAllTagsStats()
        {
            KeyValuePair<string, double> pair = new KeyValuePair<string, double>();
            List<KeyValuePair<string, double>> list = new List<KeyValuePair<string, double>>();
            list.Add(pair);
            return list;
        }
    }
}
