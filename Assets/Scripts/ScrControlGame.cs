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

    public bool nivellAcabat, morint, victoria, exiting;

    [SerializeField] AudioSource musicaFons;    //so ambient

    private void Start()
    {
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

        if (vida <= 0) Morir();
    }

    void ControlEntradaUsuari()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Sortir();

        if (nivellAcabat && Input.GetKeyDown(KeyCode.Return)) CanviEscena();

        if (morint && Input.GetKeyDown(KeyCode.Return)) SceneManager.LoadScene(0);

        if (victoria) Guanyar();

        if (victoria && Input.GetKeyDown(KeyCode.Return)) SceneManager.LoadScene(0);

        if (exiting && Input.GetKeyDown(KeyCode.Return)) SceneManager.LoadScene(0);

    }

    void Sortir()
    {
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
        if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void Morir()
    {
        morint = true;
        morir.enabled = true;
    }

    void Guanyar()
    {
        guanyar.enabled = true;
    }
}