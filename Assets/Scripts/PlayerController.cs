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
    public Sprite standRight;
    public Sprite standLeft;
    public Sprite standHoldLeft;
    public Sprite standHoldRight;

    public int animationFrameRate;
    private int currentFrame;
    private float lastMoveDir;
    private bool lookingLeft = true;

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
            lookingLeft = true;
            if (lastMoveDir >= 0)
            {
                currentFrame = 0;
            }
            //leftSprite
            if (sprite0 && currentFrame == animationFrameRate)
            {
                sprite0 = false;
                if(isCarrying) spriteRenderer.sprite = holdLeft1;
                else spriteRenderer.sprite = walkLeft1;
                currentFrame = 0;
            }
            else if(currentFrame == animationFrameRate)
            {
                sprite0 = true;
                if (isCarrying) spriteRenderer.sprite = holdLeft0;
                else spriteRenderer.sprite = walkLeft0;
                currentFrame = 0;
            }
        }
        else if (moveHorizontal > 0)
        {
            lookingLeft = false;
            if (lastMoveDir <= 0)
            {
                currentFrame = 0;
            }
            //rightSprite
            if (sprite0 && currentFrame == animationFrameRate)
            {
                sprite0 = false;
                if (isCarrying) spriteRenderer.sprite = holdRight1;
                else spriteRenderer.sprite = walkRight1;
                currentFrame = 0;
            }
            else if(currentFrame == animationFrameRate)
            {
                sprite0 = true;
                if (isCarrying) spriteRenderer.sprite = holdRight0;
                else spriteRenderer.sprite = walkRight0;
                currentFrame = 0;
            }
        }
        else
        {
            //standing still
            sprite0 = false;
            if (isCarrying)
            {
                if (lookingLeft) spriteRenderer.sprite = standHoldLeft;
                else spriteRenderer.sprite = standHoldRight;
            }
            else
            {
                if (lookingLeft) spriteRenderer.sprite = standLeft;
                else spriteRenderer.sprite = standRight;
            }
        }
        currentFrame++;
        lastMoveDir = moveHorizontal;
        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);
    }
}
