using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float speed;

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

        rb2d.MovePosition(moveTowards);
    }
}
