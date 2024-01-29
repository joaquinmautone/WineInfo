using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WineInfo.Entities
{
    public class Mesurement
    {
        public Mesurement() { }

        public Mesurement(int id, int year, Variety variety, WineType wineType, string color, int temperature, int gradation, int ph, string observations)
        {
            Id = id;
            Year = year;
            Variety = variety;
            WineType = wineType;
            Color = color;
            Temperature = temperature;
            Gradation = gradation;
            PH = ph;
            Observations = observations;
        }

        public int Id { get; set; }
        public int Year { get; set; }
        public Variety Variety { get; set; }
        public WineType WineType { get; set; }
        public string Color { get; set; } = string.Empty;
        public int Temperature { get; set; }
        public int Gradation { get; set; }
        public int PH { get; set; }
        public string Observations { get; set; } = string.Empty;
    }
}
