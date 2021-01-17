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
    ScrUI scrUI;

    public AudioClip soParetNormal, soParetXunga, soPickup, soSuperPickup, soVirus, soClau;

    void Start()
    {
        // Definim les referències que necessitarem utilitzar en aquest script
        
        rb = GetComponent<Rigidbody2D>();

        scrCG = GameObject.Find("Game Manager").GetComponent<ScrControlGame>();
        scrUI = GameObject.Find("Canvas").GetComponent<ScrUI>();

        // Desactivem la visibilitat dels elements del canvas corresponents que són controlats en aquest script per a que no apareguin des del primer moment

        nivellComplert.enabled = false;
        scrUI.pickupsAgafats.enabled = false;
    }

    private void FixedUpdate()
    {
        // Controlem els casos en els quals el jugador no podrà moure el Player (quan acaba el nivell, es posa el menú de pausa, mor...)

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
        // Controlem tots els elements amb els que pot col·lisionar el player i el que passarà en cada cas

        if (collision.CompareTag("Pickup"))
        {
            AudioSource.PlayClipAtPoint(soPickup, Camera.main.transform.position, volumPickups);    // Reprodueix el so corresponent

            // En xocar suma punts, suma un al valor total de pickups agafats, destrueix el pickup i resta un al valor de pickups restants a l'escena.

            scrP = collision.GetComponent<ScrPickup>();
            scrCG.punts += scrP.valor;
            scrCG.pickupsMenjats++;
            Destroy(collision.gameObject);
            scrCG.pickups--;
        }

        if (collision.CompareTag("SuperPickup"))
        {
            AudioSource.PlayClipAtPoint(soSuperPickup, Camera.main.transform.position, volumPickups);

            // En xocar suma punts, suma un al valor total de pickups agafats, destrueix el pickup i resta un al valor de pickups restants a l'escena.

            scrP = collision.GetComponent<ScrPickup>();
            scrCG.punts += scrP.valor;
            scrCG.pickupsMenjats++;
            Destroy(collision.gameObject);
            scrCG.pickups--;
        }

        if (collision.CompareTag("ClauVermella"))
        {
            AudioSource.PlayClipAtPoint(soClau, Camera.main.transform.position, volumGeneral);

            // En xocar crida la funció en l'ScrPorta que fa que es reprodueixi l'animació d'obrir la porta, i destrueix la clau

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

            // En xocar resta vida, i controlem que el valor de la vida no pugui ser més petit que 0. Destruïm el GameObject.

            scrCG.vida--;
            if (scrCG.vida < 0) scrCG.vida = 0;

            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Sortida"))
        {
            // En xocar mostra la pantalla de nivell acabat, amb el recompte dels pickups totals agafats en el nivell. 

            nivellComplert.enabled = true;
            scrCG.nivellAcabat = true;
            scrUI.pickupsAgafats.enabled = true;
            scrUI.pickupsAgafats.text = "Pickups recol·lectats: " + scrCG.pickupsMenjats;
        }

        if (collision.CompareTag("Victoria"))
        {
            // En xocar mostra la pantalla de victòria, amb el recompte dels pickups totals agafats en el nivell. 

            scrCG.victoria = true;
            scrUI.pickupsAgafats.enabled = true;
            scrUI.pickupsAgafats.text = "Pickups recol·lectats: " + scrCG.pickupsMenjats;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ParetElectrica"))
        {
            AudioSource.PlayClipAtPoint(soParetXunga, Camera.main.transform.position, volumGeneral);
            
            // En xocar resta vida al Player i controla que la salut no pugui ser més petita que 0.

            scrCG.vida--;
            if (scrCG.vida < 0) scrCG.vida = 0;
        }

        if (collision.gameObject.CompareTag("ParetNormal"))
        {
            AudioSource.PlayClipAtPoint(soParetNormal, Camera.main.transform.position, volumGeneral);   // reprodueix un so en col·lisionar
        }
    }

    void PlayerInput()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        rb.AddForce(new Vector2(x * forsa, y * forsa)); // físiques del moviment del player
    }
}
