using System;
using System.Collections.Generic;

namespace APBD8.Models.DTO
{
    public class SomeSortOfPrescripton
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public IEnumerable<Medicament> Medicaments { get; set; }
    }
}
