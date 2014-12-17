using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO; // necessaire pour la manipulation de fichers


namespace Projet_Info
{
    class Program
    {
        public struct Prenom
        {
            public string prenom;
            public int annee { get; set; }
            public int nombrePrenom { get; set; }
            public int ordre { get; set; }


            public override string ToString()
            {
                return prenom + "   " + annee + "  " + nombrePrenom + " " + ordre;
            }
        };

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


        public static Prenom[] miseEnFormeDonnees(string[] donneesBrutes)
        {
            Prenom[] Donnees = new Prenom[donneesBrutes.Length];


            for (int i = 1; i < donneesBrutes.Length; i++)
            {
                int indexBase = 0;
                int index = 0;
                char enCours;
                string ligne = donneesBrutes[i];
                

                for (int j = 0; j < 4; j++)
                {
                    enCours = ligne[indexBase];                    
                    while (enCours != '\t' && index+indexBase < ligne.Length)
                    {
                        Console.Write(enCours);
                        index++;

                        if (index + indexBase < ligne.Length)
                        {
                            enCours = ligne[indexBase + index];
                        }
                    }

                    switch (j)
                    {
                        case 0 :
                            Donnees[i].annee = int.Parse(ligne.Substring(indexBase, index-1));
                            break;
                        case 1 :
                            Donnees[i].prenom = ligne.Substring(indexBase, index);
                            break;
                        case 2 :
                            Donnees[i].nombrePrenom = int.Parse(ligne.Substring(indexBase, index-1));
                            break;
                        case 3 :
                            Donnees[i].ordre = int.Parse(ligne.Substring(indexBase, index));
                            break;
                    }

                    
                    index++;
                    indexBase = indexBase + index;
                    index = 0;
                }
            }
            return Donnees;
        }


        // \t tabulation
        static void Main(string[] args)
        {
            int nbDonnees;
            string[] donneeBrutes;
            Prenom[] Donnees;


            nbDonnees = compteMotsFichier();
            donneeBrutes = new string[nbDonnees];
            RemplirTableau(donneeBrutes);
            Donnees = miseEnFormeDonnees(donneeBrutes);

            for (int i = 0; i < 10; i++)
            {
                for(int j = 0; j<4; j++)
                    Console.WriteLine(Donnees[i].ToString());
            }

            Console.ReadLine();
        }
    }
}
