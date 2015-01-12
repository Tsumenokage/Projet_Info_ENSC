using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO; // necessaire pour la manipulation de fichers


namespace Projet_Info
{
    class Program
    {
        
        /*************************************/
        /******        STRUCTURES            */
        /*************************************/

        /// <summary>
        /// Structure qui va contenir les différentes données d'un prénom 
        /// contenues dans le fichier source
        /// </summary>
        public struct Prenom
        {
            public string prenom;
            public int annee;
            public int nombrePrenom;
            public int ordre;
        };

        public struct Texte
        {
            public String nomTexte;
            public String texte;
        }

        /*******************************/
        /*          FONCTIONS          */
        /*******************************/

        /// <summary>
        /// Cette fonction va compter le nombre total de ligne dans le fichier source
        /// </summary>
        /// <returns>Un entier indiquant le nombre de lignes comprises dans le fichier 
        /// source</returns>
        public static int compteMotsFichier(String file)
        {
            try
            {
                System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader monStreamReader = new StreamReader(file, encoding);
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
        
        /// <summary>
        /// Cette fonction va effectuer le stockage de chaque ligne du fichier source 
        /// dans un tableau de chaînes de caractères
        /// </summary>
        /// <param name="donneeBrutes">Le tableau qui contiendra les lignes du 
        /// fichier source</param>
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

        /// <summary>
        /// cette fonction va mettre en forme les données récupérées sous forme de ligne de 
        /// caractère en les enregistrant dans un tableau de structure Prenom
        /// </summary>
        /// <param name="donneesBrutes">Le tableau qui contient les données enregistré sous forme 
        /// de chaînes de caractères</param>
        /// <returns>Un tableau de strcture prénom qui contiendra l'ensemble 
        /// des données du fichiers source plus facilement utilisable</returns>
        public static Prenom[] miseEnFormeDonnees(string[] donneesBrutes)
        {
            Prenom[] Donnees = new Prenom[donneesBrutes.Length];


            for (int i = 1; i < donneesBrutes.Length; i++)
            {
                int indexBase = 0;
                int index = 0;
                char enCours;
                string ligne = donneesBrutes[i];
                
                //1914  Jean    25  50
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

        public static void recuperationTexteProgramme(String file, Texte[] Donneestexte)
        {
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader monStreamReader = new StreamReader(file, encoding);
            string[] mot;

            for (int i = 0; i < Donneestexte.Length; i++)
            {
                mot = monStreamReader.ReadLine().Split('\t');
                Texte texte = new Texte();

                for (int j = 0; j < 2; j++)
                {
                    if (j == 0)
                        texte.nomTexte = mot[j];
                    else
                        texte.texte = mot[j];
                }

                Donneestexte[i] = texte;
            }
        }

        public static bool choixSelection(Texte[] texteProgramme)
        {
            bool entreeOk = false;
            affichageTexte("ChoiceName", texteProgramme);

            string choix = Console.ReadLine();

            while (!entreeOk)
            {
                if (choix == "y")
                    return true;
                else if (choix == "n")
                    return false;
                else
                    affichageTexte("WrongEntry", texteProgramme);
            }

            return false;
        }

        /// <summary>
        /// Cette fonction va afficher les données d'un prénom passé en paramètre
        /// </summary>
        /// <param name="affichage">Prénom dont on veut afficher les différentes données</param>
        public static void AffichageDonnee(Prenom affichage, Texte[] texteProgramme)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write('\n');
            affichageTexte("NameData",texteProgramme, affichage.prenom.ToString(), affichage.annee.ToString());
            affichageTexte("NumberData", texteProgramme, affichage.nombrePrenom.ToString());
            affichageTexte("OrderData",texteProgramme, affichage.ordre.ToString());
            Console.Write('\n');
            Console.ResetColor();
        }


        /// <summary>
        /// Cette fonction va sélectionner un prénom aléatoire sur une année entrée par 
        /// l'utilisateur et va ensuite afficher les données de ce prénom
        /// </summary>
        /// <param name="Donnees">L'ensemble des données du fichiers sources</param>
        public static void prenomQuelconqueSurUneAnnee(Prenom[] Donnees, Texte[] texteProgramme)
        {
            Random rand = new Random();
            int alea;
            Prenom prenomChoisit = new Prenom();
            int annee = 0;
            bool anneeOk = false;

            alea = rand.Next(1, 100);
            Console.Clear();
            while (!anneeOk)
            {
                anneeOk = true;
                try
                {
                    affichageTexte("EnterYear", texteProgramme);
                    annee = int.Parse(Console.ReadLine());
                }
                catch
                {
                    affichageTexte("IncorrectYear", texteProgramme);
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

            AffichageDonnee(prenomChoisit,texteProgramme);
        }

        /// <summary>
        /// Cette fonction va demander à l'utilisatur d'enter une période sur laquelle il souhaite
        /// étudier les prénoms. Cette fonction va ensuite créer un nouveau tableau rassemblant
        /// les prénoms sur cette période.
        /// </summary>
        /// <param name="Donnees">L'ensemble des données du fichiers sources</param>
        /// <returns>Tableau de Prenom contenant les prénoms sur la période de 
        /// l'utilisateur</returns>
        public static Prenom[] demandePeriode(Prenom[] Donnees)
        {


            Prenom[] PrenomsPeriodeTemp = new Prenom[Donnees.Length];
            Prenom[] donneesSurPeriode;
            int anneeDebut = 0;
            int anneeFin = 0;
            bool anneeDebutOk = false;
            bool anneeFinOk = false;

            Console.Clear();
            while (!anneeDebutOk)
            {
                anneeDebutOk = true;

                try
                {
                    Console.WriteLine("Veuillez entrer le début de la période :");
                    anneeDebut = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Annee incorrecte");
                    anneeDebutOk = false;
                }

            }

            while (!anneeFinOk)
            {
                anneeFinOk = true;

                try
                {
                    Console.WriteLine("Veuillez entrer la fin de la période :");
                    anneeFin = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Année incorrecte");
                    anneeFinOk = false;
                }

            }


            if (anneeFin < anneeDebut)
            {
                Console.WriteLine("Fin de période inférieur de période, échange des bornes");
                int tmp = anneeFin;
                anneeFin = anneeDebut;
                anneeDebut = tmp;
            }

            donneesSurPeriode = traitementDonneesSurPeriode(Donnees, anneeDebut, anneeFin);

            return donneesSurPeriode;

        }
        
        /// <summary>
        /// Cette fonction va créer un tableau qui contiendra les prénoms sur une période
        /// donnée avec pour chaque prénom, le nombre total de fois ou il apparaît
        /// </summary>
        /// <param name="Donnees">L'ensemble des données que l'on souhaite traiter</param>
        /// <param name="anneeDebut">L'année de début de la période</param>
        /// <param name="anneeFin">L'année de fin de la période</param>
        /// <returns>Un tableau de prénom contenant les prénoms de la
        /// période avec le nombre total d'apparition sur cette période</returns>
        public static Prenom[] traitementDonneesSurPeriode(Prenom[] Donnees, int anneeDebut, int anneeFin)
        {
            int nbCase = 0;
            int indexPrenom = 0;
            bool estPresent = false;
            Prenom[] PrenomsPeriodeTemp = new Prenom[Donnees.Length];



            for (int i = 0; i < Donnees.Length; i++)
            {
                if (Donnees[i].annee <= anneeFin && Donnees[i].annee >= anneeDebut)
                {
                    int j = 0;
                    estPresent = false;
                    while (j < nbCase && !estPresent)
                    {
                        if (Donnees[i].prenom == PrenomsPeriodeTemp[j].prenom)
                        {

                            estPresent = true;
                            indexPrenom = j;
                        }
                        else
                        {
                            estPresent = false;
                        }
                        j++;

                    }

                    if (estPresent)
                    {
                        PrenomsPeriodeTemp[indexPrenom].nombrePrenom += Donnees[i].nombrePrenom;

                    }
                    else
                    {
                        PrenomsPeriodeTemp[nbCase] = Donnees[i];
                        nbCase++;
                    }
                }
            }

            Prenom[] donneeSurPeriode = new Prenom[nbCase];

            for (int i = 0; i < nbCase; i++)
            {
                donneeSurPeriode[i] = PrenomsPeriodeTemp[i];
            }

            return donneeSurPeriode;

        }

        /// <summary>
        /// Cette fonction va afficher le top10 des prénoms sur une période données
        /// par l'tilisateur
        /// </summary>
        /// <param name="Donnees">L'ensemble des données que l'on souhaite traiter</param>
        public static void top10PrenomsPeriode (Prenom[] Donnees, Texte[] texteProgramme)
        {
            
            Prenom[] donneSurPeriode;


            donneSurPeriode = demandePeriode(Donnees);

            donneSurPeriode = triSurNbNaissance(donneSurPeriode);


            for (int j = 0;  j < 10 ;  j++)
            {
                int tmp = j + 1;   
                affichageTexte("Top10Res",texteProgramme,""+tmp, donneSurPeriode[j].prenom, ""+donneSurPeriode[j].nombrePrenom);
            }
        }

        /// <summary>
        /// Cette fonction va trier un tableau de prénom en fonction du nombre de naissance
        /// </summary>
        /// <param name="Donnees">L'ensemble des données que l'on souhaite traiter</param>
        /// <returns>Le tableau passé en paramètre mais trié selon les naissances</returns>
        public static Prenom[] triSurNbNaissance (Prenom[] Donnees)
        {
            // Tri du tableau avec algorithme à bulle (compléxité en n² voir par le remplacer pas un quicksort si possible)
            int i = 1;
            int n = Donnees.Length;

            while (i < n)
            {
                for (int j = n; j > i; j--)
                {
                    if (Donnees[j - 1].nombrePrenom > Donnees[j - 2].nombrePrenom)
                    {
                        Prenom tmp = Donnees[j - 1];
                        Donnees[j - 1] = Donnees[j - 2];
                        Donnees[j - 2] = tmp;
                    }
                }
                i++;
            }

            return Donnees;

        }

        public static int partitionTriRapide(Prenom[] Donnees, int premier, int dernier, int pivot)
        {
            Prenom temp;

            temp = Donnees[dernier];
            Donnees[dernier] = Donnees[pivot];
            Donnees[pivot] = temp;
            

            int j = premier;

            for (int i = premier; i <= dernier - 1; i++)
            {
                if (String.Compare(Donnees[i].prenom,Donnees[dernier].prenom) < 0)
                {
                    temp = Donnees[i];
                    Donnees[i] = Donnees[j];
                    Donnees[j] = temp;


                    j++;
                }
            }
            temp = Donnees[dernier];
            Donnees[dernier] = Donnees[j];
            Donnees[j] = temp;


            return j;
        }


        public static void triRapideSurPrenom(Prenom[] Donnees, int premier, int dernier)
        {
            int pivot;
            if (premier <= dernier)
            {
                pivot = premier;
                pivot = partitionTriRapide(Donnees, premier, dernier, pivot);
                triRapideSurPrenom(Donnees, premier, pivot-1);
                triRapideSurPrenom(Donnees, pivot + 1, dernier);
            }
            

        }

        public static void triBulleSurPrenom(Prenom[] Donnees)
        {
            // Tri du tableau avec algorithme à bulle (compléxité en n² voir par le remplacer pas un quicksort si possible)
            int i = 1;
            int n = Donnees.Length;

            while (i < n)
            {
                for (int j = n; j > i; j--)
                {                    
                    if(String.Compare(Donnees[j-1].prenom,Donnees[j-2].prenom) < 0)
                    {
                        Prenom tmp = Donnees[j - 1];
                        Donnees[j - 1] = Donnees[j - 2];
                        Donnees[j - 2] = tmp;
                        
                    }
                }
                i++;
            }


        }


        /// <summary>
        /// Cette fonction va afficher les informations d'un prénoms aléatoire sur l'ensemble 
        /// d'une période
        /// </summary>
        /// <param name="Donnees">L'ensemble des données que l'on souhaite traiter</param>
        public static void nomPeriodeDonnee(Prenom[] Donnees, Texte[] texteProgramme)
        {
            Prenom[] prenomSurPeriode;
            Prenom prenomChoisi;
            int nbPrenom;
            Random alea = new Random();
            int rangPrenom;

            prenomSurPeriode = demandePeriode(Donnees);

            prenomSurPeriode = triSurNbNaissance(prenomSurPeriode);

            nbPrenom = prenomSurPeriode.Length;

            rangPrenom = alea.Next(0,nbPrenom-1);

            prenomChoisi = prenomSurPeriode[rangPrenom];

            affichageTexte("NumPeriode", texteProgramme, prenomChoisi.prenom, "" + prenomChoisi.nombrePrenom);
            rangPrenom = rangPrenom + 1;
            affichageTexte("OrderName", texteProgramme, "" + rangPrenom, "" + nbPrenom);

        }

        /// <summary>
        /// Cette fonction va afficher la tendance d'un prénom choisi aléatoirement
        /// sur les X dernières années entrées par l'utilisateur
        /// </summary>
        /// <param name="Donnees">L'ensemble des données que l'on souhaite traiter</param>
        public static void tendancePrenom(Prenom[] Donnees, Texte[] texteProgramme)
        {
            int nbAnneeEnArriere = 0;
            bool nbAnneeEnArriereOk = false;
            int finPremierePeriode;
            int debutSecondePeriode;

            Prenom[] PremierePeriode;
            Prenom[] SecondePeriode;

            Random aleatoire = new Random();
            int indicePrenomChoisi;
            Prenom prenomSecondePeriode;
            Prenom prenomPremierePeriode = new Prenom();

            double moyennePremierePeriode;
            double moyenneSecondePeriode;
            double ecartMoyenne;
            double ecartType;

            Console.Clear();

            while (!nbAnneeEnArriereOk)
            {
                nbAnneeEnArriereOk = true;

                try
                {
                    affichageTexte("periodeTrend", texteProgramme);
                    nbAnneeEnArriere = int.Parse(Console.ReadLine());
                }
                catch
                {
                    affichageTexte("IncorrectYear", texteProgramme);
                    nbAnneeEnArriereOk = false;
                }

            }

            debutSecondePeriode = 2013 - nbAnneeEnArriere;
            finPremierePeriode = debutSecondePeriode - 1;

            PremierePeriode = traitementDonneesSurPeriode(Donnees, 1900, finPremierePeriode);
            SecondePeriode = traitementDonneesSurPeriode(Donnees, debutSecondePeriode, 2013);

            indicePrenomChoisi = aleatoire.Next(0, Donnees.Length-1);
            prenomSecondePeriode = Donnees[indicePrenomChoisi];

            bool prenomtrouve = false;
            int indice = 0;

            while (!prenomtrouve && indice < PremierePeriode.Length )
            {
                if(PremierePeriode[indice].prenom == prenomSecondePeriode.prenom)
                {
                    prenomtrouve = true;
                    prenomPremierePeriode = PremierePeriode[indice];
                }
                indice++;
            }

            if(!prenomtrouve)
            {                
                prenomPremierePeriode.nombrePrenom = 0;
            }

            moyennePremierePeriode = calculMoyenne(prenomPremierePeriode, finPremierePeriode - 1900);
            moyenneSecondePeriode = calculMoyenne(prenomSecondePeriode, nbAnneeEnArriere);

            ecartType = calculEcartType(Donnees, finPremierePeriode, moyennePremierePeriode, prenomPremierePeriode);
            ecartMoyenne = moyenneSecondePeriode - moyennePremierePeriode;

            affichageTexte("Trend",texteProgramme, prenomSecondePeriode.prenom, ""+nbAnneeEnArriere);

            if(ecartMoyenne <= (-2*ecartType))
                affichageTexte("Abandonement", texteProgramme, "" + nbAnneeEnArriere);
            else if(ecartMoyenne < (-ecartType))
                affichageTexte("Outmoded", texteProgramme, "" + nbAnneeEnArriere);
            else if (ecartMoyenne < (ecartType))
                affichageTexte("Hold", texteProgramme, "" + nbAnneeEnArriere);
            else if (ecartMoyenne < (2*ecartType))
                affichageTexte("Fashionable", texteProgramme, "" + nbAnneeEnArriere);
            else
                affichageTexte("Explosed", texteProgramme, "" + nbAnneeEnArriere);

        }
        
        /// <summary>
        /// Cette fonction va calculer la moyenne d'apparition d'un prénom sur 
        /// nombre d'année donné
        /// </summary>
        /// <param name="prenom">Les informations du prénom dont on vaut la moyenne</param>
        /// <param name="nbAnnee">Lenombre d'année sur lesquels on étudie le prénom</param>
        /// <returns>La moyenne d'apparition du prénom</returns>
        public static double calculMoyenne (Prenom prenom, int nbAnnee)
        {
            double moyenne = prenom.nombrePrenom*1.0 / nbAnnee*1.0;

            return moyenne;
        }

        /// <summary>
        /// Cette moyenne va calculer l'écart type d'un prénom sur une période donnée
        /// </summary>
        /// <param name="Donnees">L'ensemble des données que l'on souhaite traiter</param>
        /// <param name="finPeriode">La date de fin de la période sur laquelle on veut
        /// l'écart typé (elle début toujours à 1900) </param>
        /// <param name="moyenne">La moyenne d'apparition du prénom sur la période</param>
        /// <param name="prenom">Les informations du prénom dont on vaut la moyenne</param>
        /// <returns></returns>
        public static double calculEcartType (Prenom[] Donnees, int finPeriode, double moyenne, Prenom prenom)
        {
            double total = 0.0, ecart, variance;

            for (int i = 0; i < Donnees.Length; i++)
            {
                
                if(prenom.prenom == Donnees[i].prenom && Donnees[i].annee <= finPeriode)
                {
                    
                    ecart = Donnees[i].nombrePrenom*1.0 - moyenne;
                    total = total + ecart * ecart;
                }
            }

            variance = total / (Donnees.Length*1.0 - 1);

            return Math.Sqrt(variance);

        }

        public static void affichageTexte(String texte, Texte[] texteProgramme, params String[] valeur)
        {
            bool trouve = false;
            int i = 0;
            while(trouve == false && i < texteProgramme.Length)
            {
                if (texte == texteProgramme[i].nomTexte)
                {
                    trouve = true;
                        switch (valeur.Length)
                        {
                            case 1:
                                Console.WriteLine(texteProgramme[i].texte, valeur[0]);
                                break;
                            case 2:
                                Console.WriteLine(texteProgramme[i].texte, valeur[0],valeur[1]);
                                break;
                            case 3:
                                Console.WriteLine(texteProgramme[i].texte, valeur[0], valeur[1],valeur[2]);
                                break;
                            case 4:
                                Console.WriteLine(texteProgramme[i].texte, valeur[0], valeur[1],valeur[2],valeur[3]);
                                break;
                            default :
                                Console.WriteLine(texteProgramme[i].texte);
                                break;

                        }
                }
                i++;
            }

            if(trouve == false)
                Console.WriteLine(texte);
        }

        /// <summary>
        /// Fonction qui va permettre le retour au menu lorsque l'un des traitements est terminé
        /// </summary>
        public static void retourMenu(Texte[] texteProgramme)
        {
            affichageTexte("ReturnToMenu", texteProgramme);
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            int nbDonnees;
            int choix = 0;
            string[] donneeBrutes;
            Prenom[] Donnees;
            Prenom[] DonneesTrieSurPrenom;
            bool choixOk = false;
            bool quitte = false;

            string langue = "fr_fr.txt";
            Texte[] texteProgramme;
            int nbChamp = compteMotsFichier(langue);
            texteProgramme = new Texte[nbChamp];
            recuperationTexteProgramme(langue, texteProgramme);


            nbDonnees = compteMotsFichier("prenoms_bordeaux.txt");
            donneeBrutes = new string[nbDonnees];
            remplirTableau(donneeBrutes);
            Donnees = miseEnFormeDonnees(donneeBrutes);
            DonneesTrieSurPrenom = Donnees;
            

            while (!quitte)
            {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            affichageTexte("TitleLine1", texteProgramme);
            affichageTexte("TitleLine2", texteProgramme);
            affichageTexte("TitleLine3", texteProgramme);
            Console.ResetColor();


                //Affichage d'un menu pour sélectionner l'opération à faire (WIP)
                affichageTexte("MenuLine1", texteProgramme);
                affichageTexte("MenuChoice1", texteProgramme);
                affichageTexte("MenuChoice2", texteProgramme);
                affichageTexte("MenuChoice3", texteProgramme);
                affichageTexte("MenuChoice4", texteProgramme);
                affichageTexte("MenuChoice0", texteProgramme);
                choixOk = false;
                while (!choixOk)
                {
                    choixOk = true;
                    try
                    {
                        affichageTexte("EnterChoice", texteProgramme);
                        choix = int.Parse(Console.ReadLine());

                    }
                    catch
                    {
                        affichageTexte("IncorrectValue", texteProgramme);
                        choixOk = false;
                    }
                }

                switch (choix)
                {
                    case 1:
                        prenomQuelconqueSurUneAnnee(Donnees,texteProgramme);
                        break;

                    case 2:
                        top10PrenomsPeriode(Donnees, texteProgramme);
                        break;

                    case 3:
                        nomPeriodeDonnee(Donnees, texteProgramme);
                        break;

                    case 4:
                        tendancePrenom(Donnees, texteProgramme);
                        break;

                    case 5 :
                        triRapideSurPrenom(DonneesTrieSurPrenom, 0, DonneesTrieSurPrenom.Length - 1);
                        for (int k = 0; k < 500; k++)
                        {
                            Console.WriteLine(DonneesTrieSurPrenom[k].prenom);
                        }
                        break;

                    case 0:
                        quitte = true;
                        break;
                }
                if(quitte != true)
                    retourMenu(texteProgramme);
            }
        }

        
    }
}
