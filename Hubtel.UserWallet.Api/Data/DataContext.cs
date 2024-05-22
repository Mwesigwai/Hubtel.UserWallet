using Hubtel.UserWallet.Api.WalletModels;
using Microsoft.EntityFrameworkCore;

namespace Hubtel.UserWallet.Api.Data
{
    public class DataContext
        (DbContextOptions<DataContext> options)
        :DbContext(options)
    {
        public DbSet<WalletDataModel> Wallets { get; set; }
    }
}   
