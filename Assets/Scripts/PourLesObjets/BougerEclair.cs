using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BougerEclair : MonoBehaviour
{
    private Vector3 positionDepart;
    private Vector3 direction;

    //---------------------------------------------- Variables pour les limiter le mouvement des éclairs.
    private float maxX = 30f;
    private float minX = 10f;
    private float maxZ = 25f;
    private float minZ = -25f;

    // Start is called before the first frame update
    void Start()
    {
        positionDepart = transform.position;

        Invoke("Bouge", 0.5f); // Appelle la méthode pour bouger les éclairs.
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (direction * Time.deltaTime); // Modifie la position des éclairs dans le temps.
    }

    //---------------------------------------------- Pour bouger les éclairs.
    private void Bouge()
    {
        //---------------------------------------------- Variables pour déterminer la range et l'orientation de mouvement des éclairs.
        float directionX = Random.Range(-3.5f, 3.5f); // Mouvement sur l'axe X.
        float directionZ = Random.Range(-3.5f, 3.5f);  // Mouvement sur l'axe Z.

        //---------------------------------------------- Inverse la direction en X de l'éclair, si sa future direction sort des limites de mouvement sur l'axe X des éclairs déclarées.
        if (directionX > 0)
        {
            if (transform.position.x + directionX > maxX)
            {
                directionX *= -1; // Inverse la direction en X.
            }
        }
        else
        {
            if (transform.position.x - directionX < minX)
            {
                directionX *= -1;
            }
        }

        //---------------------------------------------- Inverse la direction en Z de l'éclair, si sa future direction sort des limites de mouvement sur l'axe Z des éclairs déclarées.
        if (directionZ > 0)
        {
            if (transform.position.z + directionZ > maxZ)
            {
                directionZ *= -1; // Inverse la direction en Z.
            }
        }
        else
        {
            if (transform.position.z - directionZ < minZ)
            {
                directionZ *= -1;
            }
        }

        direction = new Vector3(directionX, 0, directionZ); // Établit le nouveau Vector3/la nouvelle position vers laquelle les éclairs vont aller.

        Invoke("Bouge", 2f); //Rappelle la méthode à chaque 2s.
    }


    //---------------------------------------------- Pour vérifier la collision avec le joueur.
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            other.SendMessage("AjouterEnergie", 20); // Envoie la qt d'énergie obtenue au script Personnage du GO personnage.

            switch (gameObject.name) // Vérifie le nom du gameObject qui entre en collision avec le joueur.
            {
                case "Eclair1":
                    Destroy(GameObject.Find("Robot1")); // Détruit le gameObject "Robot" qui suit le gameObject "Éclair" en collision.
                    break;

                case "Eclair2":
                    Destroy(GameObject.Find("Robot2"));
                    break;

                case "Eclair3":
                    Destroy(GameObject.Find("Robot3"));
                    break;

                case "Eclair4":
                    Destroy(GameObject.Find("Robot4"));
                    break;

                case "Eclair5":
                    Destroy(GameObject.Find("Robot5"));
                    break;
            }

            Destroy(gameObject); // Détruir le gameObject qui entre en collision avec le joueur.
        }
    }
}