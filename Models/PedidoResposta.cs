using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFORNECEDOR.Models
{
    public class PedidoResposta
    {
        public bool Aprovado { get; set; } 
        public string Mensagem { get; set; }
        public int QuantidadeAprovada { get; set; }
    }
}