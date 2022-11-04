using Abp.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContentCMS.Contents
{
    public interface IContentManager : IDomainService
    {
        Task<Content> GetAsync(int id);
        Task<IList<Content>> GetAllAsync();
        Task<int> CreateAsync(Content content);
        Task UpdateAsync(Content content);
        Task<bool> ContentExistsAsync(int id);
    }
}