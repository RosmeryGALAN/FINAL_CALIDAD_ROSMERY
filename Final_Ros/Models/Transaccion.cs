using System;
using System.ComponentModel.DataAnnotations;

namespace Final_Ros.Models
{
    public class Transaccion
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int IdCuenta { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Tipo { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal Monto { get; set; }

    }
}
