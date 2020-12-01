using Coelsa.Challenge.Api.Data.Repository;
using Coelsa.Challenge.Api.Model;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Coelsa.Challenge.Api.Aplication
{
    public class ContactAdd
    {
        /// <summary>
        /// Clase "Ejecuta" define los parámetros requeridos para 
        /// la cración de un nuevo contacto
        /// </summary>
        public class ParamAdd : IRequest 
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Company { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
        }

        /// <summary>
        /// Clase "EjecutarValidacion" validaciones requeridas sobre la clase "Ejecuta"
        /// </summary>
        public class EjecutarValidacion : AbstractValidator<ParamAdd>
        {
            public EjecutarValidacion() 
            {
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.FirstName).Length(2, 30);

                RuleFor(x => x.LastName).NotEmpty();
                RuleFor(x => x.LastName).Length(2, 30);

                RuleFor(x => x.Company).MaximumLength(75);

                RuleFor(x => x.Email).MaximumLength(75);
                RuleFor(x => x.Email).EmailAddress();

                RuleFor(x => x.PhoneNumber).NotEmpty();
                RuleFor(x => x.PhoneNumber).Length(6, 20);
            }
        }

        /// <summary>
        /// Clase "Manejador" desarrolla las reglas de negocio
        /// </summary>
        public class Manejador : IRequestHandler<ParamAdd> 
        {
            private readonly IContactRepository _contactRepository;            
            public Manejador(IContactRepository contactRepository) 
            {
                _contactRepository = contactRepository;                
            }

            public async Task<Unit> Handle(ParamAdd request, CancellationToken cancellationToken)
            {
                var contacto = new Contact
                {
                    FirstName = string.Join("", request.FirstName.Split("").Select(x => x[0].ToString().ToUpper() + x.Substring(1).ToLower()).ToArray()),
                    LastName = string.Join("", request.LastName.Split("").Select(x => x[0].ToString().ToUpper() + x.Substring(1).ToLower()).ToArray()),
                    Company = string.Join("", request.Company.Split("").Select(x => x[0].ToString().ToUpper() + x.Substring(1).ToLower()).ToArray()),
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber
                };

                var result = await _contactRepository.InsertAsync(contacto);

                if( result > 0) return Unit.Value;
                else throw new Exception($"Ooops! No se pudo registrar el contacto");             
            }
        }

    }
}
