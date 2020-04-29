using Domain.Models.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodequizApi.Features.Tags
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TagsController:Controller
    {
        readonly ITagsService tagsService;
        public TagsController(ITagsService tagsService)
        {
            this.tagsService = tagsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTagsQuestionsCount()
        {
            return new JsonResult(await tagsService.GetTagsQuestionsCount());
        }
    }
}
