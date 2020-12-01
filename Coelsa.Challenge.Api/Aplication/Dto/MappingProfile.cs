using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Coelsa.Challenge.Api.Model;

namespace Coelsa.Challenge.Api.Aplication.Dto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contact, ContactDto>();
        }
    }
}
