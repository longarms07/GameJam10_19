using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = GameManager.getInstance().player.transform.localPosition;

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
