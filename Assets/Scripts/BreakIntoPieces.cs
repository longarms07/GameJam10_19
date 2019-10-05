using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakIntoPieces : MonoBehaviour
{

    public List<GameObject> shipPieces;


    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnDestroy()
    {
        if(shipPieces.Count > 0)
        {
            Random rnd = new Random();

            int part1 = Random.Range(0, shipPieces.Count);
            if (shipPieces.Count > 1) {
                int part2 = Random.Range(0, shipPieces.Count);
                while (part2 == part1)
                {
                    part2 = Random.Range(0, shipPieces.Count);
                }
                Instantiate(shipPieces[part2]).transform.localPosition = this.transform.localPosition;
            }
            Instantiate(shipPieces[part1]).transform.localPosition = this.transform.localPosition;
        }
    }

}
