using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;

    public float scale;
    
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Reset the MoveDelta 
        moveDelta = new Vector3(x,y,0);

        // Swap sprite direction, Wether you're going right or left
        if(moveDelta.x > 0)
            transform.localScale = new Vector3(scale,scale,1);
        else if(moveDelta.x < 0)
            transform.localScale = new Vector3(-scale,scale,1);

        // block the player from moving on walls and npcs on y
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)

            // Allow player movment
            transform.Translate(0, (moveDelta.y * Time.deltaTime) / 3, 0);

        // block the player from moving on walls and npcs on x
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
            // Allow player movment
            transform.Translate((moveDelta.x * Time.deltaTime) / 3, 0, 0);

    }
}
