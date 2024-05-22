using Hubtel.UserWallet.Api.Data;
using Hubtel.UserWallet.Api.ReturnTypes;
using Hubtel.UserWallet.Api.WalletModels;
using Hubtel.UserWallet.Api.WalletModels.WalletEnums;
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
        public async Task<ActionResult<IEnumerable<WalletDataModel>>> Get()
        {
            var wallets = await _service.GetAllAsync();
            return Ok(wallets);
        }

        [HttpGet("{id}",Name = "GetWallet")]
        public async Task<ActionResult> Get(int id)
        {
            var wallet = await _service.GetItem(id);
            if (wallet is null)
                return BadRequest($"Wallet with id {id} does not exist");
            
            return Ok(wallet);
        }

        /// <summary>
        /// This creates a new wallet
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>
        /// In order to post a wallet, you need to provide its wallet type and Wallet scheme.
        /// 
        /// Note 1: Wallet type is an enum with two values, ie Momo and Card.
        /// Use 
        ///     
        ///     "0" for "Momo" 
        ///     
        ///     "1" for "Card" wallet type
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
        /// 
        /// </remarks>
        ///
        /// <returns>
        /// 
        /// </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<WalletServiceResponse>> Post([FromBody] WalletPostModel value)
        {
            var result = await _service.CreateAsync(value);
            if(result.OperationSuccessful)
            {
                var wallet = await _service.GetItem(_service.GetItem(value.Name).Id);
                return CreatedAtRoute("GetWallet", new { id = _service.GetItem(value.Name).Id }, wallet);
            }
              
            return BadRequest(result.Message);
        }


        [HttpDelete("{name}")]
        public async Task<ActionResult> Delete(string name)
        {
            var result = await _service.RemoveWallet(name);
            return walletResponse(result);
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
