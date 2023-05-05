using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compi
{
    internal class Polish
    {
        private string lexema;
        private string etiqueta;
        private string apuntador;

        public string Lexema { get => lexema; set => lexema = value; }
        public string Etiqueta { get => etiqueta; set => etiqueta = value; }
        public string Apuntador { get => apuntador; set => apuntador = value; }
    }
}
