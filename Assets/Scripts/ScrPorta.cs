using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrPorta : MonoBehaviour
{
    /// <summary>
    /// ----------------------------------------------------------------------------------
    /// DESCRIPCIÓ
    ///         Script utilitzat per a controlar com s'obren i tanquen les portes
    /// AUTOR:  Helena Martí
    /// DATA:   6/1/2021
    /// VERSIÓ: 1.0
    /// CONTROL DE VERSIONS
    ///         1.0: Control de l'animació per a obrir les portes.
    /// ----------------------------------------------------------------------------------
    /// </summary>

    void Start()
    {
        this.GetComponent<Animator>().enabled = false;
    }

    public void ObrirPorta()    // Activa l'animació establerta i destrueix el GameObject un cop finalitzada
    {
        this.GetComponent<Animator>().enabled = true;
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }
}
