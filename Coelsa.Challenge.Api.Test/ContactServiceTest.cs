using AutoMapper;
using Coelsa.Challenge.Api.Aplication;
using Coelsa.Challenge.Api.Aplication.Dto;
using Coelsa.Challenge.Api.Model;
using Coelsa.Challenge.Api.Data.Repository;
using GenFu;
using Moq;
using Swashbuckle.Swagger;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using System.Data;
using Moq.Dapper;
using Dapper;
using Swashbuckle.SwaggerUi;
using System.Threading.Tasks;

namespace Coelsa.Challenge.Api.Test
{
    public class ContactServiceTest
    {
        private async Task<IEnumerable<Coelsa.Challenge.Api.Model.Contact>> GetData(string companyName,int page, int pageSize)
        {
            A.Configure<Coelsa.Challenge.Api.Model.Contact>()
                .Fill(x => x.FirstName).AsFirstName()
                .Fill(x => x.LastName).AsLastName()
                .Fill(x => x.Company, new string("Coelsa"))
                .Fill(x => x.Email).AsEmailAddress()
                .Fill(x => x.Id,()=> { return new int(); });

            var list = A.ListOf<Coelsa.Challenge.Api.Model.Contact>(30);
            return list.Skip(page * pageSize).Take(pageSize).ToList();
        }

        [Fact]
        public async void GetContactByCompany ()
        {
            var mockRepository = new Mock<IContactRepository>();
            var mapConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingTest()));

            var mapper = mapConfig.CreateMapper();

            CompanyFilter.ContactList request = new CompanyFilter.ContactList();
            request.CompanyName = "Coelsa";
            request.PageNumber = 1;
            int pageSize = 4;

            mockRepository.Setup(r => r.GetByCompanyNameAsync(request.CompanyName, request.PageNumber, 4)).Returns(GetData(request.CompanyName, request.PageNumber,pageSize));
            CompanyFilter.Manejador manejador = new CompanyFilter.Manejador(mockRepository.Object, mapper);

            var list = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(list);
            Assert.True(list.ToList().Count() == 4);
            Assert.True(list.ToList()[0].Company == "Coelsa");

        }
    }
}
