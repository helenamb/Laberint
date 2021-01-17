using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ----------------------------------------------------------------------------------
/// DESCRIPCIÓ
///         Script utilitzat per a controlar el menú principal del joc
/// AUTOR:  Helena Martí
/// DATA:   7/1/2021
/// VERSIÓ: 1.0
/// CONTROL DE VERSIONS
///         1.0: Tecles establertes, amb les seves respectives funcions.
///         2.0: Addició de sons.
/// ----------------------------------------------------------------------------------
/// </summary>


public class ScrMenúPrincipal : MonoBehaviour
{
    [SerializeField] Image controls, sortir;
    [SerializeField] Text escapeControls;

    public AudioClip soOpcio;

    public float volum;

    public bool sortint, llegintcontrols;

    void Start()
    {
        // Desactiva les diverses pantalles de les opcions a les quals tenim accés en el menú principal. 

        sortir.enabled = false;
        controls.enabled = false;
        escapeControls.enabled = false;
        
        sortint = false;
        llegintcontrols = false;
    }

    void Update()
    {
        OpcionsMenu();
    }

    void OpcionsMenu()
    {
        // En prémer una tecla, activem una opció.

        if (Input.GetKeyDown(KeyCode.J)) Jugar();   

        if (Input.GetKeyDown(KeyCode.C)) Controls();

        if (Input.GetKeyDown(KeyCode.W)) Web();

        if (Input.GetKeyDown(KeyCode.S)) Sortir();

        if (sortint && Input.GetKeyDown(KeyCode.Escape))    // Si hem premut S i premem Escape, tornem al menú.
        {
            AudioSource.PlayClipAtPoint(soOpcio, Camera.main.transform.position, volum);

            sortint = false;
            sortir.enabled = false;
        }

        if (sortint && Input.GetKeyDown(KeyCode.Return))    // Si hem premut S i premem Enter, tanquem l'aplicació.
        {
            AudioSource.PlayClipAtPoint(soOpcio, Camera.main.transform.position, volum);

            Application.Quit();
        }

        if (llegintcontrols && Input.GetKeyDown(KeyCode.Escape))    // Si hem premut C i premem Escape, tornem al menú.
        {
            AudioSource.PlayClipAtPoint(soOpcio, Camera.main.transform.position, volum);

            controls.enabled = false;
            llegintcontrols = false;
            escapeControls.enabled = false;
        }
    }

    public void Jugar()
    {
        // Carrega l'escena 1 (el primer nivell).

        AudioSource.PlayClipAtPoint(soOpcio, Camera.main.transform.position, volum);

        SceneManager.LoadScene(1);
    }

    void Controls()
    {
        // Activa la pantalla amb la informació sobre els controls.

        AudioSource.PlayClipAtPoint(soOpcio, Camera.main.transform.position, volum);

        controls.enabled = true;
        llegintcontrols = true;
        escapeControls.enabled = true;
    }

    void Web()
    {
        // Carrega un enllaç extern.

        AudioSource.PlayClipAtPoint(soOpcio, Camera.main.transform.position, volum);

        Application.OpenURL("https://www.linkedin.com/in/helenamarti/");
    }

    void Sortir()
    {
        // Activa la pantalla de confirmació de la sortida. 

        AudioSource.PlayClipAtPoint(soOpcio, Camera.main.transform.position, volum);

        sortir.enabled = true;
        sortint = true;
    }

}
