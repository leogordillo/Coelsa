using AutoMapper;
using Coelsa.Challenge.Api.Aplication.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coelsa.Challenge.Api.Test
{
    public class MappingTest:Profile
    {
        public MappingTest()
        {
            CreateMap<Coelsa.Challenge.Api.Model.Contact, ContactDto>();
        }
    }
}
