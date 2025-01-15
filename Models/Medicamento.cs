using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Adicionando referência para o Table
using System.Linq;
using System.Web;

namespace WebFORNECEDOR.Models
{
    [Table("Medicamentos")] // Certificando-se de que o nome da tabela no banco é "Medicamentos"
    public class Medicamento
    {
        [Key]
        public int Id { get; set; }  // Id do medicamento

        public string Nome { get; set; }  // Nome do medicamento

        public string Lote { get; set; }  // Lote do medicamento

        public DateTime DataProducao { get; set; }  // Data de produção

        public DateTime DataValidade { get; set; }  // Data de validade

        public int QuantidadeDisponivel { get; set; }  // Quantidade disponível

        public string Descricao { get; set; }  // Descrição do medicamento

        [JsonIgnore] // Ignora a coleção de Pedidos durante a serialização
        public ICollection<Pedido> Pedidos { get; internal set; }
    }
}
