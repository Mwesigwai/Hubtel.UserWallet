using Hubtel.UserWallet.Api.ReturnTypes;
using Hubtel.UserWallet.Api.WalletModels;
using Hubtel.UserWallet.Api.WalletModels.WalletEnums;
using Hubtel.UserWallet.Api.WalletServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hubtel.UserWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController(IWalletService service) : ControllerBase
    {
        IWalletService _service = service;
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<WalletDataModel>>> Get()
        {
            var wallets = await _service.GetAllAsync();
            return Ok(wallets);
        }

        [HttpGet("{id}",Name = "GetWallet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Get(int id)
        {
            var wallet = await _service.GetWallet(id);
            if (wallet is null)
                return BadRequest($"Wallet with id {id} does not exist");
            
            return Ok(wallet);
        }

        /// <summary>
        /// This creates a new wallet
        /// </summary>
        /// <param id="value"></param>
        /// <remarks>
        /// In order to post a wallet, you need to provide its wallet type and Wallet scheme.
        /// 
        /// Note 1: Wallet type is an enum with two values, ie Momo and Card.
        /// Use 
        ///     
        ///     "0" for "Momo" 
        ///     
        ///     "1" for "Card" 
        /// 
        /// Note 2: For wallet scheme, use:-
        ///        
        ///     0 for Visa,
        ///         
        ///     1 for mastercard
        ///         
        ///     2 for mtn
        ///        
        ///     3 for vodafone
        ///       
        ///     4 for airteltigo
        ///         
        /// Note 3:
        /// 
        /// After a successfull post, an object with the id of the wallet you created is returned.
        /// 
        /// You may use this to view the wallet in particular
        /// 
        /// </remarks>
        ///<returns>
        /// 
        /// </returns>
        /// 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<WalletServiceResponse>> Post([FromBody] WalletPostModel value)
        {
            try
            {
                var result = await _service.CreateAsync(value);
                if (result.OperationSuccessful)
                {
                    var id = _service.GetWallet(value.Name).Id;
                    var walletSummery = new { walletId = id };
                    return Ok(walletSummery);
                }

                return BadRequest(result.Message);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _service.RemoveWallet(id);
            return walletResponse((WalletServiceResponse)result);
        }

        
        
        
        private ActionResult walletResponse(WalletServiceResponse response)
        {
            return
            response.OperationSuccessful is true ?
                   Ok(response.Message)
                   : BadRequest(response.Message);
        }

    }
}
