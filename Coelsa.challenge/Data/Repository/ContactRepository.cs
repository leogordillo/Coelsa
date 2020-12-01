using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Coelsa.Challenge.Api.Data.Cnx;
using Coelsa.Challenge.Api.Model;

namespace Coelsa.Challenge.Api.Data.Repository
{
    public class ContactRepository : IContactRepository
    {
        IConnectionFactory _connectionFactory;
        public ContactRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        #region Sync Methods

        public IEnumerable<Contact> GetByCompanyName(string companyName, int pageNumber, int rowsOfPage)
        {
            using (var cnx = _connectionFactory.GetDbConnection)
            {
                var query = "ContactGetByCompany";
                var parameters = new DynamicParameters();
                parameters.Add("companyName", companyName);
                parameters.Add("pageNumber", pageNumber);
                parameters.Add("rowsOfPage", rowsOfPage);
                var result = cnx.Query<Contact>(query, param: parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public int Insert(Contact contact)
        {
            using (var cnx = _connectionFactory.GetDbConnection)
            {
                var query = "ContactInsert";
                var parameters = new DynamicParameters();
                parameters.Add("FirstName", contact.FirstName);
                parameters.Add("LastName", contact.LastName);
                parameters.Add("Email", contact.Email);
                parameters.Add("PhoneNumber", contact.PhoneNumber);

                var result = cnx.Execute(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public int Update(Contact contact)
        {
            using (var cnx = _connectionFactory.GetDbConnection)
            {
                var query = "ContactUpdate";
                var parameters = new DynamicParameters();
                parameters.Add("Id", contact.Id);
                parameters.Add("FirstName", contact.FirstName);
                parameters.Add("LastName", contact.LastName);
                parameters.Add("Company", contact.Company);
                parameters.Add("Email", contact.Email);
                parameters.Add("PhoneNumber", contact.PhoneNumber);

                var result = cnx.Execute(query, param: parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }


        public int Delete(int contactId)
        {
            using (var cnx = _connectionFactory.GetDbConnection)
            {
                var query = "ContactDelete";
                var parameters = new DynamicParameters();
                parameters.Add("Id", contactId);

                var result = cnx.Execute(query, param: parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
        #endregion

        #region Async Methods

        public async Task<IEnumerable<Contact>> GetByCompanyNameAsync(string companyName, int pageNumber, int rowsOfPage)
        {
            using (var cnx = _connectionFactory.GetDbConnection)
            {
                var query = "ContactGetByCompany";
                var parameters = new DynamicParameters();
                parameters.Add("companyName", companyName);
                parameters.Add("pageNumber", pageNumber);
                parameters.Add("rowsOfPage", rowsOfPage);
                var result = await cnx.QueryAsync<Contact>(query, param: parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public async Task<int> InsertAsync(Contact contact)
        {
            using (var cnx = _connectionFactory.GetDbConnection)
            {
                var query = "ContactInsert";
                var parameters = new DynamicParameters();
                parameters.Add("FirstName", contact.FirstName);
                parameters.Add("LastName", contact.LastName);
                parameters.Add("Company", contact.Company);
                parameters.Add("Email", contact.Email);
                parameters.Add("PhoneNumber", contact.PhoneNumber);

                var result = await cnx.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> UpdateAsync(Contact contact)
        {
            using (var cnx = _connectionFactory.GetDbConnection)
            {
                var query = "ContactUpdate";
                var parameters = new DynamicParameters();
                parameters.Add("Id", contact.Id);                
                parameters.Add("FirstName", contact.FirstName);
                parameters.Add("LastName", contact.LastName);
                parameters.Add("Company", contact.Company);
                parameters.Add("Email", contact.Email);
                parameters.Add("PhoneNumber", contact.PhoneNumber);

                var result = await cnx.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }


        public async Task<int> DeleteAsync(int contactId)
        {
            using (var cnx = _connectionFactory.GetDbConnection)
            {
                var query = "ContactDelete";
                var parameters = new DynamicParameters();
                parameters.Add("Id", contactId);

                var result = await cnx.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
        #endregion
    }
}
