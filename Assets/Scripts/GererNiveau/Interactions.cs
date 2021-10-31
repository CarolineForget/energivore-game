using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interactions : MonoBehaviour
{
    //--------------------------------------------- Pour gérer les scènes.
    private GameObject currentGo; // Pour contenir le GO touché.

    private Scene scene; // Pour contenir la scène courante.
    private int noNiveau = 0; // Pour déterminer la position du niveau courant à travers les scènes.

    //--------------------------------------------- POUR LE NIVEAU 1

    private bool jaugePleine = false;
    private bool intCamion = false;
    private bool camionDemarre = false;

    public GameObject perso;
    public GameObject camion;

    public Behaviour haloPhare1; // Component Halo des phares avant pour créer un effet de lumière.
    public Behaviour haloPhare2;

    private Vector3 positionSortiCamion;

    //--------------------------------------------- POUR LE NIVEAU 2

    public Camera cam; // Pour contenir la Main Camera.
    private bool partieTerminee = false; // Pour déterminer si tous les objectifs ont été remplis.

    // Start is called before the first frame update
    void Start()
    {
        positionSortiCamion = new Vector3(-6.8f, -18.41f, -3.55f); // Initialise la position vers laquelle le joueur sera lorsqu'il sort du camion.
        
        scene = SceneManager.GetActiveScene(); // Contient la scène courante.

        switch (scene.name) // Pour déterminer le no de la scène.
        {
            case "LeLaboratoire_1":
                noNiveau = 1;
                break;

            case "Niveau-2":
                noNiveau = 3;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ClickSouris();
        SortirDuCamion();
        BougerLeCamion();

    }

    //---------------------------------------------- Pour détecter les collisions faites par la souris lors du click de la souris.
    void ClickSouris()
    {
        if (Input.GetMouseButtonDown(0)) // Détecte le click gauche de la souris.
        {
            RaycastHit hit; // Détermine le gameObject avec lequel le rayon de la souris fait une collision.
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);  // Crée un rayon à partir de la pointe du curseur de la souris.

            if (Physics.Raycast(ray, out hit, 100.0f)) // Détecte la collision entre le Raycast et le collider d'un gameObject.
            {
                if (hit.transform != null)
                {
                    currentGo = hit.transform.gameObject; // Conserve le GO sélectionné.

                    // Détermine les conditions à vérifier en fonction de la scène courante.
                    switch (scene.name)
                    {
                        case "LeLaboratoire_1":
                            ConditionN1();
                            break;

                        case "Niveau-2":
                            ConditionN2();
                            break;
                    }
                    

                }
            }

        }

    }

    //------------------------------------------- CONDITIONS POUR LE NIVEAU 1

    void ConditionN1()
    {
        switch (currentGo.name) // Vérifie le nom du gameObject touché pour appeler les méthodes désirées.
        {
            case "Disjoncteur_manette":
                ActiverManette();
                break;

            case "Manette.001":
                AccesCamion();
                break;

            case "Volant_Interactif":
                RemplirJauge();
                break;

            case "Chaise.001":
                DansCamion();
                break;

            case "Interrupteur_Phare":
                PartirCamion();
                break;
        }
    }

    //------------------------------------------- CONDITIONS POUR LE NIVEAU 2

    void ConditionN2()
    {

        // Vérifie le tag du GO sélectionné.
        if (currentGo.tag == "Tonneau" && perso.GetComponent<Personnage>().canAttack) // Si c'est un tonneau et que le personnage peut attaquer.
        {
            perso.GetComponent<Personnage>().Attaque(); // Fait attaquer le personnage.
            partieTerminee = true; // Indique que toutes les étapes sont accomplies.
            Destroy(currentGo); // Détruit le tonneau (currentGo).
        }


        if (currentGo.tag == "Chaise" && partieTerminee) // Si c'est le camion et que la partie est terminée.
        {
            perso.GetComponent<CharacterController>().enabled = false;
            DansCamion();
        }
    }




    //---------------------------------------------- Pour sortir du camion lorsque le joueur est à l'intérieur.

    void SortirDuCamion()
    {
        if (Input.GetKeyDown(KeyCode.E) && intCamion)
        {
            perso.transform.position = positionSortiCamion; // Change la position actuelle du joueur pour la position de sortie établie dans Start();
            perso.GetComponent<Personnage>().enabled = true; // Active le script "Personnage" du joueur pour lui permettre de bouger.
            intCamion = false;
        }
    }

    //---------------------------------------------- Pour bouger le camion avec le personnage vers l'avant lorsque toutes les étapes sont terminées (camionDemarre == true).

    void BougerLeCamion()
    {
        if (camionDemarre)
        {
            camion.transform.position = camion.transform.position + new Vector3(5 * Time.deltaTime, 0, 7.5f * Time.deltaTime); 
            perso.transform.position = perso.transform.position + new Vector3(5 * Time.deltaTime, 0, 7.5f * Time.deltaTime);

        }
    }

    //---------------------------------------------- Pour activer l'animation/l'accès au panneau de contrôle.

    void ActiverManette()
    {
        GameObject.Find("Disjoncteur_manette").GetComponent<Animator>().enabled = true; // Active l.animation de la manette du disjoncteur.

        foreach (Transform child in GameObject.Find("PanneauCtrl_Empty").transform)
        {
            for (int i = 0; i < child.transform.childCount; i++) // Sélectionne tous les enfants du gameObject "PanneauCtrl_Empty";
            {
                Transform parent = child.transform.GetChild(i).parent; // Sélectionne les premiers enfants.

                if (parent.gameObject.tag == "Animation")
                {
                    parent.gameObject.GetComponent<Animator>().enabled = true; // Active les animations pour le panneau de contrôle pour les gameObjects avec le tag "Animation".
                }

            }
        }

    }

    //---------------------------------------------- Pour activer le labyrinthe/l'accès au camion.

    void AccesCamion()
    {
        GameObject.Find("PlancherPlein").SetActive(false); // Désactive le plancher plein, pour ne laisser que le plancher troué.
        GameObject.Find("Caisson_Bas").GetComponent<Animator>().enabled = true; // Active l'animation pour enlever le caisson autour du camion.
        GameObject.Find("Caisson_Haut").GetComponent<Animator>().enabled = true;
    }

    //---------------------------------------------- Pour remplir la jauge d'énergie du camion.

    void RemplirJauge()
    {
        if (!jaugePleine)
        {
            GameObject.Find("Volant_Interactif").GetComponent<Animator>().enabled = true; // Animation pour tourner la vanne de la jauge.
            GameObject.Find("RemplissageJauge").GetComponent<Animator>().enabled = true; // Animation pour remplir la jauge principale.
            GameObject.Find("RemplissageJauge.001").GetComponent<Animator>().enabled = true; // Animation pour remplir la jauge secondaire.
            jaugePleine = true; // Établit que la jauge d'énergie et pleine.
            GameObject.Find("Alarme").GetComponent<Animator>().SetBool("JaugePleine", jaugePleine); // Désactive l'animation de l'alarme activée.
            GameObject.Find("NbEnergie").GetComponent<Text>().text = "Énergie : 0"; // Modifie le texte montrant la quantité d'énergie accumulée pour 0.
        }
    }

    //---------------------------------------------- Pour faire démarrer le camion lorsque le personnage est à l'intérieur de celui-ci.

    void PartirCamion()
    {
        if (jaugePleine && intCamion)
        {
            GameObject.Find("Interrupteur_Phare").GetComponent<Animator>().enabled = true; // Active l'animation de l'interrupteur des phares.
            haloPhare1.enabled = true; // Active les halos des phares avant.
            haloPhare2.enabled = true;
            camionDemarre = true; // Établit que le camion est démarré.
            Invoke("ChangerScene", 1f); // Change la scène après 1s.
        }
    }

    //---------------------------------------------- Pour faire rentrer le personnage dans le camion s'il n'est pas à l'intérieur.

    void DansCamion()
    {
        if (!intCamion)
        {
            intCamion = true; // Établit que le personnage est à l'intérieur.
            perso.GetComponent<Personnage>().enabled = false; // Désactive le script pour bouger le personnage.
            perso.transform.position = GameObject.Find("Chaise.001").transform.position; // Modifie la position du joueur pour qu'il soit à l'origine de la chaise de la cabine.

            
            if (noNiveau == 3)
            {
               camionDemarre = true; // Établit que le camion est démarré.
               Invoke("ChangerScene", 1f); // Change la scène après 1s.
            }
        }
    }

    //---------------------------------------------- Pour changer de scène.

    void ChangerScene()
    {
        gameObject.SendMessage("ChangerNiveau", noNiveau); // Envoi la position courante de la scène au script GestionNiveau pour charger la prochaine scène.
    }

}
