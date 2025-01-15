/*using System;
using System.Linq;
using System.Web.Http;
using System.Net;
using WebFORNECEDOR.Models;
using System.Data.Entity.Infrastructure;

namespace WebFORNECEDOR.Controllers
{
    public class PedidoController : ApiController
    {
        private readonly AppDbContext db = new AppDbContext();

        // POST: api/pedido/processar
        [HttpPost]
        [Route("api/pedido/processar")]
        public IHttpActionResult ProcessarPedido([FromBody] Pedido pedido)
        {
            try
            {
                if (pedido == null)
                {
                    return BadRequest("O pedido não pode ser nulo.");
                }

                if (pedido.QuantidadeSolicitada <= 0)
                {
                    return BadRequest("A quantidade solicitada deve ser maior que zero.");
                }

                var fornecedor = db.Fornecedor.Find(pedido.FornecedorId);
                if (fornecedor == null)
                {
                    return BadRequest($"Fornecedor com ID {pedido.FornecedorId} não encontrado.");
                }

                var medicamento = db.Medicamentos.Find(pedido.MedicamentoId);
                if (medicamento == null)
                {
                    return BadRequest($"Medicamento com ID {pedido.MedicamentoId} não encontrado.");
                }

                if (medicamento.QuantidadeDisponivel >= pedido.QuantidadeSolicitada)
                {
                    medicamento.QuantidadeDisponivel -= pedido.QuantidadeSolicitada;
                    pedido.Estado = "Aprovado";
                    pedido.QuantidadeAprovada = pedido.QuantidadeSolicitada;
                    pedido.MensagemResposta = "Pedido aprovado com sucesso.";
                }
                else
                {
                    pedido.Estado = "Negado";
                    pedido.QuantidadeAprovada = 0;
                    pedido.MensagemResposta = "Estoque insuficiente. Pedido negado.";
                }

                db.Pedidos.Add(pedido);
                db.SaveChanges();

                return Ok(new { mensagem = "Pedido processado com sucesso.", pedido });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new
                {
                    mensagem = "Erro interno ao processar o pedido.",
                    detalhes = ex.Message
                });
            }
        }

        // PUT: api/pedido/{id}/responder
        [HttpPut]
        [Route("api/pedido/{id}/responder")]
        public IHttpActionResult ResponderPedido(int id, [FromBody] PedidoResposta resposta)
        {
            try
            {
                var pedido = db.Pedidos.Find(id);
                if (pedido == null)
                {
                    return NotFound();
                }

                var medicamento = db.Medicamentos.Find(pedido.MedicamentoId);
                if (medicamento == null)
                {
                    return BadRequest($"Medicamento com ID {pedido.MedicamentoId} não encontrado.");
                }

                if (resposta.Aprovado && medicamento.QuantidadeDisponivel >= resposta.QuantidadeAprovada)
                {
                    medicamento.QuantidadeDisponivel -= resposta.QuantidadeAprovada;
                    pedido.Estado = "Aprovado";
                    pedido.QuantidadeAprovada = resposta.QuantidadeAprovada;
                    pedido.MensagemResposta = resposta.Mensagem ?? "Pedido aprovado com sucesso.";
                }
                else
                {
                    pedido.Estado = "Negado";
                    pedido.QuantidadeAprovada = 0;
                    pedido.MensagemResposta = "Estoque insuficiente. Pedido negado.";
                }

                db.SaveChanges();

                return Ok(new { mensagem = "Resposta registrada com sucesso.", pedido });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new
                {
                    mensagem = "Erro ao processar a resposta do pedido.",
                    detalhes = ex.Message
                });
            }
        }

        // GET: api/pedido/{id}
        [HttpGet]
        [Route("api/pedido/{id}")]
        public IHttpActionResult ObterPedidoPorId(int id)
        {
            try
            {
                var pedido = db.Pedidos.Find(id);
                if (pedido == null)
                {
                    return NotFound();
                }

                return Ok(new { pedido.Id, pedido.Estado, pedido.MensagemResposta, pedido.QuantidadeAprovada });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new
                {
                    mensagem = "Erro ao carregar o pedido.",
                    detalhes = ex.Message
                });
            }
        }

        // GET: api/pedido
        [HttpGet]
        [Route("api/pedido")]
        public IHttpActionResult ObterTodosPedidos()
        {
            try
            {
                var pedidos = db.Pedidos.Select(p => new
                {
                    p.Id,
                    p.Estado,
                    p.MensagemResposta,
                    p.QuantidadeAprovada
                }).ToList();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new
                {
                    mensagem = "Erro ao carregar os pedidos.",
                    detalhes = ex.Message
                });
            }
        }
    }
}
*/
using System;
using System.Linq;
using System.Web.Http;
using System.Net;
using WebFORNECEDOR.Models;
using System.Data.Entity.Infrastructure;

namespace WebFORNECEDOR.Controllers
{
    public class PedidoController : ApiController
    {
        private readonly AppDbContext db = new AppDbContext();

        // POST: api/pedido/processar
        [HttpPost]
        [Route("api/pedido/processar")]
        public IHttpActionResult ProcessarPedido([FromBody] Pedido pedido)
        {
            try
            {
                // Valida o pedido
                if (pedido == null || pedido.QuantidadeSolicitada <= 0)
                {
                    return BadRequest("Pedido inválido. Verifique os dados.");
                }

                // Verifica se o fornecedor existe
                var fornecedor = db.Fornecedor.Find(pedido.FornecedorId);
                if (fornecedor == null)
                {
                    return BadRequest($"Fornecedor com ID {pedido.FornecedorId} não encontrado.");
                }

                // Verifica se o medicamento existe
                var medicamento = db.Medicamentos.Find(pedido.MedicamentoId);
                if (medicamento == null)
                {
                    return BadRequest($"Medicamento com ID {pedido.MedicamentoId} não encontrado.");
                }

                // Processa o pedido com base na quantidade disponível do medicamento
                if (medicamento.QuantidadeDisponivel >= pedido.QuantidadeSolicitada)
                {
                    medicamento.QuantidadeDisponivel -= pedido.QuantidadeSolicitada;
                    pedido.Estado = "Aprovado";
                    pedido.QuantidadeAprovada = pedido.QuantidadeSolicitada;
                    pedido.MensagemResposta = "Pedido aprovado com sucesso.";
                }
                else
                {
                    pedido.Estado = "Negado";
                    pedido.QuantidadeAprovada = 0;
                    pedido.MensagemResposta = "Estoque insuficiente. Pedido negado.";
                }

                // Tenta adicionar o pedido ao banco de dados
                db.Pedidos.Add(pedido);
                db.SaveChanges(); // Aqui pode ocorrer o erro relacionado a dados inconsistentes ou faltando

                return Ok(new { mensagem = "Pedido processado com sucesso.", pedido });
            }
            catch (DbUpdateException dbEx) // Captura erros de atualização do banco de dados
            {
                // Log da exceção para detalhes sobre o erro
                Console.WriteLine($"Erro ao atualizar banco de dados: {dbEx.InnerException?.Message ?? dbEx.Message}");

                return Content(HttpStatusCode.InternalServerError, new
                {
                    mensagem = "Erro interno ao processar o pedido.",
                    detalhes = dbEx.Message
                });
            }
            catch (Exception ex)
            {
                // Log de erro geral
                Console.WriteLine($"Erro ao processar pedido: {ex.Message}");

                return Content(HttpStatusCode.InternalServerError, new
                {
                    mensagem = "Erro interno ao processar o pedido.",
                    detalhes = ex.Message
                });
            }
        }

        // Método para responder ao pedido (PUT)
        [HttpPut]
        [Route("api/pedido/{id}/responder")]
        public IHttpActionResult ResponderPedido(int id, [FromBody] PedidoResposta resposta)
        {
            try
            {
                var pedido = db.Pedidos.Find(id);
                if (pedido == null)
                {
                    return NotFound();
                }

                var medicamento = db.Medicamentos.Find(pedido.MedicamentoId);
                if (medicamento == null)
                {
                    return BadRequest($"Medicamento com ID {pedido.MedicamentoId} não encontrado.");
                }

                if (resposta.Aprovado && medicamento.QuantidadeDisponivel >= resposta.QuantidadeAprovada)
                {
                    medicamento.QuantidadeDisponivel -= resposta.QuantidadeAprovada;
                    pedido.Estado = "Aprovado";
                    pedido.QuantidadeAprovada = resposta.QuantidadeAprovada;
                    pedido.MensagemResposta = resposta.Mensagem ?? "Pedido aprovado com sucesso.";
                }
                else
                {
                    pedido.Estado = "Negado";
                    pedido.QuantidadeAprovada = 0;
                    pedido.MensagemResposta = resposta.Mensagem ?? "Estoque insuficiente. Pedido negado.";
                }

                db.SaveChanges();

                return Ok(new { mensagem = "Resposta registrada com sucesso.", pedido });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new
                {
                    mensagem = "Erro ao processar a resposta do pedido.",
                    detalhes = ex.Message
                });
            }
        }

        // Método para obter um pedido específico (GET)
        [HttpGet]
        [Route("api/pedido/{id}")]
        public IHttpActionResult ObterPedidoPorId(int id)
        {
            try
            {
                var pedido = db.Pedidos.Find(id);
                if (pedido == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    pedido.Id,
                    pedido.Estado,
                    pedido.MensagemResposta,
                    pedido.QuantidadeAprovada
                });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new
                {
                    mensagem = "Erro ao carregar o pedido.",
                    detalhes = ex.Message
                });
            }
        }

        // Método para obter todos os pedidos (GET)
        [HttpGet]
        [Route("api/pedido")]
        public IHttpActionResult ObterTodosPedidos()
        {
            try
            {
                var pedidos = db.Pedidos.Select(p => new
                {
                    p.Id,
                    p.Estado,
                    p.MensagemResposta,
                    p.QuantidadeAprovada
                }).ToList();

                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new
                {
                    mensagem = "Erro ao carregar os pedidos.",
                    detalhes = ex.Message
                });
            }
        }

        // Método privado para processar o pedido automaticamente
        private void ProcessarAutomaticamente(Pedido pedido)
        {
            var medicamento = db.Medicamentos.Find(pedido.MedicamentoId);

            if (medicamento == null)
            {
                pedido.Estado = "Negado";
                pedido.QuantidadeAprovada = 0;
                pedido.MensagemResposta = "Medicamento não encontrado. Pedido negado.";
            }
            else if (medicamento.QuantidadeDisponivel >= pedido.QuantidadeSolicitada)
            {
                medicamento.QuantidadeDisponivel -= pedido.QuantidadeSolicitada;
                pedido.Estado = "Aprovado";
                pedido.QuantidadeAprovada = pedido.QuantidadeSolicitada;
                pedido.MensagemResposta = "Pedido aprovado com sucesso.";
            }
            else
            {
                pedido.Estado = "Negado";
                pedido.QuantidadeAprovada = 0;
                pedido.MensagemResposta = "Estoque insuficiente. Pedido negado.";
            }
        }

        [HttpDelete]
        [Route("api/pedido/{id}")]
        public IHttpActionResult DeletarPedido(int id)
        {
            try
            {
                var pedido = db.Pedidos.Find(id);
                if (pedido == null)
                {
                    return NotFound();
                }

                db.Pedidos.Remove(pedido);
                db.SaveChanges();

                return Ok(new { mensagem = "Pedido excluído com sucesso.", pedidoId = id });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new
                {
                    mensagem = "Erro ao excluir o pedido.",
                    detalhes = ex.Message
                });
            }
        }

    }
}

