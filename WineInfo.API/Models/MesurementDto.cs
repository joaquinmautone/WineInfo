using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WineInfo.API.Models
{
    public class MesurementDto
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public VarietyDto Variety { get; set; }
        public WineTypeDto WineType { get; set; }
        public string Color { get; set; } = string.Empty;
        public int Temperature { get; set; }
        public int Gradation { get; set; }
        public int PH { get; set; }
        public string Observations { get; set; } = string.Empty;
    }
}
