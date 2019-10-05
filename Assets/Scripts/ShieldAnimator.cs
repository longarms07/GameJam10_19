using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAnimator : MonoBehaviour
{

    public int frameRate;
    public Sprite s0;
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public bool setActive = false;
    private int currentFrame;
    private SpriteRenderer spriteRenderer;
    private bool active = false;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (setActive) SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (currentFrame == frameRate * 4) currentFrame = 0;
            if (currentFrame == 0) spriteRenderer.sprite = s0;
            if (currentFrame == frameRate * 1) spriteRenderer.sprite = s1;
            if (currentFrame == frameRate * 2) spriteRenderer.sprite = s2;
            if (currentFrame == frameRate * 3) spriteRenderer.sprite = s3;
            currentFrame++;
        }
    }

    public void SetActive(bool set)
    {
        if (set) currentFrame = 0;
        active = set;
    }
}
