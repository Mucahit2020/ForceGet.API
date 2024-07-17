using AutoMapper;
using ForceGetCase.Core.Dtos;
using ForceGetCase.Core.Models.ComboBox;
using ForceGetCase.DataAccess.Entity;

namespace ForceGetCase.Core.Map
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            CreateMap<ComboBoxBaseModel, Modes>();

        }
    }
}