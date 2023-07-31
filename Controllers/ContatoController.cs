using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContatoController : ControllerBase
	{
		private readonly AgendaContext _context;

		public ContatoController(AgendaContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> Create(Contato contato)
		{
			await _context.AddAsync(contato);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(ObterContatoPorId), new { id = contato.Id }, contato);
		}

		[HttpGet("RetornarContatos")]
		public async Task<IActionResult> ObterTodosContatos()
		{
			var contatos = await _context.Contatos.ToListAsync();
			if (contatos.Count == 0)
			{
				return NoContent();
			}
			return Ok(contatos);
		}

		[HttpGet("/Nome/{nome}")]
		public async Task<IActionResult> ObterContatosNome(string nome)
		{
			var contatos = await _context.Contatos.Where(c => c.Nome.Contains(nome)).ToListAsync();
			if (contatos.Count == 0)
			{
				return NoContent();
			}
			return Ok(contatos);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> ObterContatoPorId(int id)
		{
			var contato = await _context.Contatos.FindAsync(id);
			if (contato == null)
			{
				return BadRequest("Invalid Id");
			}
			return Ok(contato);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, Contato contato)
		{
			Contato contatoBanco = await _context.Contatos.FindAsync(id) ?? null!;
			if (contato == null)
			{
				return NotFound();
			}
			if (contatoBanco == null)
			{
				return NotFound();
			}
			contatoBanco.Nome = contato.Nome;
			contatoBanco.Telefone = contato.Telefone;
			contatoBanco.Ativo = contato.Ativo;
			_context.Update(contatoBanco);
			await _context.SaveChangesAsync();
			return Ok(contatoBanco);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			Contato contato = _context.Contatos.Find(id) ?? null!;
			if (contato == null)
			{
				return NotFound();
			}
			_context.Remove(contato);
			await _context.SaveChangesAsync();
			return NoContent();
		}
	}
}