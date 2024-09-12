using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Petalaka.Account.Core.Utils;

namespace Petalaka.Account.Repository.Base;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PetalakaDbContext>
{
    public PetalakaDbContext CreateDbContext(string[] args)
    {
        
        var configuration = CoreHelper.GetRootAppSettings;
        
        var builder = new DbContextOptionsBuilder<PetalakaDbContext>();
        /*
        builder.UseSqlServer("server=account-service.nodfeather.win,1434;database=Petalaka-AccountService;uid=petalaka-accountservice;pwd=PetalakaAccountServiceSecret123;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true;Integrated Security=false;Timeout=30;");
        */
        builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        return new PetalakaDbContext(builder.Options);
    }
}