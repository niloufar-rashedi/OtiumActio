using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OtiumActio
{
    public class ConnectionStringSetting
    {
        public static IConfigurationRoot Configuration;

        public static string GetConnectionString()
        {
            string path = "C:\\Users\\nilou\\MyFolders\\MinaKurser\\UmeåUniversitet\\Uppgifter\\OtiumActio\\OtiumActio\\OtiumActio";
            var builder = new ConfigurationBuilder()
                   //.SetBasePath(Directory.GetCurrentDirectory())
                   .SetBasePath(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, path)))
                   .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            var connectionString = Configuration["ConnectionStrings:OtiumActioDb"];
            return connectionString;
        }
    }
}
