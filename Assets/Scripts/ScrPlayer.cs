using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ----------------------------------------------------------------------------------
/// DESCRIPCIÓ
///         Script utilitzat per al control del Player
/// AUTOR:  Helena Martí Barragán
/// DATA:   30/11/2020
/// VERSIÓ: 1.0
/// CONTROL DE VERSIONS
///         1.0: primera versió. Moviment del player en els eixos xy, col·lisió i destrucció pickups.
/// ----------------------------------------------------------------------------------
/// </summary>
    
public class ScrPlayer : MonoBehaviour
{
    float x, y; // variables per a guardar la lectura dels cursors

    [SerializeField] float forsa = 10;

    Rigidbody2D rb;
    ScrPickup scrP;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // llegim el moviment als dos eixos
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(x * forsa, y * forsa));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup"))
        {
            scrP = collision.GetComponent<ScrPickup>();
            Destroy(collision.gameObject);
        } 
    }
}
