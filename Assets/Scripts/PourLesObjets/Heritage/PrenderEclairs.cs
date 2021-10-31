using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrenderEclairs : DetruireObjets
{
    public GameObject eclair;
    public GameObject hache;

    private int nbPointsAj = 50;

    // Start is called before the first frame update
    void Start()
    {
        nbEnergie = nbPointsAj; // Initialise le nbEnergie ajouté lorsque le joueur touche à un éclair.
    }

    // ------------------------------------------------ Pour vérifier la qt d'énergie obtenue.

    public void QtEnergie(int qtEnergie)
    {
        if (qtEnergie == 50)
        {
            hache.SetActive(true); // Affiche la hache.

        }
    }

}
