using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public EnemyShipController ship;
    public int damage;

    private ParticleSystem explosion;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collisionBox;

    // Start is called before the first frame update
    void Start()
    {
        explosion = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collisionBox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Called automatically on collision
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.gameObject.layer >= 8 && collision.gameObject.layer <= 11) || collision.gameObject.layer == 0)
        {
            collisionBox.enabled = false;
            Kersplode();

            //damage 
            Damageable hp = GameManager.getInstance().player.GetComponent<Damageable>();
            if (hp != null) {
                hp.hitpoints -= damage;
            }
        }
    }

    public void Kersplode()
    {

        spriteRenderer.sprite = null;
        if (explosion != null)
        {
            explosion.Play();
            Destroy(this.gameObject, explosion.main.duration);
        }
    }
}
