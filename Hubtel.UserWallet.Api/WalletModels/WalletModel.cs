using Hubtel.UserWallet.Api.WalletModels.WalletEnums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hubtel.UserWallet.Api.WalletModels
{
    [Table("user_wallets")]
    [PrimaryKey(nameof(Name),nameof(ID))]
    public class WalletModel : IWalletModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public WalletType Type { get; set; }
        public string AccountNumber { get; set; }
        public WalletScheme AccountScheme { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Owner { get; set; }
    }
}
