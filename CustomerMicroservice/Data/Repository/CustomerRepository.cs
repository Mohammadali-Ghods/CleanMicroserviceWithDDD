using Domain.Interfaces;
using Domain.Models;
using MongoDB.Driver;
using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public async Task<Customer> GetById(string id)
        {
            return await DB.Find<Customer>().OneAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await DB.Find<Customer>().ExecuteAsync();
        }

        public async Task<Customer> GetByEmail(string email)
        {
            return await DB.Find<Customer>().Match(c => c.Email == email).ExecuteFirstAsync();
        }

        public async Task Add(Customer customer)
        {
            await customer.SaveAsync();
        }

        public async Task Update(Customer customer)
        {
            await DB.Update<Customer>()
           .MatchID(customer.ID)
           .ModifyExcept(x => new { x.ID }, customer)
           .ExecuteAsync();
        }

        public async Task Remove(Customer customer)
        {
            await DB.DeleteAsync<Customer>(customer.ID);
        }

        public void Dispose()
        {

        }

        public async Task<Customer> GetByMobileOrEmail(string mobilenumber, string email)
        {
            return await DB.Find<Customer>().Match(c => c.Email == email && c.MobileNumber==mobilenumber).ExecuteFirstAsync();
        }

        public async Task<Customer> GetByMobile(string mobilenumber)
        {
            return await DB.Find<Customer>().Match(c => c.MobileNumber == mobilenumber).ExecuteFirstAsync();
        }
    }
}
