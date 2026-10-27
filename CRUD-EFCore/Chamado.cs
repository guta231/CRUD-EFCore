namespace CRUD_EFCore
{
    public class Chamado
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime DataAbertura { get; set; } = DateTime.Now;
        public DateTime? DataFechamento { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public int? TecnicoId { get; set; }
        public Tecnico? Tecnico { get; set; }
        public int? EquipamentoId { get; set; }
        public Equipamento? Equipamento { get; set; }
    }
}
