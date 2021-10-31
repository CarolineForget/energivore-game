using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BougerEnnemi : MonoBehaviour
{
    private Transform positionDepart;
    private Transform positionFinale;

    public float ralentissement = 5f;
    private Vector3 velo = Vector3.zero;

    private GameObject robot1;
    private GameObject robot2;
    private GameObject robot3;
    private GameObject robot4;
    private GameObject robot5;

    // Start is called before the first frame update
    void Start()
    {
        //---------------------------------------------- Pour trouver et assigner chaque robot créé à sa variable GameObject.
        robot1 = GameObject.Find("Robot1");
        robot2 = GameObject.Find("Robot2");
        robot3 = GameObject.Find("Robot3");
        robot4 = GameObject.Find("Robot4");
        robot5 = GameObject.Find("Robot5");
    }

    // Update is called once per frame
    void Update()
    {
        Bouge();
    }

    //---------------------------------------------- Pour faire bouger les robots.
    void Bouge()
    {
        //---------------------------------------------- Pour sélectionner des GameObjects en particulier en fonction de leur nom.
        switch (gameObject.name)
        {
            case "Robot1":
                positionDepart = robot1.transform; // Détermine la position de départ.
                positionFinale = GameObject.Find("Eclair1").transform; // Détermine la position finale.
                robot1.transform.position = Vector3.SmoothDamp(positionDepart.position, positionFinale.position, ref velo, ralentissement); // Fait bouger le GameObject.
                robot1.transform.LookAt(GameObject.Find("Eclair1").transform); // Modifie la rotation du GameObject pour l'orienter vers la position finale, soit la cible.
                break;

            case "Robot2":
                positionDepart = robot2.transform;
                positionFinale = GameObject.Find("Eclair2").transform;
                robot2.transform.position = Vector3.SmoothDamp(positionDepart.position, positionFinale.position, ref velo, ralentissement);
                robot2.transform.LookAt(GameObject.Find("Eclair2").transform);
                break;

            case "Robot3":
                positionDepart = robot3.transform;
                positionFinale = GameObject.Find("Eclair3").transform;
                robot3.transform.position = Vector3.SmoothDamp(positionDepart.position, positionFinale.position, ref velo, ralentissement);
                robot3.transform.LookAt(GameObject.Find("Eclair3").transform);
                break;

            case "Robot4":
                positionDepart = robot4.transform;
                positionFinale = GameObject.Find("Eclair4").transform;
                robot4.transform.position = Vector3.SmoothDamp(positionDepart.position, positionFinale.position, ref velo, ralentissement);
                robot4.transform.LookAt(GameObject.Find("Eclair4").transform);
                break;

            case "Robot5":
                positionDepart = robot5.transform;
                positionFinale = GameObject.Find("Eclair5").transform;
                robot5.transform.position = Vector3.SmoothDamp(positionDepart.position, positionFinale.position, ref velo, ralentissement);
                robot5.transform.LookAt(GameObject.Find("Eclair5").transform);
                break;
        }
    }


    //---------------------------------------------- Pour vérifier la collision avec le joueur.
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Réinitialise la scène courante en la chargeant de nouveau.
        }
    }

}
