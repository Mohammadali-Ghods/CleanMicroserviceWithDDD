using Application.Interfaces;
using Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.Identity.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ICustomerAppService _customerAppService;

        public CustomerController(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        [HttpGet("customer")]
        [AllowAnonymous]
        public async Task<IEnumerable<CustomerViewModel>> Get()
        {
            return await _customerAppService.GetAll();
        }

        [HttpGet("customer/{id:guid}")]
        [AllowAnonymous]
        public async Task<CustomerViewModel> Get(string id)
        {
            return await _customerAppService.GetById(id);
        }

        [HttpPost]
        [Route("registernewcustomer")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] RegisterViewModel registerViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _customerAppService.Register(registerViewModel));
        }

        [HttpPost]
        [Route("checkregisteringcustomerverificationcode")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckRegisteringCustomerVerificationCode([FromBody] VerifyCodeViewModel verifyCodeViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _customerAppService.VerifyingCustomerWithCode(verifyCodeViewModel));
        }

        [HttpPost]
        [Route("confirmnewcustomerasrefferal")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmNewCustomerAsRefferal([FromBody] ConfirmAsRefferalViewModel confirmAsRefferalViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _customerAppService.ConfirmCustomerAsRefferal(confirmAsRefferalViewModel));
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> login([FromBody] LoginViewModel LoginViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _customerAppService.Login(LoginViewModel));
        }

        [HttpPost]
        [Route("checkregisteredcustomerverificationcode")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckRegisteredCustomerVerificationCode([FromBody] VerifyCodeViewModel verifyCodeViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _customerAppService.VerifyingRegisteredCustomerWithCode(verifyCodeViewModel));
        }

        [HttpPut("customer")]
        [AllowAnonymous]
        public async Task<IActionResult> Put([FromBody] CustomerViewModel customerViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _customerAppService.Update(customerViewModel));
        }

        [HttpDelete("customer")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(string id)
        {
            return CustomResponse(await _customerAppService.Remove(id));
        }

    }
}
