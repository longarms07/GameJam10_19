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
    public float retreatMultiplier;
    public float fireInterval;
    public float missileSpeed;

    public GameObject missile;

    private bool retreat;
    private float timeSinceFire;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        timeSinceFire += Time.deltaTime;

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
            rb2d.velocity = Vector2.zero;
        }

        //make enemy face the direction it is travelling
        Vector2 face = target - new Vector2(transform.position.x, transform.position.y);

        if (retreat)
        {
            face *= -1;
            rb2d.AddForce(offsetFromTarget.normalized * retreatMultiplier * speed);
        }
        else {
            rb2d.velocity = offsetFromTarget.normalized * -speed;

            if (timeSinceFire >= fireInterval) {

                GameObject tempMissile = Instantiate(missile);

                tempMissile.transform.localPosition = transform.localPosition;

                Rigidbody2D missileRb = GetComponent<Rigidbody2D>();
                if (missileRb != null) {
                    missileRb.AddForce(offsetFromTarget.normalized * -missileSpeed);
                }

                timeSinceFire = 0;
            }
        }

        float angle = Mathf.Atan2(face.x, face.y) * Mathf.Rad2Deg * -1;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

    }

}
