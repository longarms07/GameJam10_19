using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject toSpawn;
    public float SpawnTime;
    private float timeSinceLastSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= SpawnTime)
        {
            Instantiate(toSpawn).transform.position = this.transform.position;
            timeSinceLastSpawn = 0;
        }
    }



}
