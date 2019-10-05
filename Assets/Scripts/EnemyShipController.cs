using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float speed;
    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = GameManager.getInstance().player.transform.localPosition;

        Vector2 moveTowards = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        //get the x and y of the vector to face
        Vector2 face = moveTowards - new Vector2(transform.position.x, transform.position.y);
        double y = moveTowards.y - transform.position.y;

        float angle = Mathf.Atan2(face.x, face.y) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);


        rb2d.MovePosition(moveTowards);
    }
}
