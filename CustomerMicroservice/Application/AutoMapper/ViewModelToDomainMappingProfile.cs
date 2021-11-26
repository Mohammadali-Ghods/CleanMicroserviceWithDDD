using Application.ViewModel;
using AutoMapper;
using Domain.Commands;

namespace Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<RegisterViewModel, RegisterNewCustomerCommand>()
                .ConstructUsing(c => new RegisterNewCustomerCommand(c.FullName, c.Email,c.MobileNumber, c.BirthDate,c.FirebaseToken));
            CreateMap<CustomerViewModel, UpdateCustomerCommand>()
                .ConstructUsing(c => new UpdateCustomerCommand(c.ID, c.FullName, c.Email, c.BirthDate));
            CreateMap<CustomerViewModel, AddNewAddressCommand>()
               .ConstructUsing(c => new AddNewAddressCommand(c.CustomerAddresses,c.ID));
            CreateMap<VerifyCodeViewModel, VerifyingNewCustomerWithCodeCommand>()
             .ConstructUsing(c => new VerifyingNewCustomerWithCodeCommand(c.MobileNumber,c.Code,c.RefferalInformation.RefferalMobileNumber));
            CreateMap<ConfirmAsRefferalViewModel, ConfirmNewCustomerAsRefferalCommand>()
            .ConstructUsing(c => new ConfirmNewCustomerAsRefferalCommand(c.RefferalInformation.RefferalCustomerID,c.ID));
            CreateMap<LoginViewModel, CustomerLoginCommand>()
           .ConstructUsing(c => new CustomerLoginCommand(c.MobileNumber,""));
            CreateMap<VerifyCodeViewModel, VerifyingRegisteredCustomerWithCode>()
          .ConstructUsing(c => new VerifyingRegisteredCustomerWithCode(c.MobileNumber,c.Code));
        }
    }
}
