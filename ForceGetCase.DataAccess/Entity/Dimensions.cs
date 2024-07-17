using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceGetCase.DataAccess.Entity
{
    public class Dimensions
    {
        [Key]
        public int Id { get; set; } 
        public string Type { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }

    }
}
