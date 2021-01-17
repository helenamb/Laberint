using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // llibreria per a utilitzar els elements de tipus UI

/// <summary>
/// ----------------------------------------------------------------------------------
/// DESCRIPCIÓ
///         Script utilitzat per a gestionar les accions relacionades amb la UI
/// AUTOR:  Helena Martí
/// DATA:   31/12/2020
/// VERSIÓ: 1.0
/// CONTROL DE VERSIONS
///         1.0: Mostra puntuació, vida i temps.
/// ----------------------------------------------------------------------------------
/// </summary>

public class ScrUI : MonoBehaviour
{
    ScrControlGame scrCG;
    
    [SerializeField]
    Text puntuacio, vida, temps;

    public Text pickupsAgafats;
    
    float crono = 0;

    private void Start()
    {
        scrCG = GameObject.Find("Game Manager").GetComponent<ScrControlGame>(); // definim la referència a l'script ControlGame
    }

    void Update()
    {
        // Definim tots els casos en els quals es valors de la UI no s'han d'actualitzar
        
        if (!scrCG.nivellAcabat)
        {
            if (!scrCG.morint)
            {
                if (!scrCG.victoria)
                {
                    if (!scrCG.exiting)
                    {
                        crono += Time.deltaTime;
                        puntuacio.text = "" + scrCG.punts;
                        vida.text = "" + scrCG.vida;
                        temps.text = "" + crono.ToString("0.0");
                    }
                }
            }
        }
    }
}
