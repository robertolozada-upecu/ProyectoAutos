using SQLite;

namespace ProyectoAutos.Modelos
{
    public abstract class BaseModelo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
