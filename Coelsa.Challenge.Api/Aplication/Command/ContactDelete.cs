using Coelsa.Challenge.Api.Data.Repository;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coelsa.Challenge.Api.Aplication.Command
{
    public class ContactDelete
    {

        public class ParamDel : IRequest
        {
            public int Id { get; set; }
        }

        /// <summary>
        /// Clase "EjecutarValidacion" validaciones requeridas sobre la clase "Ejecuta"
        /// </summary>
        public class EjecutarValidacion : AbstractValidator<ParamDel>
        {
            public EjecutarValidacion()
            {
                RuleFor(x => x.Id).NotNull();
                RuleFor(x => x.Id).GreaterThan(0);
            }
        }

        /// <summary>
        /// Clase "Manejador" desarrolla las reglas de negocio
        /// </summary>
        public class Manejador : IRequestHandler<ParamDel>
        {
            private readonly IContactRepository _contactRepository;
            private readonly ILogger<ContactAdd> _logger;
            public Manejador(IContactRepository contactRepository, ILogger<ContactAdd> logger)
            {
                _contactRepository = contactRepository;
                _logger = logger;
            }

            public async Task<Unit> Handle(ParamDel request, CancellationToken cancellationToken)
            {   
                var result = await _contactRepository.DeleteAsync(request.Id);

                if (result > 0) return Unit.Value;
                else throw new Exception($"Ooops! No se pudo eliminar el contacto");
            }
        }


    }
}
