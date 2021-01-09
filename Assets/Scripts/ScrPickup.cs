using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ----------------------------------------------------------------------------------
/// DESCRIPCIÓ
///         Script utilitzat per al control dels Pickups
/// AUTOR:  Helena Martí Barragán
/// DATA:   30/11/2020
/// VERSIÓ: 1.0
/// CONTROL DE VERSIONS
///         1.0: Definició del gir de l'sprite.
/// ----------------------------------------------------------------------------------
/// </summary>

public class ScrPickup : MonoBehaviour
{
    [SerializeField] float velocitatGir = 1;

    ScrControlGame scrCG;

    public int valor = 1;   // valor de puntuació que valdrà cada pickup.

    void Start()
    {
        scrCG = GameObject.Find("Game Manager").GetComponent<ScrControlGame>();

        scrCG.pickups++;   // al inicialitzar el joc, sumarà un per cada pickup
    }

    void Update()
    {
        transform.Rotate(0, 0, velocitatGir * Time.deltaTime);
    }
}
