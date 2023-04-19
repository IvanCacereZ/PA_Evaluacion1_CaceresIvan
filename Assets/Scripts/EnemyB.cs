using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRB2D;
    [SerializeField] private float velocityModifier;
    [SerializeField] private LayerMask myLayers;
    [SerializeField] private PlayerController player;
    private int OrientacionRayCast = 1;
    private void Start()
    {
        if (myRB2D == null)
        {
            myRB2D = GetComponent<Rigidbody2D>();
        }
    }

    private void Update()
    {

        Vector2 inputVector = new Vector2(OrientacionRayCast, 0);

        RaycastHit2D raycast = Physics2D.CircleCast(transform.position, 6.0f, inputVector);

        if (raycast.collider != null)
        {
            if (raycast.collider.gameObject.tag == "Player")
            {
                transform.position = Vector2.MoveTowards(transform.position, raycast.collider.gameObject.transform.position, velocityModifier * Time.deltaTime);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player") 
        {
            this.player.RestarVida(30);
            Destroy(this.gameObject);
        }
    }
}
