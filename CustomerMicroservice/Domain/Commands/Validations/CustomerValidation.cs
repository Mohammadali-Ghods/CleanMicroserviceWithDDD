using FluentValidation;
using System;
using System.Collections.Generic;

namespace Domain.Commands.Validations
{
    public abstract class CustomerValidation<T> : AbstractValidator<T> where T : CustomerCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.FullName)
                .NotEmpty().WithMessage("Please ensure you have entered the Name")
                .Length(2, 150).WithMessage("The Name must have between 2 and 150 characters");
        }
        protected void ValidateFirebaseToken()
        {
            RuleFor(c => c.FirebaseToken)
                .NotEmpty().WithMessage("Please ensure you have entered the Firebase Token");
        }

        protected void ValidateRefferalID() 
        {
            RuleFor(c => c.RefferalInformation.RefferalCustomerID)
               .NotEmpty().WithMessage("Please ensure you have entered the refferalid");
        }

        protected void ValidateRefferalMobile()
        {
            RuleFor(c => c.RefferalInformation.RefferalMobileNumber)
               .NotEmpty().WithMessage("Please ensure you have entered the refferal mobile number");
        }

        protected void ValidateBirthDate()
        {
            RuleFor(c => c.BirthDate)
                .NotEmpty()
                .Must(HaveMinimumAge)
                .WithMessage("The customer must have 12 years or more");
        }

        protected void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
        }
        protected void ValidateCode() 
        {
            RuleFor(x => x.Code).NotEmpty().MinimumLength(4).MaximumLength(6);
        }
        protected void ValidateId()
        {
            RuleFor(c => c.ID)
                .NotEqual(String.Empty);
        }
        protected void ValidateMobileNumber() 
        {
            RuleFor(c => c.MobileNumber).NotEmpty().MinimumLength(13).MaximumLength(20);
        }
        protected static bool HaveMinimumAge(DateTime birthDate)
        {
            return birthDate <= DateTime.Now.AddYears(-12);
        }
        protected void ValidateGenderString() 
        {
            List<string> conditions = new List<string>() { "male", "female"};
            RuleFor(x => x.Gender)
              .Must(x => conditions.Contains(x))
              .WithMessage("Please only use: " + String.Join(",", conditions));
        }
        protected void AddressValidationForInsert()
        {
            RuleFor(c => c.CustomerAddresses).ForEach(Address => {
                Address.ChildRules(x => x.RuleFor(x => x.AddressContent).NotEmpty());
                Address.ChildRules(x => x.RuleFor(x => x.Title).NotEmpty());
                Address.ChildRules(x => x.RuleFor(x => x.AddressDirection).NotEmpty());
                Address.ChildRules(x => x.RuleFor(x => x.lat).NotEmpty());
                Address.ChildRules(x => x.RuleFor(x => x.lon).NotEmpty());
                Address.ChildRules(x => x.RuleFor(x => x.Floor).NotEmpty());
                Address.ChildRules(x => x.RuleFor(x => x.Type).NotEmpty());
                Address.ChildRules(x => x.RuleFor(x => x.AddressDirection).NotEmpty());
            });
        }
        protected void AddressValidationForUpdate()
        {
            RuleFor(c => c.CustomerAddresses).ForEach(Address => {
                Address.ChildRules(x => x.RuleFor(x => x.ID).NotEmpty());
                Address.ChildRules(x => x.RuleFor(x => x.AddressContent).NotEmpty());
                Address.ChildRules(x => x.RuleFor(x => x.Title).NotEmpty());
                Address.ChildRules(x => x.RuleFor(x => x.AddressDirection).NotEmpty());
                Address.ChildRules(x => x.RuleFor(x => x.lat).NotEmpty());
                Address.ChildRules(x => x.RuleFor(x => x.lon).NotEmpty());
                Address.ChildRules(x => x.RuleFor(x => x.Floor).NotEmpty());
                Address.ChildRules(x => x.RuleFor(x => x.Type).NotEmpty());
                Address.ChildRules(x => x.RuleFor(x => x.AddressDirection).NotEmpty());
            });
        }
        protected void AddressValidationForDelete()
        {
            RuleFor(c => c.CustomerAddresses).ForEach(Address => {
                Address.ChildRules(x => x.RuleFor(x => x.ID).NotEmpty());
            });
        }
    }

}