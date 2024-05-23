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

namespace Hubtel.UserWallet.Tests.Tests
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
            var walletObject = new testWalletObjects();
            var helper = new HelperMethods();
            var service = new WalletService(dbcontext, helper);
            bool canCreateMore;

            async Task CanCreateWallet()
            {
                canCreateMore = await helper.CanCreateMoreWalletsAsync(dbcontext);
                canCreateMore.Should().Be(true);
            }

            //act 
            await service.CreateAsync(walletObject.Wallet1);
            await CanCreateWallet();

            await service.CreateAsync(walletObject.Wallet2);
            await CanCreateWallet();

            await service.CreateAsync(walletObject.Wallet3);
            await CanCreateWallet();

            await service.CreateAsync(walletObject.Wallet4);
            canCreateMore = await helper.CanCreateMoreWalletsAsync(dbcontext);

            //assert
            canCreateMore.Should().BeFalse();
        }
    }
}
