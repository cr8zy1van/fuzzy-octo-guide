using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MusicApi.DataModel;

namespace MusicApi
{
    public class MusicDataContextFactory : IDesignTimeDbContextFactory<MusicDataContext>
    {
        public MusicDataContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            DbContextOptionsBuilder<MusicDataContext> builder = new DbContextOptionsBuilder<MusicDataContext>();
            string connectionStr = configuration.GetConnectionString("MusicData");
            builder.UseSqlite(connectionStr, options => options.CommandTimeout((int)TimeSpan.FromMinutes(2).TotalSeconds));

            return new MusicDataContext(builder.Options);
        }

        public MusicDataContext CreateDbContext() => CreateDbContext(new string[0]);
    }
}