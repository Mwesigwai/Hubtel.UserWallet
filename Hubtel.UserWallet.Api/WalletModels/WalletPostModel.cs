using Hubtel.UserWallet.Api.WalletModels.WalletEnums;
using System.ComponentModel.DataAnnotations;

namespace Hubtel.UserWallet.Api.WalletModels
{
    public class WalletPostModel : IWalletPostModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public WalletType WalletType { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public WalletScheme AccountScheme { get; set; }
        [Required]
        public string Owner { get; set; }
    }
}
