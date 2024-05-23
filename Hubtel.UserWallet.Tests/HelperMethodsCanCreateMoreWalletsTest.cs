using FluentAssertions;
using FakeItEasy;
using Hubtel.UserWallet.Api.Data;
using Hubtel.UserWallet.Api.ReusableMethods;
using Hubtel.UserWallet.Api.WalletModels;
using Hubtel.UserWallet.Api.WalletServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hubtel.UserWallet.Api.WalletModels.Interfaces;
using System.Runtime.InteropServices;

namespace Hubtel.UserWallet.Tests
{
    
    public class HelperMethodsCanCreateMoreWalletsTest
    {
        DataContext dbcontext;
        public HelperMethodsCanCreateMoreWalletsTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase("testWalletDatabase")
            .Options;
            if (dbcontext is null)
            {
                dbcontext = new DataContext(options);
                dbcontext.Database.EnsureCreated();
            }

            
        }
        [Fact]
        public async Task HelperMethods_CanCreateMoreWallets_returns_false_when_4_are_Created()
        {
            //arrange
            var wallet1 = new WalletPostModel
            {
                AccountNumber = "TestAccountNumber1",
                Name = "MyName",
                WalletType = Api.WalletModels.WalletEnums.WalletType.Momo,
                AccountScheme = Api.WalletModels.WalletEnums.WalletScheme.mtn,
                Owner = "MyPhoneNumber"
            }; var wallet2 = new WalletPostModel
            {
                AccountNumber = "AccountNumber2",
                Name = "MyName",
                WalletType = Api.WalletModels.WalletEnums.WalletType.Card,
                AccountScheme = Api.WalletModels.WalletEnums.WalletScheme.visa,
                Owner = "MyPhoneNumber"
            }; var wallet3 = new WalletPostModel
            {
                AccountNumber = "TestAccountNumber2",
                Name = "MyName",
                WalletType = Api.WalletModels.WalletEnums.WalletType.Momo,
                AccountScheme = Api.WalletModels.WalletEnums.WalletScheme.aireteltigo,
                Owner = "MyPhoneNumber"
            }; var wallet4 = new WalletPostModel
            {
                AccountNumber = "MyAccountNumber4",
                Name = "MyName",
                WalletType = Api.WalletModels.WalletEnums.WalletType.Card,
                AccountScheme = Api.WalletModels.WalletEnums.WalletScheme.mastercard,
                Owner = "MyPhoneNumber"
            }; var wallet5 = new WalletPostModel
            {
                AccountNumber = "FifthAccountNumber",
                Name = "MyName",
                WalletType = Api.WalletModels.WalletEnums.WalletType.Momo,
                AccountScheme = Api.WalletModels.WalletEnums.WalletScheme.mtn,
                Owner = "MyPhoneNumber"
            };
            var helper = new HelperMethods();
            var service = new WalletService(dbcontext,helper);
            bool canCreateMore;
           
            async Task CanCreateWallet()
            {
                canCreateMore = await helper.CanCreateMoreWalletsAsync(dbcontext);
                canCreateMore.Should().Be(true);
            }

            //act 
            await service.CreateAsync(wallet1);
            await CanCreateWallet();

            await service.CreateAsync(wallet2);
            await CanCreateWallet();

            await service.CreateAsync(wallet3);
            await CanCreateWallet();

            await service.CreateAsync(wallet4);

            //assert
            canCreateMore = await helper.CanCreateMoreWalletsAsync(dbcontext);
            canCreateMore.Should().BeFalse();
        }
    }
}
