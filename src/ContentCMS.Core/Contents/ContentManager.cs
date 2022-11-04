using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.UI;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ContentCMS.Contents
{
    public class ContentManager : IContentManager
    {
        private readonly IRepository<Content> _contentRepository;


        public ContentManager(IRepository<Content> contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public async Task<Content> GetAsync(int id)
        {
            var content = await _contentRepository.FirstOrDefaultAsync(id);

            return content;
        }

        public async Task<IList<Content>> GetAllAsync()
        {
            var items = await _contentRepository
                .GetAll()
                .ToListAsync();

            return items;
        }

        public async Task<int> CreateAsync(Content content)
        {
            return await _contentRepository.InsertAndGetIdAsync(content);
        }

        public async Task UpdateAsync(Content content)
        {
            await _contentRepository.UpdateAsync(content);
        }
    }
}