using Domain.Models.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces.Tags
{
    public interface ITagsService
    {
        public Task<IEnumerable<TagCountPair>> GetTagsQuestionsCount();
        public Task<int> GetMaxQuestionsCount(IEnumerable<Tag> tags);
    }
}
