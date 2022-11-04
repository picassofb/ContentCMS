using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using ContentCMS.Contents.Dtos;
using Abp.UI;
using Abp.Runtime.Session;

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

        public async Task<ContentDetailDto> InsertOrUpdateCMSContent(UpsertContentInput input)
        {

            var existingEntity = await _eventManager.ContentExistsAsync(input.Id);

            var content = !existingEntity
                ? Content.Create(input.PageName, input.PageContent)
                : Content.Update(input.Id, input.PageName, input.PageContent);

            if (!existingEntity)
            {
                var newId = await _eventManager.CreateAsync(content);
                content.SetId(newId);

                return ObjectMapper.Map<ContentDetailDto>(content);
            }

            await _eventManager.UpdateAsync(content);

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