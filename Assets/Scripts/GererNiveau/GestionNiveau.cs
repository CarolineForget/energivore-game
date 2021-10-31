using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionNiveau : MonoBehaviour
{
    private Scene scene; // Pour contenir la scène courante.
    public GameObject menu; // Pour contenir le panneauMenu.

    private bool musicPlaying = true; // Pour déterminer si la musique d'ambiance joue.

    public GameObject perso; // Pour contenir le GO personnage.

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene(); // Contient la scène courante.
    }

    // Update is called once per frame
    void Update()
    {

        switch (scene.name)
        {
            case "LeLaboratoire_1": // Détermine si la scène est le niveau 1.
                GererNiveau1();
                break;

            case "Niveau-2": // Détermine si la scène est le niveau 2.
                OuvrirMenu();
                FermerMusique();
                RetourPanneau();
                break;
        }

    }

    //---------------------------------------------- Permet l'accès au disjoncteur dans le niveau 1 lorsque tous les éclairs ont été collectés.

    void GererNiveau1()
    {
        if (GameObject.Find("Personnage").GetComponent<Personnage>().nbEnergie >= 100) 
        {
            GameObject.Find("Disjoncteur_couvercle").GetComponent<Animator>().enabled = true;
        }
    }

    //---------------------------------------------- Permet d'ouvrir le menu pour les commandes.

    void OuvrirMenu()
    {
        if (Input.GetKey(KeyCode.F1)) // Affiche le menu seulement lorsque la touche F1 est appuyée.
        {
            menu.SetActive(true); // Affiche le menu.
        } else
        {
            menu.SetActive(false); // Cache le menu.
        }
    }

    //---------------------------------------------- Permet de fermer la musique d'ambiance.

    void FermerMusique()
    {
        if (Input.GetKeyDown(KeyCode.F4)) // Si la touche F4 est appuyée.
        {
            if (musicPlaying) // Si la musique joue.
            {
                GameObject.Find("MusiqueAmbiance").GetComponent<AudioSource>().enabled = false; // Désactive la musique d'ambiance.
                musicPlaying = false; // Détermine que la musique ne joue plus.

            } else // Si la musique ne joue pas.
            {
                GameObject.Find("MusiqueAmbiance").GetComponent<AudioSource>().enabled = true; // Active la musique d'ambiance.
                musicPlaying = true; // Détermine que la musique joue.
            }
        } 
    }

    //---------------------------------------------- Permet de charger les niveaux en fonction du niveau courant. Ex: Si le joueur est à la scène intermédiare, la prochaine scène chargée sera celle du niveau 2.

    public void ChangerNiveau(int niveau)
    {
        switch (niveau)
        {
            case 0: 
                SceneManager.LoadScene("LeLaboratoire_1");
                break;

            case 1: 
                SceneManager.LoadScene("Scene_Inter");
                break;

            case 2: 
                SceneManager.LoadScene("Niveau-2");
                break;

            case 3: 
                SceneManager.LoadScene("Scene_Fin");
                break;
        }
    }

    //---------------------------------------------- Permet de retourner au panneau d'information.

    private void RetourPanneau()
    {
        if (Input.GetKey(KeyCode.F2)) // Vérifie si la touche appuyée est F2.
        {
            perso.transform.position = new Vector3(-1.5f, 1.5f, 1.1f); // Modifie la position du personnage.
            perso.GetComponent<Personnage>().enabled = false; // Désactive la capacité du joueur de bouger le personnage. (le script)

        }
        else
        {
            perso.GetComponent<Personnage>().enabled = true; // Active la capacité du joueur de bouger le personnage. (le script)
        }
    }


    

}
