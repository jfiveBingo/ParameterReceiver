using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParameterReceiver
{
    public class ParametreX
    {
        public string Code { get; set; }          // Ax1
        public int SymboleIndex { get; set; }     // 111
        public byte[] IndexHex { get; set; }      // 6F 00 00

        public byte[] ValeurHex { get; set; }     // x octets trouvés
        public double Valeur { get; set; }
    }
}
