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
    public float explodeTime;
    public Damageable damage;

    private Dictionary<ShipPartEnum, bool> partAttached;
    private SpriteRenderer skeletonRenderer;
    private SpriteRenderer shieldRenderer;
    private SpriteRenderer engineRenderer;
    private SpriteRenderer lWingRenderer;
    private SpriteRenderer rWingRenderer;
    private SpriteRenderer lGunRenderer;
    private SpriteRenderer rGunRenderer;
    private ShieldAnimator shieldAnim;
    private bool inFlight;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private ParticleSystem explosion;
    public List<GameObject> shipPieces = new List<GameObject>();
    private bool canExplode;
    private float startTime;


    // Start is called before the first frame update
    void Start()
    {
        damage = GetComponent<Damageable>();
        damage.hitpoints = 0;
        shipPieces = new List<GameObject>();
        startPosition = this.gameObject.transform.position;
        startRotation = this.gameObject.transform.rotation;
        explosion = GetComponent<ParticleSystem>();
        shieldRenderer = shield.GetComponent<SpriteRenderer>();
        engineRenderer = engine.GetComponent<SpriteRenderer>();
        lWingRenderer = lWing.GetComponent<SpriteRenderer>();
        rWingRenderer = rWing.GetComponent<SpriteRenderer>();
        lGunRenderer = lGun.GetComponent<SpriteRenderer>();
        rGunRenderer = rGun.GetComponent<SpriteRenderer>();
        shieldAnim = shield.GetComponent<ShieldAnimator>();
        skeletonRenderer = GetComponent<SpriteRenderer>();
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
        if (inFlight)
        {
            startTime += Time.deltaTime;
            if (!canExplode && startTime >= explodeTime) canExplode = true;
            this.transform.localPosition = GameManager.getInstance().player.transform.localPosition;
            this.transform.localRotation = GameManager.getInstance().player.transform.localRotation;
            if (partAttached[ShipPartEnum.Shield] && damage.hitpoints < 10) shieldAnim.SetActive(false);
        }
    }

    public bool AddPart(GameObject part)
    {
        ShipPart shipPart = part.GetComponent<ShipPart>();
        //Debug.Log(shipPart.kind);
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
        shipPieces.Add(part);
        damage.hitpoints += shipPart.durability;
        part.SetActive(false);
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

    public bool BoardShip()
    {
        if (CanFly())
        {
            inFlight = true;
            skeletonRenderer.enabled = false;
            if (partAttached[ShipPartEnum.Shield]) shieldAnim.SetActive(true);
            startTime = 0;
            return true;
        }
        return false;
    }

    public void Kersplode()
    {
        Debug.Log("Kersplode!");
        if (explosion != null)
        {
            explosion.Play();
            GameManager.getInstance().player.GetComponent<PlayerController>().Eject();
            GameManager.getInstance().player.transform.rotation = startRotation;
            inFlight = false;
            damage.hitpoints = 0;
            SpawnParts();
            this.transform.position = startPosition;
            this.transform.rotation = startRotation;
            skeletonRenderer.enabled = true;
            partAttached[ShipPartEnum.LGun] = false;
            partAttached[ShipPartEnum.LWing] = false;
            partAttached[ShipPartEnum.RGun] = false;
            partAttached[ShipPartEnum.RWing] = false;
            partAttached[ShipPartEnum.Shield] = false;
            partAttached[ShipPartEnum.Engine] = false;
            lGunRenderer.sprite = null;
            rGunRenderer.sprite = null;
            lWingRenderer.sprite = null;
            rWingRenderer.sprite = null;
            engineRenderer.sprite = null;
            shieldAnim.s0 = null;
            shieldAnim.s1 = null;
            shieldAnim.s2 = null;
            shieldAnim.s3 = null;
            shieldRenderer.sprite = null;
            startTime = 0;
            canExplode = false;
        }
    }

    public void SpawnParts()
    {


        if (shipPieces.Count > 0)
        {
            Random rnd = new Random();

            int part1 = Random.Range(0, shipPieces.Count);
            int part2 = part1;
            if (shipPieces.Count > 1)
            {
                part2 = Random.Range(0, shipPieces.Count);
                while (part2 == part1)
                {
                    part2 = Random.Range(0, shipPieces.Count);
                }
                shipPieces[part2].SetActive(true);
                shipPieces[part2].transform.localPosition = this.transform.localPosition;
                shipPieces[part2].GetComponent<Rigidbody2D>().gravityScale = 1;

            }
            shipPieces[part1].SetActive(true);
            shipPieces[part1].transform.localPosition = this.transform.localPosition;
            shipPieces[part1].GetComponent<Rigidbody2D>().gravityScale = 1;


            for (int i = 0; i < shipPieces.Count; i++)
            {
                if (i != part1 && i != part2)
                {
                    GameObject toDestroy = shipPieces[i];
                    shipPieces.Remove(toDestroy);
                    Destroy(toDestroy);
                }
                else
                {
                    shipPieces[i] = null;
                }
            }
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Time " + startTime);
        Debug.Log("Collision in playerShip aaah! " + collision.gameObject.layer);
        if (collision.gameObject.layer == 0 && canExplode && inFlight)
        {
            Kersplode();
        }
    }
}
