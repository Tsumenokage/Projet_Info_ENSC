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
            public int annee;
            public int nombrePrenom;
            public int ordre;
        };

        public static int compteMotsFichier()
        {
            try
            {
                System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader monStreamReader = new StreamReader("prenoms_bordeaux.txt", encoding);
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

        public static void remplirTableau(string[] donneeBrutes)
        {
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader monStreamReader = new StreamReader("prenoms_bordeaux.txt", encoding);
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
                        index++;

                        if (index + indexBase < ligne.Length)
                        {
                            enCours = ligne[indexBase + index];
                        }
                    }

                    switch (j)
                    {
                        case 0 :
                            Donnees[i-1].annee = int.Parse(ligne.Substring(indexBase, index));
                            break;
                        case 1 :
                            Donnees[i-1].prenom = ligne.Substring(indexBase, index);
                            break;
                        case 2 :
                            Donnees[i-1].nombrePrenom = int.Parse(ligne.Substring(indexBase, index));
                            break;
                        case 3 :
                            Donnees[i-1].ordre = int.Parse(ligne.Substring(indexBase, index));
                            break;
                    }

                    
                    index++;
                    indexBase = indexBase + index;
                    index = 0;
                }
            }
            return Donnees;
        }

        public static void AffichageDonnee (Prenom prenom)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write('\n');
            Console.WriteLine("Données concernant le prénom {0} durant l'année {1}", prenom.prenom, prenom.annee);
            Console.WriteLine("Nombre de prénom sur cette année : {0}", prenom.nombrePrenom);
            Console.WriteLine("Ordre du prénom (sur 100) sur cette année : {0}",prenom.ordre);
            Console.Write('\n');
            Console.ResetColor();
        }

        public static void prenomQuelconqueSurUneAnnee(Prenom[] Donnees)
        {
            Random rand = new Random();
            int alea;
            Prenom prenomChoisit = new Prenom();
            int annee = 0;
            bool anneeOk = false;

            alea = rand.Next(1, 100);

            while (!anneeOk)
            {
                anneeOk = true;
                try
                {
                    Console.WriteLine("Veuillez entrer une année : ");
                    annee = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Annee incorrecte");
                    anneeOk = false;
                }
            }

            int NbAnneeCorrecte = 0;
            for (int i = 0; i < Donnees.Length; i++)
            {
                if (annee == Donnees[i].annee)
                {
                    NbAnneeCorrecte++;
                    if(NbAnneeCorrecte == alea)
                    {
                        prenomChoisit = Donnees[i];
                    }
                }
            }

            AffichageDonnee(prenomChoisit);
        }


        // \t tabulation
        static void Main(string[] args)
        {
            int nbDonnees;
            int choix = 0;
            string[] donneeBrutes;
            Prenom[] Donnees;
            bool choixOk = false;
            bool quitte = false;

            nbDonnees = compteMotsFichier();
            donneeBrutes = new string[nbDonnees];
            remplirTableau(donneeBrutes);
            Donnees = miseEnFormeDonnees(donneeBrutes);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*************************************************************************************");
            Console.WriteLine("*                    Projet Informatique : étude des prenoms                        *");
            Console.WriteLine("*************************************************************************************");
            Console.ResetColor();

            while (!quitte)
            {
                //Affichage d'un menu pour selectionner l'opération à faire (WIP)
                Console.WriteLine("Menu : (en cours)");
                Console.WriteLine("1) Affichage d'un prénom quelqconque sur une année");
                Console.WriteLine("0) Quitter le programme");
                choixOk = false;
                while (!choixOk)
                {
                    choixOk = true;
                    try
                    {
                        Console.WriteLine("question");
                        choix = int.Parse(Console.ReadLine());

                    }
                    catch
                    {
                        Console.WriteLine("Valeur incorrecte");
                        choixOk = false;
                    }
                }

                switch (choix)
                {
                    case 1:
                        prenomQuelconqueSurUneAnnee(Donnees);
                        break;

                    case 0:
                        quitte = true;
                        break;
                }
            }
        }
    }
}
