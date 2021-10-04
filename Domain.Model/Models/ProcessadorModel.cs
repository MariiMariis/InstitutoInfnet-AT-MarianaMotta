using System;
using System.ComponentModel;

namespace Domain.Model.Models
{
    public class ProcessadorModel
    {
        public int Id { get; set; }
        
        public string NomeProcessador { get; set; }

        [DisplayName("Descrição do ítem")]
        public string ItemDescription { get; set; }

        public int Cores { get; set; }

        public int Threads { get; set; }

        [DisplayName("Frequencia de Base")]
        public float BaseFrequency { get; set; }

        public DateTime LaunchDate { get; set; }
        
        public int FabricanteId { get; set; }
        
        public FabricanteModel Fabricante { get; set; }

    }
}

