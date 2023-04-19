using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private Transform[] checkpointsPatrol;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private LayerMask myLayers;
    [SerializeField] private float distanceModifier = 15;
    [SerializeField] private PlayerController player;
    private Transform currentPositionTarget;
    private int patrolPos = 0;
    

    private void Start() {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;
    }
    private void Update()
    {
        float anguloRotacion = transform.rotation.eulerAngles.z;
        float anguloRadianes = anguloRotacion * Mathf.Deg2Rad;
        Vector2 direccionAdelante = new Vector2(Mathf.Cos(anguloRadianes), Mathf.Sin(anguloRadianes));
        CheckNewPoint();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direccionAdelante, distanceModifier, myLayers);
        Debug.DrawRay(transform.position, direccionAdelante, Color.red);
        if (hit.collider != null)
        {
            velocityModifier = velocityModifier * 2;
        }
        else
        {
            velocityModifier = 5.0f;
        }
    }

    private void CheckNewPoint(){
        if(Mathf.Abs((transform.position - currentPositionTarget.position).magnitude) < 0.25){
            patrolPos = patrolPos + 1 == checkpointsPatrol.Length? 0: patrolPos+1;
            currentPositionTarget = checkpointsPatrol[patrolPos];
            myRBD2.velocity = (currentPositionTarget.position - transform.position).normalized*velocityModifier;
            CheckFlip(myRBD2.velocity.x);
        }
    }

    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.player.RestarVida(20);
            Destroy(this.gameObject);
        }
    }
}
