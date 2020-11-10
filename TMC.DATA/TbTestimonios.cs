﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMC.DATA
{
    public class TbTestimonios
    {
        public int IDTestimonio { get; set; }

        [Display(Name = "Selección del usuario")]
        public int IDUsuario { get; set; }

        [Required(ErrorMessage = "Testimonio requerido")]
        [Display(Name = "Testimonio del usuario")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string testimonio { get; set; }
    }
}
