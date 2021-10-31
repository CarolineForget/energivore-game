using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCamera : MonoBehaviour
{
    //----------------------------------- Pour la rotation de la caméra de haut en bas.
    [Range(1f, 5f)]
    public float pourRotation = 1f;
    private float sourisY = 0;

    //----------------------------------- Pour la rotation du personnage et de la caméra de gauche à droite.
    public GameObject personnage;

    [Range(1f, 5f)]
    public float pourRotationPerso = 1f;
    private float sourisX = 0;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined; // Confine le curseur dans la fenêtre de jeu. Il faut toucher escape pour en sortir.

    }

    // Update is called once per frame
    void Update()
    {
        Tourne();
    }

    void Tourne()
    {
        sourisX = Input.GetAxis("Mouse X");
        sourisY = Input.GetAxis("Mouse Y");

        //------------------------------- Pour la caméra.
        pourRotation += sourisY;
        pourRotation = Mathf.Clamp(pourRotation, -55, 50); // Pour restreindre la rotation en hauteur de la caméra. Ex: (variable à restreindre, valeur max., valeur min.).

        Vector3 rotationEnEuleur = transform.eulerAngles; // Prend toutes les valeurs de rotation de la caméra.
        rotationEnEuleur.x = -pourRotation; // Modifie la valeur de rotation X de la caméra.

        transform.rotation = Quaternion.Euler(rotationEnEuleur); // Empêche le gimbal lock.

        //------------------------------- Pour le personnage.
        pourRotationPerso += sourisX;

        Vector3 rotationPersoEnEuler = personnage.transform.eulerAngles; // Prend toutes les valeurs de rotation du GameObject personnage.
        rotationPersoEnEuler.y = pourRotationPerso; // Modifie la valeur de rotation Y du GameObject personnage.

        personnage.transform.rotation = Quaternion.Euler(rotationPersoEnEuler); // Empêche le gimbal lock.
    }
}

