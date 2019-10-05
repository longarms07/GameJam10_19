using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager g;

    public GameObject player;

    private GameManager() {

    }

    public static GameManager getInstance() {
        
        return g;
    }

    private void Awake()
    {
        if (g == null)
        {
            g = this;
        }
    }

}
