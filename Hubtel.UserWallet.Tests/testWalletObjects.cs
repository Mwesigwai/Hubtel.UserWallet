using Hubtel.UserWallet.Api.WalletModels;
using Hubtel.UserWallet.Api.WalletModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hubtel.UserWallet.Tests
{
    public class testWalletObjects
    {
        WalletPostModel wallet1 = new WalletPostModel
        {
            AccountNumber = "TestAccountNumber1",
            Name = "MyName",
            WalletType = Api.WalletModels.WalletEnums.WalletType.Momo,
            AccountScheme = Api.WalletModels.WalletEnums.WalletScheme.mtn,
            Owner = "MyPhoneNumber"
        };

        WalletPostModel wallet2 = new WalletPostModel
        {
            AccountNumber = "AccountNumber2",
            Name = "MyName",
            WalletType = Api.WalletModels.WalletEnums.WalletType.Card,
            AccountScheme = Api.WalletModels.WalletEnums.WalletScheme.visa,
            Owner = "MyPhoneNumber"
        };

        WalletPostModel wallet3 = new WalletPostModel
        {
            AccountNumber = "TestAccountNumber2",
            Name = "MyName",
            WalletType = Api.WalletModels.WalletEnums.WalletType.Momo,
            AccountScheme = Api.WalletModels.WalletEnums.WalletScheme.aireteltigo,
            Owner = "MyPhoneNumber"
        };

        WalletPostModel wallet4 = new WalletPostModel
        {
            AccountNumber = "MyAccountNumber4",
            Name = "MyName",
            WalletType = Api.WalletModels.WalletEnums.WalletType.Card,
            AccountScheme = Api.WalletModels.WalletEnums.WalletScheme.mastercard,
            Owner = "MyPhoneNumber"
        };
        public IWalletPostModel Wallet1 { get => wallet1; }
        public IWalletPostModel Wallet2 { get => wallet2; }
        public IWalletPostModel Wallet3 { get => wallet3; }
        public IWalletPostModel Wallet4 { get => wallet4; }
    }
}
