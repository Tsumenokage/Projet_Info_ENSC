using System ;
using System.IO ; // necessaire pour la manipulation de fichers

namespace Projet{

public class ReduireFichier
{

public static void reduireFichier(string fichier_source, int ratio, string fichier_cible) 
{ 
  try 
  { 
    // Création d'une instance de StreamReader pour permettre la lecture de notre fichier source 
    System.Text.Encoding   encoding = System.Text.Encoding.GetEncoding(  "iso-8859-1"  );
    StreamReader monStreamReader = new StreamReader(fichier_source,encoding); 

    // Création d'une instance de StreamWriter pour permettre l'ecriture de notre fichier cible
    StreamWriter monStreamWriter = File.CreateText(fichier_cible); 



    int nbMots = 0 ;
    string mot = monStreamReader.ReadLine(); 

    // Lecture de tous les mots du fichier (un par lignes) 
    while (mot != null) 
    { 
      nbMots++ ;
      if (nbMots%ratio == 0)
       monStreamWriter.WriteLine(mot) ;
     mot = monStreamReader.ReadLine();

    } 
    // Fermeture du StreamReader (attention très important) 
    monStreamReader.Close(); 
    // Fermeture du StreamWriter (attention très important) 
    monStreamWriter.Close(); 
  } 
  catch (Exception ex) 
  { 
    // Code exécuté en cas d'exception 
    Console.Write("Une erreur est survenue au cours de l'opération :"); 
    Console.WriteLine(ex.Message); 
  } 
}

public static int Main(string[] arguments)
{

  if (arguments.Length !=3  )
  {
  Console.WriteLine(@"
But :
      Ce programme lit un fichier texte et n'en retient qu'une ligne sur n pour créer un nouveau fichier.
      En sortie, le fichier créé est donc une réduction du fichier d'entrée d'un certain ratio.

Utilisation :
      mono reduireFichier.exe nom_fichier_source ratio nom_fichier_cible

") ; 
  return 1 ;
} 	 

    reduireFichier(arguments[0], Int32.Parse(arguments[1]), arguments[2] ) ;

return 0 ;
}


} // end of class

} // end of namespace
