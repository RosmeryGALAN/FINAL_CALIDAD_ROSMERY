using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Final_Ros.Models
{
    public class Cuenta
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Categoria { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal Saldo { get; set; }
        public decimal Limite { get; set; }
        public List<Transaccion> Transaccions { get; set; }
    }
}
