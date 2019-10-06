using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public EnemyShipController ship;
    public int damage;
    public GameObject spawner;

    private ParticleSystem explosion;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collisionBox;
    private Rigidbody2D rb2d;
    private ExplosionAudio exAud;

    // Start is called before the first frame update
    void Start()
    {
        exAud = GetComponent<ExplosionAudio>();
        explosion = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collisionBox = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Called automatically on collision
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != spawner)
        {
            if ((collision.gameObject.layer >= 8 && collision.gameObject.layer <= 11) || collision.gameObject.layer == 0)
            {
                if (collisionBox != null)
                    collisionBox.enabled = false;
                Kersplode();

                //damage 
                Damageable hp = collision.gameObject.GetComponent<Damageable>();
                if (hp != null)
                {
                    if (collision.gameObject != GameManager.getInstance().playerShip
                        || GameManager.getInstance().playerShip.GetComponent<PlayerShip>().canBeDamaged)
                        hp.hitpoints -= damage;
                }
            }
        }
    }

    public void Kersplode()
    {

        if (explosion != null)
        {

            spriteRenderer.sprite = null;
            exAud.explode();
            explosion.Play();
            Destroy(this.gameObject, explosion.main.duration);
        }
    }
}
