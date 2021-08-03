using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            //Usado para migrações
            string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
            var optionBuilder = new DbContextOptionsBuilder<MyContext>();

            if (Environment.GetEnvironmentVariable("DATABASE").ToLower() == "sqlserver".ToLower())
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
