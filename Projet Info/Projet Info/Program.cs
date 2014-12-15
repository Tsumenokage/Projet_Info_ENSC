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

        public static void RemplirTableau(string[] donneeBrutes)
        {
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader monStreamReader = new StreamReader("H:\\Info\\Projet Info\\trunk\\prenoms_bordeaux.txt", encoding);
            string mot;

            for (int i = 0; i < donneeBrutes.Length; i++)
            {
                mot = monStreamReader.ReadLine();
                donneeBrutes[i] = mot;
            }
            // Fermeture du StreamReader (attention très important) 
            monStreamReader.Close(); 
        }


        public static string[,] miseEnFormeDonnees(string[] donneesBrutes)
        {
            string[,] Donnees = new string[donneesBrutes.Length, 4];

            for (int i = 0; i < donneesBrutes.Length; i++)
            {
                int indexBase = 0;
                int index = 0;
                char enCours;
                string ligne = donneesBrutes[i];
                enCours = ligne[index];

                for (int j = 0; j < 4; j++)
                {
                    while (enCours != '\t' && enCours != '\n' && enCours != '\r')
                    {
                        Console.Write(enCours);
                        index++;
                        enCours = ligne[index];
                    }

                    Donnees[i, j] = ligne.Substring(indexBase, index);
                    index++;
                    indexBase = index;                    
                }
            }
            return Donnees;
        }


        // \t tabulation
        static void Main(string[] args)
        {
            int nbDonnees;
            string[] donneeBrutes;
            string[,] Donnees;


            nbDonnees = compteMotsFichier();
            donneeBrutes = new string[nbDonnees];
            RemplirTableau(donneeBrutes);
            Donnees = miseEnFormeDonnees(donneeBrutes);

            for (int i = 0; i < 10; i++)
            {
                for(int j = 0; j<4; j++)
                    Console.WriteLine(Donnees[i,j]);
            }

            Console.ReadLine();
        }
    }
}
