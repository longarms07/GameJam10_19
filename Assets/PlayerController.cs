using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // player attributes
    public float speed;
    public float hitPoints;
    public bool isCarrying;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called at regular intervals
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);
    }
}
