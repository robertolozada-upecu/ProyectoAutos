using SQLite;

namespace ProyectoAutos.Modelos
{
    [Table("autos")]
    public class Auto : BaseModelo
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }

        [MaxLength(7)]
        public string Placa { get; set; }
    }
}
