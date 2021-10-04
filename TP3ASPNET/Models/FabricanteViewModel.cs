using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ATASP2021.Models
{
    public class FabricanteViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Remote("Exist", "Fabricante", AdditionalFields = "Id")]
        public string NomeFabricante { get; set; }
        [Required]
        //[StringLength(14)]
        public string Fundador { get; set; }
        public string PaisOrigem { get; set; }

        [DisplayName("Data de fundação")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataFundacao { get; set; }

        public List<ProcessadorViewModel> Processadores { get; set; }
    }
}
