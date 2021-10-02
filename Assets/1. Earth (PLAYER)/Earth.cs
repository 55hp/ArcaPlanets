using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Earth : Singleton<Earth>
{
    /// <summary>
    /// The Earth Class define charateristics and behaviors of the Player of the game rapresented by a Earth Planet/ship
    /// </summary>
    int myHp;
    public bool testCondition = true;

    [SerializeField] GameObject cockpit;
    [SerializeField] Sprite[] cockpitSprites;


    [SerializeField] GameObject body;
    [SerializeField] GameObject propulsor;

    [SerializeField] GameObject lowerShield;

    [SerializeField] GameObject myWeapon;
    [SerializeField] GameObject leftCannon;
    [SerializeField] GameObject rightCannon;
    [SerializeField] GameObject earthBullet;


    [SerializeField] Sprite[] earthSprites;
    [SerializeField] Sprite[] lowerShieldSprites;

    [SerializeField] GameObject transformationEffect;
    IEnumerator shieldLoop;

    bool alive;
    Vector3 mySize;
    Animator myAnimator;

    private void OnEnable()
    {
        EventManager.OnStateHaveBeenChanged += OnStateChanged;
        EventManager.OnLifeLost += LoseLife;
    }

    private void OnDisable()
    {
        EventManager.OnStateHaveBeenChanged -= OnStateChanged;
        EventManager.OnLifeLost -= LoseLife;
    }

    private void Start()
    {
        mySize = transform.localScale;
        myAnimator = GetComponent<Animator>();
    }

    public void InitEarth()
    {
        cockpit.GetComponent<SpriteRenderer>().sprite = cockpitSprites[0];

        myHp = 3;
        alive = true;
        lowerShield.SetActive(false);

        leftCannon.GetComponent<ShootingModule>().SetShootingModule(ShootingModule.ShootingType.FIXED_RATE_ONE, earthBullet, earthBullet, 0, 0.8f);
        rightCannon.GetComponent<ShootingModule>().SetShootingModule(ShootingModule.ShootingType.FIXED_RATE_ONE, earthBullet, earthBullet, 0.4f, 0.8f);
    }

    public void OnStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.Boot:
                InitEarth();
                shieldLoop = AnimationController.LoopingCicle(lowerShield, lowerShieldSprites, 0.1f);
                gameObject.GetComponent<EarthController>().ResetEarthPosition();
                break;
            case GameManager.GameState.Ready:
                gameObject.GetComponent<EarthController>().GoPlay(true);
                break;
            case GameManager.GameState.Play:
                Invulnerability(false);
                break;
            case GameManager.GameState.Pause:
                break;
            case GameManager.GameState.Gameover:
                gameObject.GetComponent<EarthController>().GoPlay(false);
                Invulnerability(true);
                EffectsReset();
                break;
            case GameManager.GameState.Win:
                gameObject.GetComponent<EarthController>().GoPlay(false);
                Invulnerability(true);
                EffectsReset();
                break;
        }
    }


    //SOLO PER TESTING
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Bigger
            Instantiate(transformationEffect, this.transform);
            myAnimator.SetTrigger("Bigger");

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Smaller
            Instantiate(transformationEffect, this.transform);
            myAnimator.SetTrigger("Smaller");

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //Gun
            Instantiate(transformationEffect, this.transform);
            myAnimator.SetTrigger("Gun");

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            myAnimator.SetTrigger("Hit");

        }

    }




    /// <summary>
    /// This method will be called every time something occurs that deals dmg to our EarthShip by the event it is registered to.
    /// </summary>
    /// <param name="dmg"></param>
    public void LoseLife()
    {
        if (gameObject.layer == 8)
        {
            myHp--;
            if (myHp <= 0)
            {
                alive = false;
                EventManager.ChangeGameState(GameState.Gameover);
            }
        }
    }


    public bool IsAlive()
    {
        return alive;
    }

    public int GetHP()
    {
        return myHp;
    }

    public void SetHP(int amount)
    {
        if(gameObject.layer == 8)
        myHp = amount;
    }

    public void AddHp()
    {
        myHp++;
    }

    /// <summary>
    ///  When the Earth collides with the moon it will be applied a force on the Moon based on the point of collision.
    ///  This method will be (probably) modified later.
    /// </summary>
    /// <param name="coll"></param>
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Moon")
        {
            Rigidbody2D ballRb = coll.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = coll.contacts[0].point;
            Vector3 paddleCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

            ballRb.velocity = Vector2.zero;

            float difference = paddleCenter.x - hitPoint.x;
            
            if (hitPoint.x < paddleCenter.x)
            {
                ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * 200)), 50));
            }
            else
            {
                ballRb.AddForce(new Vector2((Mathf.Abs(difference * 200)), 50));
            }
            StartCoroutine(AnimationController.BlinkAnimation(cockpit, cockpitSprites[0], cockpitSprites[1], 0.1f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MobProjectile")
            StartCoroutine(AnimationController.BlinkAnimation(cockpit, cockpitSprites[0], cockpitSprites[2], 0.1f));
    }

    public void Invulnerability(bool on)
    {
        if(on)
            gameObject.layer = 26;
        else
            gameObject.layer = 8;
    }



    /// <summary>
    /// Power ups are coroutines that occurs for a determinated amount of time and applies different effects
    /// </summary>
    #region POWER UP EFFECTS REGION
        
    public void EffectsReset()
    {
        //Bigger effects reset
        this.GetComponent<PolygonCollider2D>().enabled = false;

        //Smaller effects reset
        propulsor.GetComponent<SpriteRenderer>().sprite = earthSprites[7];
        this.GetComponent<CapsuleCollider2D>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = true;

        //Lower Shield effect reset
        //lowerShield.SetActive(false);

        //Double bullets effect reset
        myWeapon.SetActive(false);

    }

    public IEnumerator Bigger(float timer)
    {
        this.GetComponent<PolygonCollider2D>().enabled = true;

        yield return new WaitForSeconds(timer);

        this.GetComponent<PolygonCollider2D>().enabled = false;
    }
    
    public IEnumerator Smaller(float timer)
    {
        propulsor.GetComponent<SpriteRenderer>().sprite = null;
        this.GetComponent<CapsuleCollider2D>().enabled = true;
        this.GetComponent<CircleCollider2D>().enabled = false;

        yield return new WaitForSeconds(timer);
        
        propulsor.GetComponent<SpriteRenderer>().sprite = earthSprites[7];
        this.GetComponent<CapsuleCollider2D>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = true;
    }

    public IEnumerator LowShield(float timer)
    {
        lowerShield.SetActive(true);
        StartCoroutine(AnimationController.GoIn(lowerShield, 0.1f));
        StartCoroutine(shieldLoop);
        yield return new WaitForSeconds(timer);
        lowerShield.SetActive(false);
        StopCoroutine(shieldLoop);

    }

    public IEnumerator DoubleBullets(float timer)
    {
        myWeapon.SetActive(true);
        leftCannon.GetComponent<ShootingModule>().TurnOn();
        rightCannon.GetComponent<ShootingModule>().TurnOn();
        yield return new WaitForSeconds(timer);
        leftCannon.GetComponent<ShootingModule>().TurnOff();
        rightCannon.GetComponent<ShootingModule>().TurnOff();
        myWeapon.SetActive(false);
    }
    #endregion



    

}
