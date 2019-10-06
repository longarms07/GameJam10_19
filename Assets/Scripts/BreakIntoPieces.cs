using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakIntoPieces : MonoBehaviour
{

    public List<GameObject> shipPieces;
    public int durability;

    private ParticleSystem explosion;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {

        explosion = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        //Debug.Log("Collision aaah! " + collision.gameObject.layer);
        if (collision.gameObject.layer == 10) 
        {
            Debug.Log("Collision aaah! " + collision.gameObject);
            PlayerShip playerShip = GameManager.getInstance().playerShip.GetComponent<PlayerShip>();
            if (playerShip != null)
            {
                    collision.otherCollider.enabled = false;
                    if (playerShip.durability >= durability)
                    {
                        Kersplode();
                    }
                    if (playerShip.durability <= durability) playerShip.Kersplode();
                    else playerShip.durability = playerShip.durability - durability;

                
            }
        }
    }

    public void Kersplode()
    {
        
        spriteRenderer.sprite = null;
        if (explosion != null)
        {
            explosion.Play();
            SpawnParts();
            Destroy(this.gameObject, explosion.main.duration);
        }
    }


    public void SpawnParts()
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
