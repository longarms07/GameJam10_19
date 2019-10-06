using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakIntoPieces : MonoBehaviour
{

    public List<GameObject> shipPieces;
    public int score;

    private ParticleSystem explosion;
    private SpriteRenderer spriteRenderer;
    private Damageable hp;
    private ExplosionAudio exAud;
    private bool Kersploding = false;


    // Start is called before the first frame update
    void Start()
    {

        exAud = GetComponent<ExplosionAudio>();
        explosion = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hp = gameObject.GetComponent<Damageable>();
    }

     void Update()
    {
        if (hp.hitpoints <= 0)
        {
            Debug.Log("Hp less than 0");
            Kersplode();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("Collision aaah! " + collision.gameObject.layer + (collision.gameObject.layer == 10));
        if (collision.gameObject.layer == 10) 
        {
            Debug.Log("Collision aaah! " + collision.gameObject);
            PlayerShip playerShip = GameManager.getInstance().playerShip.GetComponent<PlayerShip>();
            Damageable dam = GetComponent<Damageable>();
            Debug.Log("playerShip != null"+ playerShip != null);
            Debug.Log("dam !=null"+ dam != null);
            Debug.Log("playerShip.canBeDamaged"+ playerShip.canBeDamaged);
            if (playerShip != null && dam !=null && playerShip.canBeDamaged)
            {
                Debug.Log("Here");
                collision.otherCollider.enabled = false;
                if (dam.hitpoints > hp.hitpoints)
                    {
                        dam.hitpoints = dam.hitpoints - hp.hitpoints;
                        Kersplode();
                    }
                else if (dam.hitpoints < hp.hitpoints)
                {
                    hp.hitpoints = hp.hitpoints - dam.hitpoints;
                    playerShip.Kersplode();
                }
                else
                {
                    playerShip.Kersplode();
                    Kersplode();
                }
                
            }
        }
    }

    public void Kersplode()
    {
        
        if(explosion != null && !Kersploding)
        {
            Kersploding = true;
            spriteRenderer.sprite = null;
            exAud.explode();
            explosion.Play();
            SpawnParts();
            GameManager.getInstance().score += score;
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
