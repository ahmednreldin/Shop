using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Shop.Brokers.Storages
{
    public partial class StorageBroker : DbContext,IStorageBroker
    {
        private readonly IConfiguration Configuration;
        public StorageBroker(IConfiguration configuration)
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
