using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ATASP2021.Models
{
    public class ProcessadorViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Remote("Exist", "Aluno", "Id")]
        public string NomeProcessador { get; set; }

        [Required]
        [Display(Name = "Descrição do ítem")]
        public string ItemDescription { get; set; }

        public int Cores { get; set; }
        public int Threads { get; set; }

        [DisplayName("Frequencia de Base")]
        public float BaseFrequency { get; set; }
        [Required]

        public DateTime LaunchDate { get; set; }

        public int FabricanteId { get; set; }
        public FabricanteViewModel Fabricante { get; set; }
    }
}
