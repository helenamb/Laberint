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
///         1.0: primera versió. Mostra puntuació, vida i temps
/// ----------------------------------------------------------------------------------
/// </summary>

public class ScrUI : MonoBehaviour
{
    [SerializeField]
    Text puntuacio, vida, temps;

    float crono = 0;

    void Update()
    {
        crono += Time.deltaTime;
        puntuacio.text = "" + ScrControlGame.punts;
        vida.text = "" + ScrControlGame.vida;
        temps.text = "" + crono.ToString("0.0");
    }
}
