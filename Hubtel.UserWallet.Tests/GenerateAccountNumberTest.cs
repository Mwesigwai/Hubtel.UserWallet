using FluentAssertions;
using Hubtel.UserWallet.Api.Data;
using Hubtel.UserWallet.Api.ReusableMethods;
using Hubtel.UserWallet.Api.WalletModels.WalletEnums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hubtel.UserWallet.Tests
{
    public class GenerateAccountNumberTest()
    {
        IHelperMethods _helper = new HelperMethods();
    
        [Fact]
        public void HelperMethods_GenerateAccountNumber_returns_6_characters_when_walletType_is_Card()
        {
            //arrange
            var walletType = WalletType.Card;
            string testAccountNumber = "5555555555Test";
            
            //act
            var accountNumber = _helper.GenerateAccountNumber(testAccountNumber, walletType);

            //assert
            accountNumber.Length.Should().Be(6);
        }
    }
}
