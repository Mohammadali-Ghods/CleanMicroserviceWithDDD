using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetById(string id);
        Task<Customer> GetByMobileOrEmail(string mobilenumber,string email);
        Task<Customer> GetByMobile(string mobilenumber);
        Task<IEnumerable<Customer>> GetAll();
        Task Add(Customer customer);
        Task Update(Customer customer);
        Task Remove(Customer customer);
    }
}