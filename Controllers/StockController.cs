using System.Linq;
using System.Web.Http;
using WebFORNECEDOR.Models;

namespace WebApplication.Controllers
{
    public class StockController : ApiController
    {
        private readonly AppDbContext db = new AppDbContext();

        // GET api/Stock
        [HttpGet]
        public IHttpActionResult Get()
        {
            var stocks = db.Medicamentos.ToList();
            return Ok(stocks);
        }

        // GET api/Stock/{id}
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var stock = db.Medicamentos.Find(id);
            if (stock == null)
                return NotFound();

            return Ok(stock);
        }

        // POST api/Stock
        [HttpPost]
        public IHttpActionResult Post([FromBody] Medicamento medicamento)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validação adicional para as datas
            if (medicamento.DataProducao == default || medicamento.DataValidade == default)
                return BadRequest("As datas de produção e validade são obrigatórias.");

            if (medicamento.DataValidade <= medicamento.DataProducao)
                return BadRequest("A data de validade deve ser posterior à data de produção.");

            db.Medicamentos.Add(medicamento);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = medicamento.Id }, medicamento);
        }

        // PUT api/Stock/{id}
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Medicamento medicamentoAtualizado)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = db.Medicamentos.Find(id);
            if (stock == null)
                return NotFound();

            // Atualizar os campos relevantes
            stock.Nome = medicamentoAtualizado.Nome;
            stock.Descricao = medicamentoAtualizado.Descricao;
            stock.QuantidadeDisponivel = medicamentoAtualizado.QuantidadeDisponivel;
            stock.Lote = medicamentoAtualizado.Lote;
            stock.DataProducao = medicamentoAtualizado.DataProducao;
            stock.DataValidade = medicamentoAtualizado.DataValidade;

            db.SaveChanges();

            return Ok(stock);
        }

        // DELETE api/Stock/{id}
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var stock = db.Medicamentos.Find(id);
            if (stock == null)
                return NotFound();

            db.Medicamentos.Remove(stock);
            db.SaveChanges();

            return Ok("Medicamento removido com sucesso.");
        }
    }
}
