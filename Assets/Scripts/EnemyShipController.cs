using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float speed;
    public float rotateSpeed;
    public float retreatThreshold;
    public float retreatUntil;

    private bool retreat;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 target = GameManager.getInstance().player.transform.localPosition;

        Vector2 offsetFromTarget = new Vector2(transform.position.x, transform.position.y) - target;

        //moves away from the player when it gets too close
        if (Vector2.Distance(transform.position, target) < retreatThreshold)
        {
            retreat = true;
        }
        else if (Vector2.Distance(transform.position, target) >= retreatUntil)
        {
            retreat = false;
        }

        if (retreat)
        {
            rb2d.AddForce(-offsetFromTarget * speed);
        }
        else {
            rb2d.AddForce(offsetFromTarget * speed);
        }
        


    }

}
