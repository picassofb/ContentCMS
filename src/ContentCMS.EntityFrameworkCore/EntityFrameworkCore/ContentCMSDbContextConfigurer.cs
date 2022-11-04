using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ContentCMS.EntityFrameworkCore
{
    public static class ContentCMSDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ContentCMSDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ContentCMSDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
