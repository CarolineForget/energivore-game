using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Personnage : MonoBehaviour
{
    [Header("Donnees joueur")]

    public CharacterController controller;
    public Animator animPerso;

    //---------------------------------------------- Vitesse de mouvement.
    [Range(1f, 15f)]
    public float vitesseMarche;
    [Range(0f, 3f)]
    public float acceleration;
    private float vitessePerso;

    //---------------------------------------------- Pour le saut.
    [Range(3f, 8f)]
    public float jumpHeight;


    [Header("Donnees environnement")]
    public float gravite = -9.81f;

    //---------------------------------------------- Pour les valeurs à afficher sur le canvas.
    [Header ("Donnees affichees")]
    public int nbEnergie = 0; // La qt d'énergie amassée.
    public int nbItem = 0; // La qt d'item amassée.


    //---------------------------------------------- Pour faire avancer le personnage.
    private float bougeAvant = 0;
    private float bougeCote = 0;

    private Vector3 nouvellePosition;
    private Vector3 velo; // velocity du personnage

    //---------------------------------------------- Pour les mouvements troisiemePersonne.
    public float tempsDeRotation = 0.1f;
    private float adoucirTempsDeRotation;
    public Transform cameraTP;
    public bool is3P = false; // Permet de déterminer si le joueur désire jouer à la 3P.

    public Camera cam1P; // Camera pour la 1P.
    public Camera cam3P; // Camera pour la 3P.

    //---------------------------------------------- Pour gérer les actions.
    private bool isWalking;
    private bool isRunning;

    public bool canAttack = false; // Détermine si le joueur peut attaquer.

    public bool canMoveSlide = false; // Pour déterminer si le panneau peut afficher la prochaine section.


    // Update is called once per frame
    void Update()
    {
        bougeAvant = Input.GetAxisRaw("Vertical"); // Donne la valeur -1, 0 ou 1 à bougeAvant si les touches W/S sont activées.
        bougeCote = Input.GetAxisRaw("Horizontal"); // Donne la valeur -1, 0 ou 1 à bougeCote si les touches A/D sont activées.
        vitessePerso = (Input.GetKey(KeyCode.LeftShift)) ? vitesseMarche * acceleration : vitesseMarche; //Si la touche LeftShift est appuyée, la vitesse de marche est multipliée (vitessePerso), sinon la vitesse de marche reste normale.


        GestionCam();

        // Modifie la façon de déplacer le personnage en fonction de la perspective choisie.
        if (is3P)
        {
            AvanceTroisiemePersonne();
        } else
        {
            AvancePremierePersonne();
        }

        Tombe();
        Saute();
    }

    //---------------------------------------------- Pour modifier la caméra active en fonction de la perspective choisie.

    void GestionCam()
    {
        if (Input.GetKeyDown(KeyCode.F3)) // Détermine si le joueur veut changer de perspective en appuyant F3.
        {
            is3P = (is3P) ? false : true; // Permet de déterminer si le joueur est en 3P ou non. S'il est en 3P, is3P va être modifier pour false pour basculer en 1P.

            if (is3P) //En 3P
            {
                cam1P.enabled = false; //Désactive la caméra 1P.
                GameObject.Find("Main Camera").GetComponent<RotationCamera>().enabled = false; // Désactive le script pour faire tourner la caméra en 1P.
                cam3P.enabled = true; //Active la caméra en 3P.

            } else //En 1P
            {
                cam1P.enabled = true; //Désactive la caméra 1P.
                GameObject.Find("Main Camera").GetComponent<RotationCamera>().enabled = true; // Active le script pour faire tourner la caméra en 1P.
                cam3P.enabled = false; //Désactive la caméra en 3P.
            }
        }

    }

    //---------------------------------------------- Pour faire bouger le personnage à la 3P.

    void AvanceTroisiemePersonne()
    {
           Vector3 direction = new Vector3(bougeCote, 0, bougeAvant).normalized; // .normalized permet de normaliser la vitesse pour que le personnage ne soit pas plus rapide en diagonale.

           if (direction.magnitude >= 0.1f)
           {
               float angleVoulu = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTP.eulerAngles.y; // Pour trouver l'angle de modification pour faire bouger le personnage dans la bonne direction en fonction des touches et de la caméra.
               float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angleVoulu, ref adoucirTempsDeRotation, tempsDeRotation); // Adouci la rotation du personnage.

                transform.rotation = Quaternion.Euler(0, angle, 0); // Faire tourner le personnage dans la bonne direction.

                Vector3 directionMouvement = Quaternion.Euler(0, angleVoulu, 0) * Vector3.forward; 

                controller.Move(directionMouvement * vitessePerso * Time.deltaTime);
           }

           AnimationPersonnage();

    }

    //---------------------------------------------- Pour faire bouger le personnage à la 1P.

    void AvancePremierePersonne()
    {

         nouvellePosition = (transform.right * bougeCote) + (transform.forward * bougeAvant); // Établit la nouvelle position du CharacterController en Vector3.

         controller.Move(nouvellePosition * vitessePerso * Time.deltaTime); // Fait déplacer le personnage vers la nouvelle position en fonction du temps.

         AnimationPersonnage();

    }

    //---------------------------------------------- Pour faire tomber le personnage vers le sol.

    void Tombe()
    {
        velo.y += gravite * Time.deltaTime; // Dirige le personnage vers le sol en modifiant son Vector3. Ex: (0, -0.981f, 0).
        controller.Move(velo * Time.deltaTime); 
    }

    //---------------------------------------------- Pour faire sauter le personnage.

    void Saute()
    {
        if (controller.isGrounded && Input.GetButton("Jump")) // Vérifie que le CharacterController touche le sol et que la touche "Espace" est activée.
        {
            animPerso.SetTrigger("Saute"); // Trigger l'animation de saut du personnage.
            velo.y = jumpHeight; // Modifie la velocity, soit la position en hauteur du personnage.
        }
    }

    //---------------------------------------------- Pour faire l'attaque du personnage.

    public void Attaque()
    {
         animPerso.SetTrigger("Attaque"); // Actuve l'animation d'attaque.

    }

    //---------------------------------------------- Pour déterminer si le personnage peut attaquer.

    public void PeutAttaquer(bool peutAtt)
    {
        canAttack = peutAtt;
    }

    //---------------------------------------------- Pour gérer les animations de marche et de course.

    void AnimationPersonnage()
    {
        isWalking = (bougeAvant != 0 || bougeCote != 0) ? true : false; // Le personnage marche (isWalking = true) lorsque les touches WASD sont activées.
        isRunning = (Input.GetKey(KeyCode.LeftShift)) ? true : false; // Le personnage court (isRunning = true) lorsque LeftShift est activé.

        animPerso.SetBool("Walk", isWalking); // Active/Désactive l'animation de marche.
        animPerso.SetBool("Run", isRunning); // Active/Désactive l'animation de course.
    }

    //---------------------------------------------- Pour modifier le nombre d'énergie affiché.

    public void AjouterEnergie(int nbPoints)
    {
        nbEnergie += nbPoints; // Augmente la qt d'énergie en fonction de nbPoints reçu.
        canMoveSlide = true; // Permet à la prochaine section d'être affichée.
        GameObject.Find("NbEnergie").GetComponent<Text>().text = "Énergie : " + nbEnergie.ToString() + " %"; // Modifie le texte montrant la quantité d'énergie accumulée en fonction du nbPoints accumulés en touchant des éclairs.
    }

    //---------------------------------------------- Pour modifier le nombre d'objets affichés.

    public void AjouterItem(int nbPoints)
    {
        nbItem += nbPoints; // Augmente la qt d'items en fonction de nbPoints reçu.
        canMoveSlide = true; // Permet à la prochaine section d'être affichée.
        GameObject.Find("NbObjets").GetComponent<Text>().text = "Inventaire : " + nbItem.ToString() + " %"; // Modifie le texte montrant la quantité d'objets accumulés en fonction du nbPoints accumulés en ramassant les objets.
    }

}
