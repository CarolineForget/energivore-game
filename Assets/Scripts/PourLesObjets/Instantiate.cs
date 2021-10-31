using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    public GameObject parent; // Contenant dans lequel se trouveront les instances des GameObjects créés.

    public Transform eclairPfb; 
    private Transform eclair;

    public Transform robotPfb;
    private Transform robot;

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 1; i <= 5; i++) 
        {
            //---------------------------------------------- Pour créer des éclairs sur la scène.

            eclair = Instantiate(eclairPfb, new Vector3(Random.Range(10f, 30f), -18.4f, Random.Range(-20f, 20f)), Quaternion.identity, parent.transform); // Initialise la position des éclairs à des endroits aléatoires de la scène.
            eclair.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0)); // Initialise la rotation des éclairs.
            eclair.name = "Eclair" + i; // Modifie le nom des GameObjects "Eclair" en fonction de i

            //---------------------------------------------- Pour créer des robots sur la scène.

            robot = Instantiate(robotPfb, new Vector3(Random.Range(10f, 30f), -18.4f, Random.Range(-20f, 20f)), Quaternion.identity, parent.transform); // Initialise la position des robots à des endroits aléatoires de la scène.
            robot.localRotation = Quaternion.Euler(new Vector3(0, 0, 0)); // Initialise la rotation des robots.
            robot.name = "Robot" + i; // Modifie le nom des GameObjects "Robot" en fonction de i

        }

    }

}
