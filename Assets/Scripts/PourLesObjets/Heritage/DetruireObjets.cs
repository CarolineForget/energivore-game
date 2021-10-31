using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetruireObjets : MonoBehaviour
{
    protected int nbEnergie;
    protected int nbItems;

    static int nbItemTotal = 0;
    static int nbEnergieTotal = 0;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (gameObject.tag == "Objet") // Si le personnage touche aux éclairs.
            {
                nbEnergieTotal += nbEnergie; // Augmente la qt d'énergie totale.

                gameObject.SendMessage("QtEnergie", nbEnergieTotal); // Envoie la qt d'énergie totale au script PrenderEclairs.
                other.SendMessage("AjouterEnergie", nbEnergie); // Envoie la qt d'énergie obtenue au script Personnage du GO personnage.

            }
            else // Si le personnage touche aux items.
            {
                nbItemTotal += nbItems; // Augmente la qt d'items totale.

                gameObject.SendMessage("PersoPossedeObjet", nbItemTotal); // Envoie la qt d'items totale au script PrenderSac.
                other.SendMessage("AjouterItem", nbItems); // Envoie la qt d'items obtenue au script Personnage du GO personnage.
            }

            Destroy(gameObject); // Détruit le GO en collision avec le personnage.
        }
    }
}
