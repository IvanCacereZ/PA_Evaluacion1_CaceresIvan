using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private Transform[] checkpointsPatrol;
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private AnimatorController animatorController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private LayerMask myLayers;
    [SerializeField] private float distanceModifier = 10;
    private Transform currentPositionTarget;
    private int patrolPos = 0;

    private void Start() {
        currentPositionTarget = checkpointsPatrol[patrolPos];
        transform.position = currentPositionTarget.position;
    }
    private void Update()
    {
        CheckNewPoint();
    }
    private void FixedUpdate() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, new Vector3(1,0 ,0), out hit, distanceModifier, myLayers))
        {
            Debug.DrawRay(transform.position, new Vector3(1, 0, 0) * distanceModifier, Color.yellow);
            velocityModifier = 10.0f;
        }
        else
        {
            Debug.DrawRay(transform.position, new Vector3(1, 0, 0) * distanceModifier, Color.red);
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
}
