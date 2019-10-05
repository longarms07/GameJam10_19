using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{

    public GameObject shield;
    public GameObject engine;
    public GameObject lWing;
    public GameObject rWing;
    public GameObject lGun;
    public GameObject rGun;

    private Dictionary<ShipPartEnum, bool> partAttached;
    private SpriteRenderer shieldRenderer;
    private SpriteRenderer engineRenderer;
    private SpriteRenderer lWingRenderer;
    private SpriteRenderer rWingRenderer;
    private SpriteRenderer lGunRenderer;
    private SpriteRenderer rGunRenderer;
    private ShieldAnimator shieldAnim;



    // Start is called before the first frame update
    void Start()
    {
        shieldRenderer = shield.GetComponent<SpriteRenderer>();
        engineRenderer = engine.GetComponent<SpriteRenderer>();
        lWingRenderer = lWing.GetComponent<SpriteRenderer>();
        rWingRenderer = rWing.GetComponent<SpriteRenderer>();
        lGunRenderer = lGun.GetComponent<SpriteRenderer>();
        rGunRenderer = rGun.GetComponent<SpriteRenderer>();
        shieldAnim = shield.GetComponent<ShieldAnimator>();
        partAttached = new Dictionary<ShipPartEnum, bool>();
        partAttached[ShipPartEnum.LGun] = false;
        partAttached[ShipPartEnum.LWing] = false;
        partAttached[ShipPartEnum.RGun] = false;
        partAttached[ShipPartEnum.RWing] = false;
        partAttached[ShipPartEnum.Shield] = false;
        partAttached[ShipPartEnum.Engine] = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddPart(GameObject part)
    {
        ShipPart shipPart = part.GetComponent<ShipPart>();
        Debug.Log(shipPart.kind);
        if (shipPart == null) return false;
        if (partAttached[shipPart.kind])
        {
            switch (shipPart.kind)
            {
                case (ShipPartEnum.LGun):
                    lGunRenderer.sprite = shipPart.sprite;
                    if (!partAttached[ShipPartEnum.RGun])
                        shipPart.kind = ShipPartEnum.RGun;
                    else return false;
                    break;
                case (ShipPartEnum.RGun):
                    if (!partAttached[ShipPartEnum.LGun])
                        shipPart.kind = ShipPartEnum.LGun;
                    else return false;
                    break;
                default:
                    return false;
            }
        }

        partAttached[shipPart.kind] = true;
        Debug.Log(shipPart.kind);
        switch (shipPart.kind)
        {
            case (ShipPartEnum.LGun):
                lGunRenderer.sprite = shipPart.sprite;
                break;
            case (ShipPartEnum.RGun):
                rGunRenderer.sprite = shipPart.sprite;
                break;
            case (ShipPartEnum.LWing):
                lWingRenderer.sprite = shipPart.sprite;
                break;
            case (ShipPartEnum.RWing):
                rWingRenderer.sprite = shipPart.sprite;
                break;
            case (ShipPartEnum.Engine):
                engineRenderer.sprite = shipPart.sprite;
                break;
            case (ShipPartEnum.Shield):
                if (shipPart.shieldAnims.Length != 3) return false;
                shieldAnim.s0 = shipPart.sprite;
                shieldAnim.s1 = shipPart.shieldAnims[0];
                shieldAnim.s2 = shipPart.shieldAnims[1];
                shieldAnim.s3 = shipPart.shieldAnims[2];
                shieldRenderer.sprite = shipPart.sprite;
                break;
        }

        return true;

    }

    public bool CanFly()
    {
        if (partAttached[ShipPartEnum.LWing] && partAttached[ShipPartEnum.RWing] && partAttached[ShipPartEnum.Engine])
            return true;
        return false;
    }

    public bool CanShoot()
    {
        if (partAttached[ShipPartEnum.RGun] || partAttached[ShipPartEnum.LGun])
            return true;
        return false;
    }

    public bool HasShield()
    {
        if (partAttached[ShipPartEnum.Shield]) return true;
        return false;
    }



}
