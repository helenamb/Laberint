using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ----------------------------------------------------------------------------------
/// DESCRIPCIÓ
///         Script utilitzat per al control del Player
/// AUTOR:  Helena Martí Barragán
/// DATA:   30/11/2020
/// VERSIÓ: 1.0
/// CONTROL DE VERSIONS
///         1.0: Moviment del player en els eixos xy, col·lisió i destrucció pickups.
///         2.0: Sumar punts en col·lisionar amb els pickups.
///         3.0: Controlar la col·lisió amb les claus de les portes i el que fan.
///         4.0: Controlar les col·lisions amb els enemics i altres objectes que et treuen vida. Control del fi del joc. 
///         5.0: Addició de sons.
/// ----------------------------------------------------------------------------------
/// </summary>
    
public class ScrPlayer : MonoBehaviour
{
    float x, y; // variables per a guardar la lectura dels cursors

    public float volumGeneral, volumPickups;

    [SerializeField] float forsa = 10;
    [SerializeField] GameObject portaVermella, portaBlava, portaVerda;
    [SerializeField] Image nivellComplert;

    Rigidbody2D rb;
    ScrPickup scrP;
    ScrControlGame scrCG;

    public AudioClip soParetNormal, soParetXunga, soPickup, soSuperPickup, soVirus, soClau;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        scrCG = GameObject.Find("Game Manager").GetComponent<ScrControlGame>();

        nivellComplert.enabled = false;
    }

    private void FixedUpdate()
    {
        if (!scrCG.nivellAcabat)
        {
            if (!scrCG.morint)
            {
               if (!scrCG.victoria)
               {
                    if (!scrCG.exiting)
                    {
                        PlayerInput();
                    }
               }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup"))
        {
            AudioSource.PlayClipAtPoint(soPickup, Camera.main.transform.position, volumPickups);

            scrP = collision.GetComponent<ScrPickup>();
            scrCG.punts += scrP.valor;
            Destroy(collision.gameObject);
            scrCG.pickups--;
        }

        if (collision.CompareTag("SuperPickup"))
        {
            AudioSource.PlayClipAtPoint(soSuperPickup, Camera.main.transform.position, volumPickups);

            scrP = collision.GetComponent<ScrPickup>();
            scrCG.punts += scrP.valor;
            Destroy(collision.gameObject);
            scrCG.pickups--;
        }


        if (collision.CompareTag("ClauVermella"))
        {
            AudioSource.PlayClipAtPoint(soClau, Camera.main.transform.position, volumGeneral);

            portaVermella.GetComponent<ScrPorta>().ObrirPorta();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("ClauVerda"))
        {
            AudioSource.PlayClipAtPoint(soClau, Camera.main.transform.position, volumGeneral);

            portaVerda.GetComponent<ScrPorta>().ObrirPorta();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("ClauBlava"))
        {
            AudioSource.PlayClipAtPoint(soClau, Camera.main.transform.position, volumGeneral);

            portaBlava.GetComponent<ScrPorta>().ObrirPorta();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Virus"))
        {
            AudioSource.PlayClipAtPoint(soClau, Camera.main.transform.position, volumGeneral);

            scrCG.vida--;
            if (scrCG.vida < 0) scrCG.vida = 0;

            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Sortida"))
        {
            nivellComplert.enabled = true;
            scrCG.nivellAcabat = true;
        }

        if (collision.CompareTag("Victoria"))
        {
            scrCG.victoria = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ParetElectrica"))
        {
            AudioSource.PlayClipAtPoint(soParetXunga, Camera.main.transform.position, volumGeneral);
            
            scrCG.vida--;
            if (scrCG.vida < 0) scrCG.vida = 0;
        }

        if (collision.gameObject.CompareTag("ParetNormal"))
        {
            AudioSource.PlayClipAtPoint(soParetNormal, Camera.main.transform.position, volumGeneral);
        }
    }

    void PlayerInput()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        rb.AddForce(new Vector2(x * forsa, y * forsa));
    }
}
