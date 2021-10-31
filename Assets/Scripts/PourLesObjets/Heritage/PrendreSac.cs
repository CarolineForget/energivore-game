using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrendreSac : DetruireObjets
{
    public GameObject sac;
    public GameObject eclair;

    public GameObject hachePerso;
    public GameObject eclairFinal;

    private int nbPointsAj = 1;


    // Start is called before the first frame update
    void Start()
    {
        nbItems = nbPointsAj; // Initialise le nbItems ajouté lorsque le joueur touche à un item.
    }

    // ------------------------------------------------ Pour vérifier la qt d'item obtenue.

    public void PersoPossedeObjet(int qtObjet)
    {
        if (qtObjet == 1) 
        {
            eclair.SetActive(true); // Affiche le premier éclair.
            sac.SetActive(true); // Affiche le sac sur le personnage.

        } else
        {
            hachePerso.SetActive(true); // Affiche la hache du personnage.
            eclairFinal.SetActive(true); // Affiche l'éclair final.

            GameObject.Find("Personnage").SendMessage("PeutAttaquer", true); // Envoie le message que le personnage peut attaquer au script Personnage du GO personnage.
        }

    }

}
