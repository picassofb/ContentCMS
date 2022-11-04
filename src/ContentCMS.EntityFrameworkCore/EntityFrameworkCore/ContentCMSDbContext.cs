using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ContentCMS.Authorization.Roles;
using ContentCMS.Authorization.Users;
using ContentCMS.Contents;
using ContentCMS.MultiTenancy;

namespace ContentCMS.EntityFrameworkCore
{
    public class ContentCMSDbContext : AbpZeroDbContext<Tenant, Role, User, ContentCMSDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public DbSet<Content> Contents { get; set; }

        public ContentCMSDbContext(DbContextOptions<ContentCMSDbContext> options)
            : base(options)
        {
        }
    }
}
