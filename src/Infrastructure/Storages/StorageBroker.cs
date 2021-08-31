using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Shop.Brokers.Storages
{
    public partial class Storage : DbContext,IStorage
    {
        private readonly IConfiguration Configuration;
        public Storage(IConfiguration configuration)
        {

            this.Configuration = configuration; 
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            string connectionString = this.Configuration.GetConnectionString("DefaultConnection"); 
            builder.UseSqlServer(connectionString);
        }
    }
}
