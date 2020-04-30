using Domain.Models.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Interfaces.Tags
{
    public interface ITagsRepository
    {
        public Task<IEnumerable<TagCountPair>> GetTagCountPairs();

        public Task<int> GetMaxQuestionsCount(IEnumerable<Tag> tags);
    }
}
