using Microsoft.AspNetCore.Mvc;
using TaskMasterApi.Context;
using TaskMasterApi.Entities;


namespace TaskMasterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        // GET /Tarefa/{id}
        // Busca uma tarefa específica pelo id
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            // Buscar o Id no banco utilizando o EF
            var tarefa = _context.Tarefas.Find(id);

            // Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            if (tarefa == null)
                return NotFound(); // Retorna 404 se não encontrar
                
            // caso contrário retornar 200 OK com a tarefa encontrada
            return Ok(tarefa);
        }

        // GET /Tarefa/ObterTodos
        // Retorna todas as tarefas cadastradas
        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            // Buscar todas as tarefas no banco utilizando o EF
            var tarefas = _context.Tarefas.ToList();
            return Ok(tarefas);
        }


        // GET /Tarefa/ObterPorTitulo?titulo=exemplo
        // Retorna tarefas cujo título contenha o texto informado
        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            // Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            var tarefas = _context.Tarefas.Where(x => x.Titulo.Contains(titulo)).ToList();
            return Ok(tarefas);
        }


        // GET /Tarefa/ObterPorData?data=2025-08-15
        // Retorna tarefas criadas na data informada
        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date).ToList();
            return Ok(tarefa);
        }


         // GET /Tarefa/ObterPorStatus?status=Concluida
        // Retorna tarefas com o status informado
        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            // Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro            
            var tarefa = _context.Tarefas
                                 .Where(x => x.Status == status)
                                 .ToList();
            return Ok(tarefa);
        }


        // POST /Tarefa
        // Cria uma nova tarefa
        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)

            _context.Tarefas.Add(tarefa); // Adiciona a tarefa ao DbContext
            _context.SaveChanges(); // Salva as alterações no banco
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }


        // PUT /Tarefa/{id}
        // Atualiza uma tarefa existente
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            // Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            _context.Tarefas.Update(tarefaBanco); // Marca como atualizado no EF
            _context.SaveChanges(); // Salva as alterações

            return Ok(tarefaBanco);
        }


        // DELETE /Tarefa/{id}
        // Remove uma tarefa pelo ID
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            // Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            _context.Tarefas.Remove(tarefaBanco); // Remove a tarefa
            _context.SaveChanges(); // Salva as alterações

            return NoContent(); // Retorna 204 sem conteúdo
        }
    }
}
