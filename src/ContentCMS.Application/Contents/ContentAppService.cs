using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using ContentCMS.Contents.Dtos;
using Abp.UI;

namespace ContentCMS.Contents
{
    public class ContentAppService : ContentCMSAppServiceBase, IContentAppService
    {
        private readonly IContentManager _eventManager;

        public ContentAppService(IContentManager eventManager)
        {
            _eventManager = eventManager;
        }

        public async Task<ContentDetailDto> GetCMSContent(int id)
        {
            var content = await _eventManager.GetAsync(id);

            if (content == null)
            {
                throw new UserFriendlyException("Content does not exists!");
            }

            return ObjectMapper.Map<ContentDetailDto>(content);
        }

        public async Task<ContentDetailDto> InsertOrUpdateCMSContent(CreateContentInput input)
        {

            var existingEntity = await _eventManager.GetAsync(input.Id);

            var content = existingEntity != null
                ? ObjectMapper.Map(input, existingEntity)
                : ObjectMapper.Map<Content>(input);

            if (existingEntity == null)
            {
                var newId = await _eventManager.CreateAsync(content);
                content.SetId(newId);
            }
            else
            {
                await _eventManager.UpdateAsync(content);
            }

            return ObjectMapper.Map<ContentDetailDto>(content);
        }


        public async Task<ListResultDto<ContentDetailDto>> GetAll()
        {

            var items = (await _eventManager
                    .GetAllAsync())
                    .OrderBy(x => x.PageName);

            return new ListResultDto<ContentDetailDto>
            {
                Items = ObjectMapper.Map<List<ContentDetailDto>>(items)
            };
        }
    }
}