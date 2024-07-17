using ForceGetCase.Core.Dtos;
using ForceGetCase.DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceGetCase.Core.Services.Abstracts
{
    public interface IOfferService
    {
        Task<Offers> AddOfferAsync(OfferDto offerDto);
        Task<List<Offers>> GetOffersAsync();


    }
}
