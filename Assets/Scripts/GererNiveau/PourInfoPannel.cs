using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourInfoPannel : MonoBehaviour
{
    public GameObject[] listeSection; // Crée un tableau pour insérer toutes les sections à partir de l'inspecteur de Unity.
    public GameObject conteneur; // Contient toutes les sections.
    public GameObject mask; // Cache le contenu dans le conteneur.

    private float largeurMask; // Pour contenir la largeur du masque.
    private float posDepart; // Pour contenir la position de départ du conteneur.
    private int noSection = 1; // Pour déterminer la section à afficher.

    public GameObject perso; // Contient le GO personnage.



    // Start is called before the first frame update
    void Start()
    {
        largeurMask = mask.GetComponent<RectTransform>().rect.width; // Initialise la largeur du masque.
        posDepart = conteneur.transform.localPosition.x; // Initialise la position du départ du conteneur.

    }

    // Update is called once per frame
    void Update()
    {

        // Appelle la méthode BougeSlides() si il est possible de déplacer le conteneur et que la section courante n'est pas la dernière.
        if (perso.GetComponent<Personnage>().canMoveSlide && (noSection < listeSection.Length))
        {
            BougeSlides();
        }

    }

    // ---------------------------------------- Fait déplacer le conteneur jusqu'à la prochaine section.

    void BougeSlides()
    {

        float posFinal = posDepart - (noSection * largeurMask + 0.15f); // Calcule la position finale du conteneur en fonction de la différence entre sa position initiale et le produit de noSection et de la largeur du masque.

        // Déplace le conteneur lorsque sa position est plus grande que la position finale.
        if (conteneur.transform.localPosition.x > posFinal)
        {
            conteneur.transform.Translate(-(Time.deltaTime * 4), 0, 0); // Fait déplacer le conteneur.

        } else
        {
            perso.GetComponent<Personnage>().canMoveSlide = false; // Empêche le conteneur de se déplacer.
            noSection++; // Augmente le noSection pour pouvoir passer à la prochaine étape.
        }



    }

}
