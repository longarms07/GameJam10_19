using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnClick : MonoBehaviour
{
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

    private void OnMouseDown() //Works on touch device as well.
    {
        spriteRenderer.sprite = null;
        if (explosion != null)
        {
            explosion.Play();
        }
        BreakIntoPieces pieces = this.gameObject.GetComponent<BreakIntoPieces>();
        if (pieces != null) pieces.SpawnParts();
        Destroy(this.gameObject, explosion.duration);
    }
}
