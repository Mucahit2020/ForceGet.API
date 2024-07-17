using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceGetCase.Core.Models.ComboBox
{
    public interface IComboBoxItem
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}