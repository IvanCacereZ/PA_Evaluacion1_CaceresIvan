using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class WanderingWalker : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] float _velocity;
    [SerializeField] float JumpScale;
    private int OrientacionRayCast = -1;
    [SerializeField] float distanceModifier = 2.0f;
    [SerializeField] private LayerMask myLayers;


    private void Awake() {
        _transform = GetComponent<Transform>();    
    }

    private void Update(){
        if (Input.GetKey(KeyCode.W) == true)
        {
            _transform.position = new Vector2(_transform.position.x, _transform.position.y + JumpScale * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) == true)
        {
            _transform.position = new Vector2(_transform.position.x + _velocity * -1 * Time.deltaTime, _transform.position.y);
        }
        if (Input.GetKey(KeyCode.D) == true)
        {
            _transform.position = new Vector2(_transform.position.x + _velocity * Time.deltaTime, _transform.position.y);
        }

        Vector2 inputVector = new Vector3(0, OrientacionRayCast);

        RaycastHit2D raycast = Physics2D.Raycast(transform.position, inputVector, distanceModifier, myLayers);
        Debug.DrawRay(transform.position, inputVector.normalized * distanceModifier, Color.red);

        //if (raycast.collider != null){}       
    }
}