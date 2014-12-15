using System ;
using System.IO ; // necessaire pour la manipulation de fichers

namespace Projet{

public class CompteMots
{

public static void ouvrirFichier(string fichier) 
{ 
  try 
  { 
    Console.WriteLine("\nOuverture du fichier : "+fichier) ;

    // Création d'une instance de StreamReader pour permettre la lecture de notre fichier 
    System.Text.Encoding   encoding = System.Text.Encoding.GetEncoding(  "iso-8859-1"  );
    StreamReader monStreamReader = new StreamReader(fichier,encoding); 

    int nbMots = 0 ;
    int tailleMin = 100 ;
    int tailleMax = 0 ;

    Console.Write("Lecture du dictionnaire, veuillez patienter...") ;
    
    string mot = monStreamReader.ReadLine(); 
    Console.WriteLine(mot);
    // Lecture de tous les mots du dictionnaire (un par lignes) 
    while (mot != null) 
    { 
      nbMots++ ;
    
     if (mot.Length < tailleMin) tailleMin = mot.Length ;
     if (mot.Length > tailleMax) tailleMax = mot.Length ;

     mot = monStreamReader.ReadLine();

    } 
    // Fermeture du StreamReader (attention très important) 
    monStreamReader.Close(); 

	Console.WiteLine("\n{0} mots lus dans le fichier {1}", nbMots, fichier) ;
	Console.WriteLine("\nLe mot le plus court fait {0} lettre(s).",tailleMin) ;
    Console.WriteLine("\nLe mot le plus  long fait {0} lettre(s).",tailleMax) ;

  } 
  catch (Exception ex) 
  { 
    // Code exécuté en cas d'exception 
    Console.Write("Une erreur est survenue au cours de la lecture :"); 
    Console.WriteLine(ex.Message); 
  } 
}

public static int Main(string[] arguments)
{

  if (arguments.Length == 0 )
  Console.WriteLine(@"

But :
      ce programme ouvre tous les fichiers indiqués en paramètres. Il 
      s'agit obligatoirement de fichiers textes.
      Il indique pour chacun d'eux le nombre de mots trouvés et les
      tailles du mot le plus court et du mot le plus long.

Utilisation :
    mono compteMots.exe nom_fichier_1.txt [nom_fichier_2.txt...]

") ; 

  for (int i = 0 ; i < arguments.Length ; i++)
    ouvrirFichier(arguments[i]) ;

return 0 ;
}


} // end of class

} // end of namespace
