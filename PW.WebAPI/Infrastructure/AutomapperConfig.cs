using AutoMapper;
using AutoMapper.QueryableExtensions;
using PW.WebAPI.Models;
using PW.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW.WebAPI.Infrastructure
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, UserViewModel>();
                cfg.CreateMap<Transaction, TransactionViewModel>()
                .ForMember(x => x.Credit, opt => opt.Ignore());
            });
            config.AssertConfigurationIsValid();

        }
    }
}