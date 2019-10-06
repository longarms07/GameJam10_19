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

    
    private PlayerShip ship;
    private TextMeshProUGUI durabilityText;

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
        
    }

    private void Update()
    {
        if(durabilityText!=null) durabilityText.text = "Ship HP: " + ship.durability;
    }

}
