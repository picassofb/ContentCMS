using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ContentCMS.Configuration;
using ContentCMS.Web;

namespace ContentCMS.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ContentCMSDbContextFactory : IDesignTimeDbContextFactory<ContentCMSDbContext>
    {
        public ContentCMSDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ContentCMSDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            ContentCMSDbContextConfigurer.Configure(builder, configuration.GetConnectionString(ContentCMSConsts.ConnectionStringName));

            return new ContentCMSDbContext(builder.Options);
        }
    }
}
