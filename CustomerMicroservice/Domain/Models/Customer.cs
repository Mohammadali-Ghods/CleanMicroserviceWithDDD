using Domain.ValueObject;
using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Models
{
    public class Customer : Entity
    {
        public Customer(string id, string fullname, string email,
            string mobilenumber, DateTime birthDate, GenderType gender
            , string code,string firebasetoken)
        {
            ID = id;
            FullName = fullname;
            Email = email;
            BirthDate = birthDate;
            MobileNumber = mobilenumber;
            Gender = gender;
            CreatedDate = DateTime.Now;
            LastCode = new Code(code);
            LastStatus = new LastStatus(Status.WaitingForMobileVerification);
            CustomerAddresses = new List<Address>();
            CustomerBankCards = new List<BankCard>();
            InvitationCustomersList = new List<Invitation>();
            FirebaseToken = firebasetoken;
            RefferalInformationList = new List<RefferalInformation>();
        }

        // Empty constructor for EF
        protected Customer() { }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string FirebaseToken { get; private set; }
        public string MobileNumber { get; private set; }
        public string ImageName { get; private set; }
        public GenderType Gender { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string LastMobileToken { get; private set; }
        public LastStatus LastStatus { get; private set; }
        public List<RefferalInformation> RefferalInformationList { get; private set; }
        public Code LastCode { get; private set; }
        public List<Address> CustomerAddresses { get; private set; }
        public List<BankCard> CustomerBankCards { get; private set; }
        public List<Invitation> InvitationCustomersList { get; private set; }
        public void UpdateMobileToken(string NewMobileToken) => LastMobileToken = NewMobileToken;
        public void UpdateImageName(string NewImageName) => ImageName = NewImageName;
        public void WaitingForRefferalStatus() => LastStatus = new LastStatus(Status.WaitingForRefferal);
        public void RegisterationSucceedStatus() => LastStatus = new LastStatus(Status.RegisterationSucceed);
        public void RegisterationFailedStatus() => LastStatus = new LastStatus(Status.RegisterationFailed);
        public void DisabledBySecurityReasonStatus() => LastStatus = new LastStatus(Status.DisabledBySecurityReason);
        public void DisabledByAdminStatus() => LastStatus = new LastStatus(Status.DisabledByAdmin);
        public void DeletedWithUserRequestStatus() => LastStatus = new LastStatus(Status.DeletedWithUserRequest);
        public void AutoLogoutForSecurityStatus() => LastStatus = new LastStatus(Status.AutoLoggedOutForSecurityReason);
        public void AddAddress(Address newaddress) => CustomerAddresses.Add(newaddress);
        public void DeleteAddress(string ID) => CustomerAddresses.RemoveAll(x => x.ID == ID);
        public void UpdateAddress(Address updatedaddress)
        {
            DeleteAddress(updatedaddress.ID);
            CustomerAddresses.Add(updatedaddress);
        }
        public void AddBankCard(BankCard newbankcard) => CustomerBankCards.Add(newbankcard);
        public void DeleteBankCard(string ID) => CustomerBankCards.RemoveAll(x => x.ID == ID);
        public void UpdateBankCard(BankCard updatedbankcard)
        {
            DeleteBankCard(updatedbankcard.ID);
            CustomerBankCards.Add(updatedbankcard);
        }
        public void AddInvitation(Invitation newinvitation) => InvitationCustomersList.Add(newinvitation);
        public void DeleteInvitation(string customerid) => InvitationCustomersList.RemoveAll(x => x.CustomerID == customerid);
        public void UpdateInvitation(Invitation updatedinvitation)
        {
            DeleteInvitation(updatedinvitation.CustomerID);
            InvitationCustomersList.Add(updatedinvitation);
        }

        public void AddRefferal(RefferalInformation newrefferal) => RefferalInformationList.Add(newrefferal);
        public void UpdateRefferal(RefferalInformation refferalinformation) 
        {
            DeleteRefferal(refferalinformation.RefferalCustomerID);
            RefferalInformationList.Add(refferalinformation);
        }
        public void UpdateCode(string newcode) 
        {
            LastCode = new Code(newcode);
        }
        public void DeleteRefferal(string refferalcustomerid) => RefferalInformationList.RemoveAll(x => x.RefferalCustomerID == refferalcustomerid);

    }
}