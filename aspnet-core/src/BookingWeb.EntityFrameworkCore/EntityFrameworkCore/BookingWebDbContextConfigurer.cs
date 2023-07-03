using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace BookingWeb.EntityFrameworkCore
{
    public static class BookingWebDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<BookingWebDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<BookingWebDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
