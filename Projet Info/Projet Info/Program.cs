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
        /// <summary>
        /// Structure Texte qui contiendra l'identifiant du texte
        /// à afficher dans la console ainsi que le texte que l'on veut afficher
        /// </summary>
        public struct Texte
        {
            public String nomTexte;
            public String texte;
        }

        /*******************************/
        /*          FONCTIONS          */
        /*******************************/



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
                        prenomSurUneAnnee(Donnees, texteProgramme);
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

                    case 5:
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
                if (quitte != true)
                    retourMenu(texteProgramme);
            }
        }


        /***************************/
        /*  FONCTIONS D'AFFICHAGE  */
        /***************************/

        /// <summary>
        /// Cette fonction va récupérer les données contenue dans un fichier passé en paramètre
        /// et va les stockés dans un tableau de Texte
        /// </summary>
        /// <param name="file">Le chemin du fichier de langue désiré</param>
        /// <param name="Donneestexte">Le tableau dans lequel seront stockés les informations</param>
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

        /// <summary>
        /// C'est la fonction qui va gérer l'affichage du texte dans l'ensemble du programme
        /// </summary>
        /// <param name="texte">Identifiant du texte que l'on veut afficher</param>
        /// <param name="texteProgramme">Tableau qui contient l'ensemble des textes du 
        /// programme sous forme de structure Texte</param>
        /// <param name="valeur">Tableau existant ou nom et à taille variable (mots clef param) 
        /// qui contiendra les paramètres que l'on souhaite afficher dans 
        /// une chaîne de caractères</param>
        public static void affichageTexte(String texte, Texte[] texteProgramme, params String[] valeur)
        {
            bool trouve = false;
            int i = 0;
            while (trouve == false && i < texteProgramme.Length)
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
                            Console.WriteLine(texteProgramme[i].texte, valeur[0], valeur[1]);
                            break;
                        case 3:
                            Console.WriteLine(texteProgramme[i].texte, valeur[0], valeur[1], valeur[2]);
                            break;
                        case 4:
                            Console.WriteLine(texteProgramme[i].texte, valeur[0], valeur[1], valeur[2], valeur[3]);
                            break;
                        default:
                            Console.WriteLine(texteProgramme[i].texte);
                            break;

                    }
                }
                i++;
            }

            if (trouve == false)
                Console.WriteLine(texte);
        }

        /// <summary>
        /// Cette fonction va afficher les données d'un prénom passé en paramètre
        /// </summary>
        /// <param name="affichage">Prénom dont on veut afficher les différentes données</param>
        /// <param name="texteProgramme">Ce tableau contient l'ensemble des Texte 
        /// qui seront afficher</param>
        public static void affichageDonnee(Prenom affichage, Texte[] texteProgramme)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write('\n');
            affichageTexte("NameData", texteProgramme, affichage.prenom.ToString(), affichage.annee.ToString());
            affichageTexte("NumberData", texteProgramme, affichage.nombrePrenom.ToString());
            affichageTexte("OrderData", texteProgramme, affichage.ordre.ToString());
            Console.Write('\n');
            Console.ResetColor();
        }

        /// <summary>
        /// Fonction qui va permettre le retour au menu lorsque l'un des traitements est terminé
        /// </summary>
        /// <param name="texteProgramme">Ce tableau contient l'ensemble des Texte 
        /// qui seront afficher</param>
        public static void retourMenu(Texte[] texteProgramme)
        {
            affichageTexte("ReturnToMenu", texteProgramme);
            Console.ReadLine();
        }

        /*********************************************/
        /*          LECTURE DES DONNEES              */
        /*********************************************/
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

        /************************************************/
        /*           ALGORITHME TRI ET RECHERCHE       */
        /***********************************************/

        /// <summary>
        /// Cette fonction va trier un tableau de prénom en fonction du nombre de naissance
        /// </summary>
        /// <param name="Donnees">L'ensemble des données que l'on souhaite traiter</param>
        /// <returns>Le tableau passé en paramètre mais trié selon les naissances</returns>
        public static Prenom[] triSurNbNaissance(Prenom[] Donnees)
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

        /// <summary>
        /// Cette fonction va permettre la partition du tableau dans le cas d'un tri
        /// utilisant l'algoritmhe quicksort
        /// </summary>
        /// <param name="Donnees">La tableau que l'on désire trié</param>
        /// <param name="premier">Le premier élément à partir duquel on va trier
        /// le tableau</param>
        /// <param name="dernier">le dernier élément que l'on va trié</param>
        /// <param name="pivot">Valeur qui determinera ou sera effectuer la partition
        /// du tableau</param>
        /// <returns></returns>
        public static int partitionTriRapide(Prenom[] Donnees, int premier, int dernier, int pivot)
        {
            Prenom temp;

            temp = Donnees[dernier];
            Donnees[dernier] = Donnees[pivot];
            Donnees[pivot] = temp;


            int j = premier;

            for (int i = premier; i <= dernier - 1; i++)
            {
                if (String.Compare(Donnees[i].prenom, Donnees[dernier].prenom) < 0)
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

        /// <summary>
        /// Algorithme récurssif pour trier un tableau par ordre alphabétique sur les prénoms
        /// en utilisant l'alogrithme quicksort beaucoup plus rapide et avec
        /// une compléxité faible (de l'ordre de nLog(n) en moyenne)
        /// </summary>
        /// <param name="Donnees">Les données que l'ont souhaite triées</param>
        /// <param name="premier">Borne inférieur à partir de laquelle on va trier
        /// le tableau (par défaut = 0)</param>
        /// <param name="dernier">Borne supérieur pour le tri, on ira jusqu'a 
        /// cette dernière (par défaut = taille-1)</param>
        public static void triRapideSurPrenom(Prenom[] Donnees, int premier, int dernier)
        {
            int pivot;
            if (premier <= dernier)
            {
                pivot = premier;
                pivot = partitionTriRapide(Donnees, premier, dernier, pivot);
                triRapideSurPrenom(Donnees, premier, pivot - 1);
                triRapideSurPrenom(Donnees, pivot + 1, dernier);
            }


        }

        /// <summary>
        /// Recherche d'un prénom dans un tableau trué en utilisant un algorithme de recherche
        /// par dichotomie
        /// </summary>
        /// <param name="Donnees">Les Données sur lesquelles ont effectue la recherche</param>
        /// <param name="prenom">Le prenom que l'on souhaite trouvé</param>
        /// <returns>La position du prénom dans le tableau
        /// retoourne -1 si le prénom ne s'y trouve pas</returns>
        public static int rechercheDichotomiquePrenom(Prenom[] Donnees, string prenom)
        {
            int debut = 0;
            int fin = Donnees.Length - 1;
            bool trouve = false;
            int indice;
            int i = -1;

            while (debut <= fin && !trouve)
            {
                i = (debut + fin) / 2;

                if (Donnees[i].prenom == prenom)
                    trouve = true;
                else if (string.Compare(Donnees[i].prenom, prenom) > 0)
                    fin = i - 1;
                else
                    debut = i + 1;
            }
            if (trouve)
                indice = i;
            else
                indice = -1;

            return indice;
        }

        /**********************************************/
        /*                 QUESTION 1                 */
        /**********************************************/

        /// <summary>
        /// Cette fonction va sélectionner un prénom aléatoire sur une année entrée par 
        /// l'utilisateur et va ensuite afficher les données de ce prénom
        /// </summary>
        /// <param name="texteProgramme">Ce tableau contient l'ensemble des Texte 
        /// qui seront afficher</param>
        /// <param name="Donnees">L'ensemble des données du fichiers sources</param>
        public static void prenomSurUneAnnee(Prenom[] Donnees, Texte[] texteProgramme)
        {
            Random rand = new Random();
            int indice;
            Prenom prenomChoisit = new Prenom();
            int annee = 0;
            bool anneeOk = false;
            Prenom[] anneeEtudiee;
            bool choixPrenom;


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

            choixPrenom = choixSelection(texteProgramme);

            anneeEtudiee = traitementDonneesSurPeriode(Donnees, annee, annee);

            if (choixPrenom)
            {
                indice = demandeChoixPrenom(anneeEtudiee, texteProgramme);
            }
            else
            {
                indice = rand.Next(0, 99);
            }
            prenomChoisit = anneeEtudiee[indice];
            affichageDonnee(prenomChoisit, texteProgramme);
        }

        /// <summary>
        /// Cette fonction va demander à l'utilisateur si il désire entrer lui même un prénom
        /// </summary>
        /// <param name="texteProgramme">Ce tableau contient l'ensemble des Texte 
        /// qui seront afficher</param>
        /// <returns></returns>
        public static bool choixSelection(Texte[] texteProgramme)
        {
            bool entreeOk = false;


            while (!entreeOk)
            {
                entreeOk = true;
                try
                {
                    affichageTexte("ChoiceName", texteProgramme);
                    string choix = Console.ReadLine();
                    if (choix == "y")
                        return true;
                    else if (choix == "n")
                        return false;
                    else
                    {
                        affichageTexte("WrongEntry", texteProgramme);
                        entreeOk = false;
                    }
                }
                catch
                {
                    affichageTexte("WrongEntry", texteProgramme);
                    entreeOk = false;
                }
            }

            return false;
        }

        /// <summary>
        /// Cette fonction va demander d'entrer un prenom à l'utilisateur et va ensuite vérifier 
        /// si ce prénom existe bien avant de retourner son indice dans le tableau 
        /// passé en paramètes
        /// </summary>
        /// <param name="Donnees">Tableau qui contient l'ensemble des prénoms 
        /// sur la période voulue</param>
        /// <param name="texteProgramme">Ce tableau contient l'ensemble des Texte 
        /// qui seront afficher</param>        
        /// <returns>l'indice du prénom choisi dans le tableau en paramètre</returns>
        public static int demandeChoixPrenom(Prenom[] Donnees, Texte[] texteProgramme)
        {
            bool prenomOk = false;
            string prenom = "null";
            int indice = 0;

            triRapideSurPrenom(Donnees, 0, Donnees.Length - 1);
            while (!prenomOk || indice == -1)
            {
                prenomOk = true;
                try
                {
                    affichageTexte("EnterFirstName", texteProgramme);
                    prenom = Console.ReadLine();
                    prenom = prenom.ToUpper();
                }
                catch
                {
                    affichageTexte("WrongEntry", texteProgramme);
                    prenomOk = false;
                }

                indice = rechercheDichotomiquePrenom(Donnees, prenom);
                if (indice == -1)
                    affichageTexte("NotFound", texteProgramme);
            }

            return indice;


        }

        /**********************************************/
        /*                 QUESTION 2                 */
        /**********************************************/

        /// <summary>
        /// Cette fonction va afficher le top10 des prénoms sur une période données
        /// par l'tilisateur
        /// </summary>
        /// <param name="texteProgramme">Ce tableau contient l'ensemble des Texte 
        /// qui seront afficher</param>
        /// <param name="Donnees">L'ensemble des données que l'on souhaite traiter</param>
        public static void top10PrenomsPeriode(Prenom[] Donnees, Texte[] texteProgramme)
        {

            Prenom[] donneSurPeriode;


            donneSurPeriode = demandePeriode(Donnees);

            donneSurPeriode = triSurNbNaissance(donneSurPeriode);


            for (int j = 0; j < 10; j++)
            {
                int tmp = j + 1;
                affichageTexte("Top10Res", texteProgramme, "" + tmp, donneSurPeriode[j].prenom, "" + donneSurPeriode[j].nombrePrenom);
            }
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

        /**********************************************/
        /*                 QUESTION 3                 */
        /**********************************************/

        /// <summary>
        /// Cette fonction va afficher les informations d'un prénoms aléatoire sur l'ensemble 
        /// d'une période
        /// </summary>
        /// <param name="Donnees">L'ensemble des données que l'on souhaite traiter</param>
        /// <param name="texteProgramme">Ce tableau contient l'ensemble des Texte 
        /// qui seront afficher</param>
        public static void nomPeriodeDonnee(Prenom[] Donnees, Texte[] texteProgramme)
        {
            Prenom[] prenomSurPeriode;
            Prenom prenomChoisi;
            int nbPrenom;
            Random alea = new Random();
            int rangPrenom = 0;
            int indice;
            bool choixPrenom;


            prenomSurPeriode = demandePeriode(Donnees);
            choixPrenom = choixSelection(texteProgramme);
            nbPrenom = prenomSurPeriode.Length;


            if (choixPrenom)
            {
                indice = demandeChoixPrenom(prenomSurPeriode, texteProgramme);
            }
            else
            {
                indice = alea.Next(0, nbPrenom - 1);
            }

            prenomChoisi = prenomSurPeriode[indice];

            prenomSurPeriode = triSurNbNaissance(prenomSurPeriode);

            int i = 0;

            while (i < prenomSurPeriode.Length)
            {
                if (prenomSurPeriode[i].prenom == prenomChoisi.prenom)
                    rangPrenom = i;
                i++;
            }

            affichageTexte("NumPeriode", texteProgramme, prenomChoisi.prenom, "" + prenomChoisi.nombrePrenom);
            rangPrenom = rangPrenom + 1;
            affichageTexte("OrderName", texteProgramme, "" + rangPrenom, "" + nbPrenom);

        }

        /**********************************************/
        /*                 QUESTION 4                 */
        /**********************************************/

        /// <summary>
        /// Cette fonction va afficher la tendance d'un prénom choisi aléatoirement
        /// sur les X dernières années entrées par l'utilisateur
        /// </summary>
        /// <param name="Donnees">L'ensemble des données que l'on souhaite traiter</param>
        /// <param name="texteProgramme">Ce tableau contient l'ensemble des Texte 
        /// qui seront afficher</param>
        public static void tendancePrenom(Prenom[] Donnees, Texte[] texteProgramme)
        {
            int nbAnneeEnArriere = 0;
            bool nbAnneeEnArriereOk = false;
            bool choixPrenom;
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

            choixPrenom = choixSelection(texteProgramme);

            if (choixPrenom)
            {
                prenomSecondePeriode = demandeChoixPrenomTendance(SecondePeriode, texteProgramme);
            }
            else
            {
                indicePrenomChoisi = aleatoire.Next(0, Donnees.Length - 1);
                prenomSecondePeriode = Donnees[indicePrenomChoisi];
            }

            triRapideSurPrenom(PremierePeriode, 0, PremierePeriode.Length - 1);
            int indicePremiere = rechercheDichotomiquePrenom(PremierePeriode, prenomSecondePeriode.prenom);
            if (indicePremiere != -1)
                prenomPremierePeriode = PremierePeriode[indicePremiere];
            else
                prenomPremierePeriode.nombrePrenom = 0;

            moyennePremierePeriode = calculMoyenne(prenomPremierePeriode, finPremierePeriode - 1900);
            moyenneSecondePeriode = calculMoyenne(prenomSecondePeriode, nbAnneeEnArriere);

            ecartType = calculEcartType(Donnees, finPremierePeriode, moyennePremierePeriode, prenomPremierePeriode);
            ecartMoyenne = moyenneSecondePeriode - moyennePremierePeriode;

            affichageTexte("Trend", texteProgramme, prenomSecondePeriode.prenom, "" + nbAnneeEnArriere);

            if (ecartMoyenne <= (-2 * ecartType))
                affichageTexte("Abandonement", texteProgramme, "" + nbAnneeEnArriere);
            else if (ecartMoyenne < (-ecartType))
                affichageTexte("Outmoded", texteProgramme, "" + nbAnneeEnArriere);
            else if (ecartMoyenne < (ecartType))
                affichageTexte("Hold", texteProgramme, "" + nbAnneeEnArriere);
            else if (ecartMoyenne < (2 * ecartType))
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
        public static double calculMoyenne(Prenom prenom, int nbAnnee)
        {
            double moyenne = prenom.nombrePrenom * 1.0 / nbAnnee * 1.0;

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
        public static double calculEcartType(Prenom[] Donnees, int finPeriode, double moyenne, Prenom prenom)
        {
            double total = 0.0, ecart, variance;

            for (int i = 0; i < Donnees.Length; i++)
            {

                if (prenom.prenom == Donnees[i].prenom && Donnees[i].annee <= finPeriode)
                {

                    ecart = Donnees[i].nombrePrenom * 1.0 - moyenne;
                    total = total + ecart * ecart;
                }
            }

            variance = total / (Donnees.Length * 1.0 - 1);

            return Math.Sqrt(variance);

        }

        /// <summary>
        /// Cette fonction va demander à l'utilisateur de rentrer un prénom mais avec 
        /// quelques modifications pour l'adapter à la question sur la tendance
        /// </summary>
        /// <param name="DonneesPeriode">Les prénoms sur un périodes données</param>
        /// <param name="texteProgramme">Ce tableau contient l'ensemble des Texte 
        /// qui seront afficher</param>>
        /// <returns>va retourner une structure prénom correspondant au prénom choisie par
        /// l'utilisateur</returns>
        public static Prenom demandeChoixPrenomTendance(Prenom[] DonneesPeriode, Texte[] texteProgramme)
        {
            bool prenomOk = false;
            string prenom = "null";
            int indice = 0;

            triRapideSurPrenom(DonneesPeriode, 0, DonneesPeriode.Length - 1);
            while (!prenomOk)
            {
                prenomOk = true;
                try
                {
                    affichageTexte("EnterFirstName", texteProgramme);
                    prenom = Console.ReadLine();
                    prenom = prenom.ToUpper();
                }
                catch
                {
                    affichageTexte("WrongEntry", texteProgramme);
                    prenomOk = false;
                }

                indice = rechercheDichotomiquePrenom(DonneesPeriode, prenom);
                if (indice == -1)
                    affichageTexte("NotFound", texteProgramme);
            }

            if (indice != -1)
                return DonneesPeriode[indice];
            else
            {
                Prenom retour = new Prenom();
                retour.nombrePrenom = 0;
                retour.ordre = 0;
                retour.prenom = prenom;
                return retour;
            }
        }        

    }
}
