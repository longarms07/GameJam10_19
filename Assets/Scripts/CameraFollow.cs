using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject toFollow;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - toFollow.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = toFollow.transform.position + offset;
    }
}
