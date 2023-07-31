namespace WebAPI.Entities
{
	public class Contato
	{
		public int Id { get; set; }
		public string Nome { get; set; } = null!;

		public string? Telefone { get; set; }

		public bool Ativo { get; set; }
	}
}