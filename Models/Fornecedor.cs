using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFORNECEDOR.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        // Relacionamento com Pedido
        [JsonIgnore]
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}