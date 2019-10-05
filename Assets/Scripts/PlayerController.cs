using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // player attributes
    public float speed;
    public float hitPoints;
    public bool isCarrying;

    public Sprite walkLeft0;
    public Sprite walkLeft1;
    public Sprite walkRight0;
    public Sprite walkRight1;
    public Sprite holdLeft0;
    public Sprite holdLeft1;
    public Sprite holdRight0;
    public Sprite holdRight1;
    public Sprite stand;

    public int animationFrameRate;
    private int currentFrame;
    private float lastMoveDir;

    private SpriteRenderer spriteRenderer;
    private bool sprite0;



    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameManager.getInstance().player = this.gameObject;
    }

    // FixedUpdate is called at regular intervals
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        if(moveHorizontal < 0)
        {
            if(lastMoveDir >= 0)
            {
                currentFrame = 0;
            }
            //leftSprite
            if (sprite0 && currentFrame == animationFrameRate)
            {
                sprite0 = false;
                spriteRenderer.sprite = walkLeft1;
                currentFrame = 0;
            }
            else if(currentFrame == animationFrameRate)
            {
                sprite0 = true;
                spriteRenderer.sprite = walkLeft0;
                currentFrame = 0;
            }
        }
        else if (moveHorizontal > 0)
        {
            if (lastMoveDir <= 0)
            {
                currentFrame = 0;
            }
            //rightSprite
            if (sprite0 && currentFrame == animationFrameRate)
            {
                sprite0 = false;
                spriteRenderer.sprite = walkRight1;
                currentFrame = 0;
            }
            else if(currentFrame == animationFrameRate)
            {
                sprite0 = true;
                spriteRenderer.sprite = walkRight0;
                currentFrame = 0;
            }
        }
        else
        {
            //standing still
            sprite0 = false;
            spriteRenderer.sprite = stand;
        }
        currentFrame++;
        lastMoveDir = moveHorizontal;
        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);
    }
}
