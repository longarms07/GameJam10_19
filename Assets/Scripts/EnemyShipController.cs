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

    // Update is called once per frame
    void Update()
    {
        /*
            move torwards or away from the player
         */
        Vector2 target = GameManager.getInstance().player.transform.localPosition;

        Vector2 moveTowards = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        //moves away from the player when it gets too close
        if (Vector2.Distance(transform.position, target) < retreatThreshold) {
            retreat = true;
        }
        else if (Vector2.Distance(transform.position, target) >= retreatUntil)
        {
            retreat = false;
        }

        
        if (retreat) {
            moveTowards = Vector2.MoveTowards(transform.position, target, -1 * speed * Time.deltaTime);
        }
        


        /*
            face the move direction
         */
        Vector2 face = moveTowards - new Vector2(transform.position.x, transform.position.y);

        //get the angle and rotate 
        float angle = Mathf.Atan2(face.x, face.y) * Mathf.Rad2Deg * -1;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //turn around when nessecary 
        if (retreat)
        {
            Quaternion.Inverse(rotation);
            angle += 180 % 360;
            angle += 0.00f;
        }
        else {
            angle -= 0.00f;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);


        Vector2 newPos = new Vector2(transform.position.x, transform.position.y) + new Vector2(speed * Mathf.Cos(angle * Mathf.Deg2Rad), speed * Mathf.Sin(angle * Mathf.Deg2Rad));

        moveTowards = Vector2.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
        if (retreat) { moveTowards = Vector2.MoveTowards(transform.position, newPos, -1 * speed * Time.deltaTime); }

        rb2d.MovePosition(moveTowards);
    }

}
