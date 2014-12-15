using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO; // necessaire pour la manipulation de fichers


namespace Projet_Info
{
    class Program
    {
        public static int compteMotsFichier()
        {
            try
            {
                System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader monStreamReader = new StreamReader("H:\\Info\\Projet Info\\trunk\\prenoms_bordeaux.txt", encoding);
                int nbMots = 0;
                string mot = monStreamReader.ReadLine();

                while (mot != null)
                {
                    Console.WriteLine(mot);
                    mot = monStreamReader.ReadLine();
                    nbMots++;
                }
                monStreamReader.Close();
                return nbMots;
            }
            catch (Exception ex)
            {
                // Code exécuté en cas d'exception 
                Console.Write("Une erreur est survenue au cours de la lecture :");
                Console.WriteLine(ex.Message);
            }
            return -1;
        }

        // \t tabulation
        static void Main(string[] args)
        {
            int nbDonnees;
            nbDonnees = compteMotsFichier();


            Console.ReadLine();
        }
    }
}
