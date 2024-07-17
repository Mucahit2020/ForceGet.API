using AutoMapper;
using ForceGetCase.Core.Models.ComboBox;
using ForceGetCase.Core.Services.Abstracts;
using ForceGetCase.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForceGetCase.Core.Services.Concrates
{
    public class ComboBoxService : IComboBoxService
    {
        private readonly ForceGetDbContext _context;
        private readonly IMapper _mapper;

        public ComboBoxService(ForceGetDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ComboBoxBaseModel>> GetModeOptions() =>
            await GetComboBoxDataAsync(_context.Modes);

        public async Task<List<ComboBoxBaseModel>> GetMovementTypes() =>
            await GetComboBoxDataAsync(_context.MovementTypes);

        public async Task<List<ComboBoxBaseModel>> GetIncoterms() =>
            await GetComboBoxDataAsync(_context.Incoterms);

        public async Task<List<ComboBoxBaseModel>> GetCountries() =>
            await GetComboBoxDataAsync(_context.Countries);

        public async Task<List<ComboBoxBaseModel>> GetPackageTypes() =>
            await GetComboBoxDataAsync(_context.PackageTypes);

        public async Task<List<ComboBoxBaseModel>> GetUnit1() =>
            await GetComboBoxDataAsync(_context.Unit1);

        public async Task<List<ComboBoxBaseModel>> GetUnit2() =>
            await GetComboBoxDataAsync(_context.Unit2);

        public async Task<List<ComboBoxBaseModel>> GetCurrency() =>
            await GetComboBoxDataAsync(_context.Currency);

        private async Task<List<ComboBoxBaseModel>> GetComboBoxDataAsync<T>(DbSet<T> dbSet) where T : class
        {
            var data = await dbSet.Select(x => new ComboBoxBaseModel
            {
                Id = EF.Property<int>(x, "Id"),
                Name = EF.Property<string>(x, "Name"),
            }).ToListAsync();

            return _mapper.Map<List<ComboBoxBaseModel>>(data);
        }
    }
}
