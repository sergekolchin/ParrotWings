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
        public static MapperConfiguration Configure()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, UserViewModel>();

                cfg.CreateMap<Transaction, TransactionViewModel>()
                .ForMember(d => d.Credit, opt => opt.Ignore());

                cfg.CreateMap<Transaction, SelectableTransactionViewModel>();
            });
        }
    }
}