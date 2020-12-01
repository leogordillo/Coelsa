using Coelsa.Challenge.Api.Model;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coelsa.Challenge.Api.Data.Repository
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetByCompanyName(string companyName, int pageNumber, int rowsOfPage);
        Task<IEnumerable<Contact>> GetByCompanyNameAsync(string companyName, int pageNumber, int rowsOfPage);
        int Delete(int contactId);
        Task<int> DeleteAsync(int contactId);
        int Insert(Contact contact);
        Task<int> InsertAsync(Contact contact);
        int Update(Contact contact);
        Task<int> UpdateAsync(Contact contact);
    }
}