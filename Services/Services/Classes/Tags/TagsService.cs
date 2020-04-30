using Domain.Models.Tags;
using Repository.Repository.Classes.Tags;
using Repository.Repository.Interfaces.Tags;
using Services.Services.Interfaces.Tags;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Classes.Tags
{
    public class TagsService : ITagsService
    {
        readonly ITagsRepository tagsRepository;
        public TagsService(ITagsRepository tagsRepository)
        {
            this.tagsRepository = tagsRepository;
        }

        public async Task<int> GetMaxQuestionsCount(IEnumerable<Tag> tags)
        {
            return await tagsRepository.GetMaxQuestionsCount(tags);
        }

        public async Task<IEnumerable<TagCountPair>> GetTagsQuestionsCount()
        {
            return await tagsRepository.GetTagCountPairs();            
        }
    }
}
