using Hubtel.UserWallet.Api.WalletModels.WalletEnums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hubtel.UserWallet.Api.WalletModels
{
    
    public class WalletDataModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string AccountNumber { get; set; }
        public string AccountScheme { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Owner { get; set; }
    }
}
