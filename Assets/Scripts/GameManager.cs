﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager g;

    public GameObject player;

    private GameManager() {

    }

    public static GameManager getInstance() {
        if (g == null) {
            g = new GameManager();
        }
        return g;
    }
}
