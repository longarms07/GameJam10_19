using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // player attributes
    public float speed;
    public float flightSpeed;
    public float rotateSpeed;
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

    public GameObject playerShip;

    private int currentFrame;
    private float lastMoveDir;
    private bool lookingLeft = true;
    private GameObject heldPart = null;
    private PlayerShip ship;
    private SpriteRenderer spriteRenderer;
    private bool sprite0;
    private bool onShip;
    private Rigidbody2D rigid;



    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(playerShip!=null) ship = playerShip.GetComponent<PlayerShip>();
        rigid = this.GetComponent<Rigidbody2D>();
        GameManager.getInstance().player = this.gameObject;
    }

    // FixedUpdate is called at regular intervals
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (onShip)
        {
            


            float moveVertical = Input.GetAxis("Vertical");
            Vector2 face = new Vector2(moveHorizontal, moveVertical);
            float angle = Mathf.Atan2(face.x, face.y) * Mathf.Rad2Deg * -1;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

            Vector2 target = new Vector2(this.gameObject.transform.localPosition.x+moveHorizontal, this.gameObject.transform.localPosition.y + moveVertical);
            Vector3 moveTowards = Vector3.MoveTowards(this.gameObject.transform.localPosition, target, flightSpeed * Time.fixedDeltaTime);

            rb2d.MovePosition(moveTowards);

        }
        else
        {
            

            if (moveHorizontal < 0)
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
                    if (isCarrying) spriteRenderer.sprite = holdLeft1;
                    else spriteRenderer.sprite = walkLeft1;
                    currentFrame = 0;
                }
                else if (currentFrame == animationFrameRate)
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
                else if (currentFrame == animationFrameRate)
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
            Vector2 movement = new Vector2(moveHorizontal + transform.position.x, transform.position.y);


            //actually move the player
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, movement, step);


            if (isCarrying && heldPart != null)
            {
                if (lookingLeft) heldPart.transform.localPosition = this.gameObject.transform.localPosition + new Vector3(0, partOffset, 1);
                else heldPart.transform.localPosition = this.gameObject.transform.localPosition + new Vector3(0, partOffset, 1);
                CheckForShip();
            }
            if (Input.GetKeyDown(GameManager.getInstance().interactionKey))
            {
                if (!isCarrying)
                    CheckForPart();
                else PutDownPart();
            }
            else if (Input.GetKeyDown(GameManager.getInstance().boardShipKey))
            {
                RaycastHit2D hit = SearchForShip();
                if (hit.collider != null)
                {
                    if (ship.BoardShip())
                    {
                        rigid.gravityScale = 0;
                        this.gameObject.transform.localPosition = playerShip.transform.localPosition;
                        onShip = true;
                    }
                }
            }
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

    public void CheckForShip()
    {
        if (playerShip != null)
        {
            RaycastHit2D hit = SearchForShip();
           

            if (hit.collider != null)
            {
                if (ship.AddPart(heldPart))
                {
                    
                    isCarrying = false;
                }
                else PutDownPart();
            }
        }
    }

    public RaycastHit2D SearchForShip()
    {
        if (lookingLeft) return Physics2D.CircleCast(this.gameObject.transform.localPosition, this.gameObject.transform.localScale.y / 2, Vector3.left, 0, LayerMask.GetMask("PlayerShip"));
        else return Physics2D.CircleCast(this.gameObject.transform.localPosition, this.gameObject.transform.localScale.y / 2, Vector3.right, 0, LayerMask.GetMask("PlayerShip"));
    }

    public void PutDownPart()
    {
        isCarrying = false;
        heldPart.GetComponent<Rigidbody2D>().gravityScale = 1;
        heldPart = null;
    }

    public void Eject()
    {
        onShip = false;
        rigid.gravityScale = 1;
    }
}
