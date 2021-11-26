using Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerAppService : IDisposable
    {
        Task<IEnumerable<CustomerViewModel>> GetAll();
        Task<CustomerViewModel> GetById(string id);
        Task<ResultModel> Register(RegisterViewModel customerViewModel);
        Task<ResultModel> Update(CustomerViewModel customerViewModel);
        Task<ResultModel> Remove(string id);
        Task<ResultModel> VerifyingCustomerWithCode(VerifyCodeViewModel customerViewModel);
        Task<ResultModel> ConfirmCustomerAsRefferal(ConfirmAsRefferalViewModel customerViewModel);
        Task<ResultModel> Login(LoginViewModel customerViewModel);
        Task<ResultModel> VerifyingRegisteredCustomerWithCode(VerifyCodeViewModel customerViewModel);

    }
}
