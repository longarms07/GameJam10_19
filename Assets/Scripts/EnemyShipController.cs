﻿using System.Collections;
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
            rb2d.velocity = Vector2.zero;
        }

        //make enemy face the direction it is travelling
        Vector2 face = target - new Vector2(transform.position.x, transform.position.y);

        float angle = Mathf.Atan2(face.x, face.y) * Mathf.Rad2Deg * -1;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (retreat)
        {
            rb2d.AddForce(offsetFromTarget.normalized * retreatMultiplier * speed);
        }
        else {
            rb2d.velocity = offsetFromTarget.normalized * -speed;
        }


        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

    }

}
