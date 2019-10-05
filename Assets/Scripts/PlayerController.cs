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

    public float raycastDistance;
    public float partOffset;

    private int currentFrame;
    private float lastMoveDir;
    private bool lookingLeft = true;
    private GameObject heldPart = null;

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
        if (isCarrying && heldPart != null)
        {
            if (lookingLeft) heldPart.transform.localPosition = this.gameObject.transform.localPosition + new Vector3(0, partOffset, 1);
            else heldPart.transform.localPosition = this.gameObject.transform.localPosition + new Vector3(0, partOffset, 1);
        }
        if (Input.GetKeyDown(GameManager.getInstance().interactionKey))
        {
            if(!isCarrying)
                CheckForPart();
        }
    }

    public void CheckForPart()
    {

        RaycastHit2D hit;
        if (lookingLeft) hit = Physics2D.CircleCast(this.gameObject.transform.localPosition, this.gameObject.transform.localScale.y/2, Vector3.left, raycastDistance, LayerMask.GetMask("ShipPart"));
        else hit = Physics2D.CircleCast(this.gameObject.transform.localPosition, this.gameObject.transform.localScale.y / 2, Vector3.right, raycastDistance, LayerMask.GetMask("ShipPart"));

        if (hit.collider != null)
        {
            isCarrying = true;
            heldPart = hit.collider.gameObject;
            heldPart.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

}
