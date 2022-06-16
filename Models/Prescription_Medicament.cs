using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD8.Models
{
    public class Prescription_Medicament
    {
        public int Dose { get; set; }
        [Required]
        [MaxLength(100)]
        public string Details { get; set; }
        public int IdMedicament { get; set; }
        public int IdPrescription { get; set; }
        [ForeignKey("IdMedicament")]
        public virtual Medicament Medicament { get; set; }
        [ForeignKey("IdPrescription")]
        public virtual Prescription Prescription { get; set; }
    }
}
