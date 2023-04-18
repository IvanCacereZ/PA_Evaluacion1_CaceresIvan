using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : MonoBehaviour
{
    [SerializeField] private float velocidadEnemigoB;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanceMinima;
    private int numeroAleatorio;
    [SerializeField] private LayerMask myLayers;
    [SerializeField] private float distanceModifier = 10.0f;
    SpriteRenderer sprite;
    Rigidbody2D myrb2d;
    private void Start()
    {
        numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
        if(sprite == null)
        {
            sprite = GetComponent<SpriteRenderer>();
        }
        if(myrb2d == null)
        {
            myrb2d = GetComponent<Rigidbody2D>();
        }
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, puntosMovimiento[numeroAleatorio].position, velocidadEnemigoB * Time.deltaTime);
        if (Vector3.Distance(transform.position, puntosMovimiento[numeroAleatorio].position) < distanceMinima)
        {
            CheckFlip(myrb2d.velocity.x);
            numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
        }
        Vector3 inputVector = new Vector3(transform.position.x, transform.position.y, 0);

        RaycastHit2D raycast = Physics2D.Raycast(transform.position, inputVector.normalized, distanceModifier, myLayers);
        Debug.DrawRay(transform.position, inputVector.normalized * distanceModifier, Color.red);

        if (raycast.collider != null)
        {
            if (raycast.collider.gameObject.tag == "Player")
            {
                velocidadEnemigoB = 20.0f;
            }
            else
            {
                velocidadEnemigoB = 10.0f;
            }
        }
    }
    private void CheckFlip(float x_Position)
    {
        sprite.flipX = (x_Position - transform.position.x) < 0;
    }
}
