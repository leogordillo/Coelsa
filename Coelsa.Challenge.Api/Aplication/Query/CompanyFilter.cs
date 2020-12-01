using AutoMapper;
using Coelsa.Challenge.Api.Aplication.Dto;
using Coelsa.Challenge.Api.Data.Repository;
using Coelsa.Challenge.Api.Model;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Coelsa.Challenge.Api.Aplication
{
    public class CompanyFilter
    {
        public CompanyFilter()
        { 
        }

        public class ContactList : IRequest<IEnumerable<ContactDto>>
        {
            public string CompanyName { get; set; }
            public int PageNumber { get; set; }
        }

        /// <summary>
        /// Clase "EjecutarValidacion" validaciones requeridas sobre la clase "ContactList"
        /// </summary>
        public class EjecutarValidacion : AbstractValidator<ContactList>
        {
            public EjecutarValidacion()
            {
                RuleFor(x => x.CompanyName).NotNull();
                RuleFor(x => x.CompanyName).NotEmpty();
                RuleFor(x => x.CompanyName).MaximumLength(75);
                RuleFor(x => x.PageNumber).GreaterThan(0);
                RuleFor(x => x.PageNumber).NotNull();
            }
        }

        public class Manejador : IRequestHandler<ContactList, IEnumerable<ContactDto>>
        {
            private readonly IContactRepository _contactRepository;
            private readonly IMapper _mapper;
            private int rowsOfPage;

            public Manejador(IContactRepository contactRepository, IMapper mapper)
            {
                _contactRepository = contactRepository;
                _mapper = mapper;
                rowsOfPage = 4;
            }
            public async Task<IEnumerable<ContactDto>> Handle(ContactList request, CancellationToken cancellationToken)
            {
                var contacts = await _contactRepository.GetByCompanyNameAsync(request.CompanyName, request.PageNumber, rowsOfPage);
                var contacsDto = _mapper.Map<List<Contact>, List<ContactDto>>(contacts.ToList());
                return  contacsDto;
            }
        }


    }
}
