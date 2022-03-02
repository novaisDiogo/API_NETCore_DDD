using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("launchSettings.json")
             .Build();

            //Usado para migrações
            string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
            string databaseName = Environment.GetEnvironmentVariable("DATABASE");

            if (String.IsNullOrEmpty(connectionString) || String.IsNullOrEmpty(databaseName))
            {
                connectionString = builder["profiles:application:environmentVariables:DB_CONNECTION"];
                databaseName = builder["profiles:application:environmentVariables:DATABASE"];
            }

            var optionBuilder = new DbContextOptionsBuilder<MyContext>();

            if (databaseName.ToLower() == "sqlserver".ToLower())
            {
                optionBuilder.UseSqlServer(connectionString);
            }
            else
            {
                optionBuilder.UseMySql(connectionString);
            }

            return new MyContext(optionBuilder.Options);
        }
    }
}
