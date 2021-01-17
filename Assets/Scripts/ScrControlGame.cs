using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ----------------------------------------------------------------------------------
/// DESCRIPCIÓ
///         Script utilitzat per a gestionar les accions del joc
/// AUTOR:  Helena Martí
/// DATA:   31/12/2020
/// VERSIÓ: 1.0
/// CONTROL DE VERSIONS
///         1.0: Inicialització de les variables que apareixen en la UI.
///         2.0: Tecles especials per a controlar accions del joc.
///         3.0: Control de canvis en la UI (victòria, derrota, etc.).
/// ----------------------------------------------------------------------------------
/// </summary>

public class ScrControlGame : MonoBehaviour
{
    [SerializeField] Image morir, guanyar, exit;
    
    public int punts = 0;
    public int vida = 5;
    public int pickups = 0;
    public int pickupsMenjats = 0;

    public bool nivellAcabat, morint, victoria, exiting;

    [SerializeField] AudioSource musicaFons;    //so ambient

    private void Start()
    {
        // En començar, desactiva tots els elements del canvas.

        morir.enabled = false;
        guanyar.enabled = false;
        exit.enabled = false;
        
        nivellAcabat = false;
        morint = false;
        victoria = false;
        exiting = false;
    }

    private void Update()
    {
        ControlEntradaUsuari();

        if (vida <= 0) Morir(); // Controla el Game Over.
    }

    void ControlEntradaUsuari()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Sortir(); // Controla l'activació del menú de pausa.

        if (nivellAcabat && Input.GetKeyDown(KeyCode.Return)) CanviEscena();    // En acabar el nivell, si fem Enter passem al següent nivell.

        if (morint && Input.GetKeyDown(KeyCode.Return)) SceneManager.LoadScene(0);  // En morir, si fem Enter tornem al menú principal.

        if (victoria) Guanyar();    // Controla l'activació de la pantalla de victòria.

        if (victoria && Input.GetKeyDown(KeyCode.Return)) SceneManager.LoadScene(0);    // En guanyar, l'Enter porta al menú principal.

        if (exiting && Input.GetKeyDown(KeyCode.Return)) SceneManager.LoadScene(0); // En el menú de pausa, l'Enter porta al menú principal.

    }

    void Sortir()
    {
        // Controla l'aparició de la pantalla de pausa.
        
        if (exiting)
        {
            exit.enabled = false;
            exiting = false;
        }

        else
        {
            exit.enabled = true;
            exiting = true;
        }
    }

    public void CanviEscena()
    {
        // Carrega l'escena següent a l'actual, si n'hi ha.

        if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void Morir()
    {
        // Controla l'aparició de la pantalla de Game Over.

        morint = true;
        morir.enabled = true;
    }

    void Guanyar()
    {
        // Controla l'aparició de la pantalla de victòria.

        guanyar.enabled = true;
    }
}