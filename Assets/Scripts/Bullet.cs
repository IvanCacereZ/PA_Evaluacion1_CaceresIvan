using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocidad = 10f;

    private Camera camara;

    Vector3 direccion;


    Vector3 AnteriorPosicion;
    private float distanciaRecorrida = 0.0f;

    void Start()
    {
        AnteriorPosicion = transform.position;
        camara = Camera.main;
        direccion = GetDirection(); //obtenemos la posicion del mouse para calcular la direccion de la bala
    }

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, AnteriorPosicion);
        distanciaRecorrida += distancia;
        AnteriorPosicion = transform.position;
        transform.Translate(direccion * velocidad * Time.deltaTime); //La bala seguira un camino fijo hacia esta direccion
        DistanceMaxima(); //preguntamos cada frame lo siguiente:
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
    private Vector3 GetDirection()
    {
        Vector3 posicionMouse = Input.mousePosition;
        posicionMouse.z = -camara.transform.position.z;
        Vector3 posicionMouseEnMundo = camara.ScreenToWorldPoint(posicionMouse);

        Vector3 direccion = posicionMouseEnMundo - transform.position;
        direccion.Normalize();
        return direccion;
    }
    private void DistanceMaxima() //Esto permite que no existan tantas balas en la escena y alteren el rendimiento
    {
        if(distanciaRecorrida > 100.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
