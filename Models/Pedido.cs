using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebFORNECEDOR.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        // Relacionamento com Fornecedor
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }

        // Relacionamento com Medicamento
        public int MedicamentoId { get; set; }
        public Medicamento Medicamento { get; set; }

        public int QuantidadeSolicitada { get; set; }
        public int? QuantidadeAprovada { get; set; }
        public string MensagemResposta { get; set; }
        public string Estado { get; set; }
        public string Descricao { get; set; }

        public DateTime DataPedido { get; set; }
    }
}