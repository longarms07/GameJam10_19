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


        rb2d.MovePosition(moveTowards);


        /*
            face the move direction
         */
        Vector2 face = moveTowards - new Vector2(transform.position.x, transform.position.y);
        double y = moveTowards.y - transform.position.y;

        //get the angle and rotate 
        float angle = Mathf.Atan2(face.x, face.y) * Mathf.Rad2Deg * -1;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //turn around when nessecary 
        if (retreat)
        {
            Quaternion.Inverse(rotation);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

    }

}
