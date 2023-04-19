using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRBD2;
    [SerializeField] private float velocityModifier = 5f;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform Transform;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Text vidaUI;
    [SerializeField] private Text gameOver;
    public int vida = 50;
    private void Update() {
        vidaUI.text = "vida = " + vida;
        Vector2 movementPlayer = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        myRBD2.velocity = movementPlayer * velocityModifier;


        Vector2 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        CheckFlip(mouseInput.x);
    
        Debug.DrawRay(transform.position, mouseInput.normalized * rayDistance, Color.red);

        if(Input.GetMouseButtonDown(0)){
            Debug.Log("Right Click");
            Instantiate(Bullet, Transform.transform.position, Transform.transform.rotation);
        }else if(Input.GetMouseButtonDown(1)){
            Debug.Log("Left Click");
        }
        destroyPlayer();
    }

    private void CheckFlip(float x_Position){
        spriteRenderer.flipX = (x_Position - transform.position.x) < 0;
    }
    public void RestarVida(int n)
    {
        vida = vida - n;
    }
    private void destroyPlayer()
    {
        if(vida <= 0)
        {
            gameOver.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
