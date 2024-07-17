using AutoMapper;
using ForceGetCase.Core.Dtos;
using ForceGetCase.Core.Services.Abstracts;
using ForceGetCase.DataAccess.Context;
using ForceGetCase.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ForceGetCase.Core.Services.Concrates
{
    public class OfferService : IOfferService
    {
        private readonly ForceGetDbContext _context;
        private readonly IMapper _mapper;

        public OfferService(ForceGetDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<Offers>> GetOffersAsync()
        {
            return await _context.Offers.ToListAsync();
        }

        public async Task<Offers> AddOfferAsync(OfferDto offerDto)
        {
            if (offerDto == null)
            {
                throw new ArgumentNullException(nameof(offerDto), "OfferDto cannot be null");
            }

            // Asynchronous data fetching
            var cartonValues = await _context.Dimensions.FirstOrDefaultAsync(x => x.Type == "Cartons");
            var boxesValues = await _context.Dimensions.FirstOrDefaultAsync(x => x.Type == "Boxes");
            var palletsValues = await _context.Dimensions.FirstOrDefaultAsync(x => x.Type == "Pallets");

            if (offerDto.Country == "USA" && offerDto.Unit1 == "CM")
            {
                offerDto.Unit1 = ConvertToInches(offerDto.Quantity, offerDto.Unit1);
                offerDto.Unit2 = ConvertToPounds(offerDto.Quantity, offerDto.Unit2);
            }
            else
            {
                offerDto.Unit1 = $"{offerDto.Quantity} {offerDto.Unit1}";
                offerDto.Unit2 = $"{offerDto.Quantity} {offerDto.Unit2}";
            }

            if (offerDto.PackageType == "Cartons")
            {
                ValidateDimensions(cartonValues, boxesValues, palletsValues);
                double boxesCount = CalculateBoxesCount(offerDto.Quantity, cartonValues, boxesValues);
                double palletsCount = CalculatePalletsCount(boxesCount, boxesValues, palletsValues);
                ValidatePalletsCount(palletsCount, offerDto.Mode);
            }
            else if (offerDto.PackageType == "Boxes")
            {
                ValidateDimensions(boxesValues, palletsValues);
                double palletsCount = CalculatePalletsCountForBoxes(offerDto.Quantity, boxesValues, palletsValues);
                ValidatePalletsCount(palletsCount, offerDto.Mode);
            }
            else if (offerDto.PackageType == "Pallets")
            {
                ValidatePalletsCount(offerDto.Quantity, offerDto.Mode);
            }
            else
            {
                throw new InvalidOperationException("Invalid package type.");
            }

            return await SaveOfferAsync(offerDto);
        }

        private void ValidateDimensions(params Dimensions[] dimensions)
        {
            if (dimensions.Any(d => d == null))
            {
                throw new InvalidOperationException("Dimension data is missing.");
            }
        }

        private double CalculateCartonToBoxesFloor(Dimensions cartonValues, Dimensions boxesValues)
        {
            double widthRatio = boxesValues.Width / cartonValues.Width;
            double lengthRatio = boxesValues.Length / cartonValues.Length;
            double heightRatio = boxesValues.Height / cartonValues.Height;

            return Math.Floor(widthRatio * lengthRatio * heightRatio);
        }

        private double CalculateBoxesToPalletsFloor(Dimensions boxesValues, Dimensions palletsValues)
        {
            double widthRatio = palletsValues.Width / boxesValues.Width;
            double lengthRatio = palletsValues.Length / boxesValues.Length;
            double heightRatio = palletsValues.Height / boxesValues.Height;

            return Math.Floor(widthRatio * lengthRatio * heightRatio);
        }

        private double CalculateBoxesCount(double quantity, Dimensions cartonValues, Dimensions boxesValues)
        {
            double cartonToBoxesFloor = CalculateCartonToBoxesFloor(cartonValues, boxesValues);
            return Math.Floor(quantity / cartonToBoxesFloor);
        }

        private double CalculatePalletsCount(double boxesCount, Dimensions boxesValues, Dimensions palletsValues)
        {
            double boxesToPalletsFloor = CalculateBoxesToPalletsFloor(boxesValues, palletsValues);
            return Math.Floor(boxesCount / boxesToPalletsFloor);
        }

        private double CalculatePalletsCountForBoxes(double quantity, Dimensions boxesValues, Dimensions palletsValues)
        {
            double boxesToPalletsFloor = CalculateBoxesToPalletsFloor(boxesValues, palletsValues);
            return Math.Floor(quantity / boxesToPalletsFloor);
        }

        private void ValidatePalletsCount(double palletsCount, string mode)
        {
            if (palletsCount < 24 && mode != "LCL")
            {
                throw new InvalidOperationException("If pallet count is less than 24, mode should be LCL.");
            }
            if (palletsCount >= 24 && mode == "FCL")
            {
                throw new InvalidOperationException("If pallet count is 24 or more, mode should be FCL.");
            }
        }

        private void ValidatePalletsCount(int quantity, string mode)
        {
            if (quantity < 24 && mode != "LCL")
            {
                throw new InvalidOperationException("If pallet count is less than 24, mode should be LCL.");
            }
            if (quantity >= 24 && mode == "FCL")
            {
                throw new InvalidOperationException("If pallet count is 24 or more, mode should be FCL.");
            }
        }

        private async Task<Offers> SaveOfferAsync(OfferDto offerDto)
        {
            var offer = _mapper.Map<Offers>(offerDto);
            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();
            return offer;
        }

        private string ConvertToInches(double quantity, string unit)
        {
            double conversionFactor = 2.54;
            double inches = quantity * conversionFactor;
            return $"{inches} IN";
        }

        private string ConvertToPounds(double quantity, string unit)
        {
            return $"{quantity} LB";
        }
    }
}
