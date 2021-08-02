using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            //Usado para criar as migrações
            var connectionString = "Data Source=DESKTOP-MNKOETH;Initial Catalog=dbApi;Integrated Security=True";
            //var connectionString = "Server=localhost;Port=3306;Database=dbAPI;Uid=root;Pwd=Mudar@123";
            var optionBuilder = new DbContextOptionsBuilder<MyContext>();
            //optionBuilder.UseMySql(connectionString);
            optionBuilder.UseSqlServer(connectionString);
            return new MyContext(optionBuilder.Options);
        }
    }
}
