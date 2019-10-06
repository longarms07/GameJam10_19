using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager g;

    public string interactionKey;
    public string boardShipKey;
    public GameObject player;
    public GameObject playerShip;
    public GameObject durabilityTextMesh;
    public GameObject scoreTextMesh;
    public int score;


    private PlayerShip ship;
    private PlayerController playerController;
    private Damageable playerHp;
    private Damageable shipHp;
    private TextMeshProUGUI durabilityText;
    private TextMeshProUGUI scoreText;

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

    private void Start()
    {
        if (playerShip!=null) ship = playerShip.GetComponent<PlayerShip>();
        if (durabilityTextMesh != null) durabilityText = durabilityTextMesh.GetComponent<TextMeshProUGUI>();
        if (scoreTextMesh != null) scoreText = scoreTextMesh.GetComponent<TextMeshProUGUI>();
        playerHp = player.GetComponent<Damageable>();
        shipHp = playerShip.GetComponent<Damageable>();
        playerController = player.GetComponent<PlayerController>();
        score = 0;
    }

    private void Update()
    {
        if(durabilityText!=null) durabilityText.text = "Ship HP: " + shipHp.hitpoints+"\nPlayer HP: "+playerHp.hitpoints;
        if (scoreText != null) scoreText.text = "Score: " + score;
    }

}
