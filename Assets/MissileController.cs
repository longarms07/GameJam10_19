using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public EnemyShipController ship;

    private ParticleSystem explosion;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        explosion = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            Kersplode();
            Debug.Log(collision.gameObject.layer);
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
