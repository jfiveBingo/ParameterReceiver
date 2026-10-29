using System;
using System.Linq;

namespace ParameterReceiver
{
    public class ParametreXManager
    {
        public ParametreX[] Serie { get; private set; }



        public double Min { get; private set; }
        public double Max { get; private set; }

        public ParametreXManager(ParametreX[] serie)
        {
            Serie = serie;
            Min = double.MaxValue;
            Max = double.MinValue;
        }

        public static ParametreX Creer(string code, int index)
        {
            return new ParametreX
            {
                Code = code,
                SymboleIndex = index,
                IndexHex = BitConverter.GetBytes(index).Take(4).ToArray()
            };
        }

        public byte[] ExtraireDataApresCode(byte[] paquet, byte[] codeHex)
        {
            int pos = Chercher(paquet, codeHex);

            if (pos == -1) return null;

            // Position juste après le code de 4 octets
            int debut = pos + codeHex.Length + 10;

            // Récupérer toutes les données restantes
            int longueur = paquet.Length - debut;

            byte[] data = new byte[longueur];

            Buffer.BlockCopy(paquet, debut, data, 0, longueur);

            return data;
        }

        public void TraiterSerie(byte[] paquet)
        {
            foreach (ParametreX p in Serie)
            {
                int pos = Chercher(paquet, p.IndexHex);

                if (pos == -1 || pos + 12 > paquet.Length)
                    continue;

                if (paquet[pos + 11] != 0x40)
                    continue;

                p.ValeurHex = new byte[8];
                Buffer.BlockCopy(paquet, pos + 4, p.ValeurHex, 0, 8);

                p.Valeur = ConvertirValeurDouble(p.ValeurHex);
            }

            // Calcul du Min et Max UNE SEULE FOIS après le remplissage
            bool trouve = false;

            foreach (ParametreX p in Serie)
            {
                if (p.ValeurHex == null)
                    continue;

                if (!trouve)
                {
                    Min = p.Valeur;
                    Max = p.Valeur;
                    trouve = true;
                }
                else
                {
                    if (p.Valeur < Min) Min = p.Valeur;
                    if (p.Valeur > Max) Max = p.Valeur;
                }
            }
        }

        private double ConvertirValeurDouble(byte[] valeurHex)
        {
            if (valeurHex == null || valeurHex.Length != 8)
                return -1;

            // Le paquet est en Big Endian -> BitConverter attend du Little Endian
            byte[] little = (byte[])valeurHex.Clone();
            //Array.Reverse(little);

            return BitConverter.ToDouble(little, 0);
        }

        private int Chercher(byte[] data, byte[] motif)
        {
            for (int i = 0; i <= data.Length - motif.Length; i++)
            {
                bool ok = true;

                for (int j = 0; j < motif.Length; j++)
                {
                    if (data[i + j] != motif[j])
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                    return i;
            }

            return -1;
        }
    }

}


