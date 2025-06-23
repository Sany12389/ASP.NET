using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using taskManager.Models;

namespace taskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        // Simulação de um banco de dados em memória
        private static List<Tarefa> tarefas = new List<Tarefa>();
        private static int proximoId = 1;

        [HttpGet]
        public ActionResult<IEnumerable<Tarefa>> Get()
        {
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public ActionResult<Tarefa> Get(int id)
        {
            var tarefa = tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa == null)
                return NotFound();
            return Ok(tarefa);
        }

        [HttpPost]
        public ActionResult<Tarefa> Post([FromBody] Tarefa novaTarefa)
        {
            novaTarefa.Id = proximoId++;
            novaTarefa.DataCriacao = System.DateTime.Now;
            tarefas.Add(novaTarefa);
            return CreatedAtAction(nameof(Get), new { id = novaTarefa.Id }, novaTarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Tarefa tarefaAtualizada)
        {
            var tarefa = tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa == null)
                return NotFound();

            tarefa.Titulo = tarefaAtualizada.Titulo;
            tarefa.Descricao = tarefaAtualizada.Descricao;
            tarefa.Concluida = tarefaAtualizada.Concluida;
            tarefa.DataConclusao = tarefaAtualizada.DataConclusao;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tarefa = tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa == null)
                return NotFound();

            tarefas.Remove(tarefa);
            return NoContent();
        }
    }
}