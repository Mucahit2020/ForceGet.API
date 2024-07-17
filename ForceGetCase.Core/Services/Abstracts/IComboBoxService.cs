using ForceGetCase.Core.Models.ComboBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceGetCase.Core.Services.Abstracts
{
    public interface IComboBoxService
    {
        Task<List<ComboBoxBaseModel>> GetModeOptions();
        Task<List<ComboBoxBaseModel>> GetMovementTypes();
        Task<List<ComboBoxBaseModel>> GetIncoterms();
        Task<List<ComboBoxBaseModel>> GetCountries();
        Task<List<ComboBoxBaseModel>> GetPackageTypes();
        Task<List<ComboBoxBaseModel>> GetUnit1();
        Task<List<ComboBoxBaseModel>> GetUnit2();
        Task<List<ComboBoxBaseModel>> GetCurrency();



    }
}
