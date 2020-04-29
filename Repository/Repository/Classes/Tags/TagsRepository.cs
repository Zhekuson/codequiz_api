using Domain.Models.Tags;
using Repository.Repository.Interfaces.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Classes.Tags
{
    public class TagsRepository : EntityRepository, ITagsRepository 
    {
        public Task<IEnumerable<TagCountPair>> GetTagCountPairs()
        {
            throw new NotImplementedException();
        }
    }
}
