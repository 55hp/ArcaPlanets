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

    [SerializeField] GameObject lowerShield;
    
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
        
        leftCannon.GetComponent<ShootingScript>().SetShootingStyle(earthBullet, leftCannon, ShootingScript.ShootingType.FIXED_RATE_DOUBLE);
        rightCannon.GetComponent<ShootingScript>().SetShootingStyle(earthBullet, rightCannon, ShootingScript.ShootingType.FIXED_RATE_DOUBLE);
    }

    public void InitEarth()
    {
        cockpit.GetComponent<SpriteRenderer>().sprite = cockpitSprites[0];

        myHp = 3;
        alive = true;
        lowerShield.SetActive(false);
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
            UIManager.Instance.LoseLife();
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
        {

            StartCoroutine(AnimationController.BlinkAnimation(cockpit, cockpitSprites[0], cockpitSprites[2], 0.1f));
            gameObject.GetComponent<EarthAnimator>().HitAnimation();
        }
    }

    public void Invulnerability(bool on)
    {
        if(on)
            gameObject.layer = 26;
        else
            gameObject.layer = 8;
    }

    public void StartShooting()
    {
        StartCoroutine(Shoot());
        
    }

    public void StopShooting()
    {
        StopCoroutine(Shoot());

    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            leftCannon.GetComponent<ShootingScript>().Shoot();
            rightCannon.GetComponent<ShootingScript>().Shoot();
        }
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
        this.GetComponent<CapsuleCollider2D>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = true;

        //ShootingReset
        StopShooting();

        //Lower Shield effect reset
        //lowerShield.SetActive(false);
    }

    /// <summary>
    /// Effetto di base per la terra
    /// </summary>
    public abstract class BaseEarthPowerUp : PowerUpDefinition
    {
        public readonly Earth Earth;
        
        /// <summary>
        /// Crea un power up di base 
        /// </summary>
        /// <param name="Earth"></param>
        public BaseEarthPowerUp(Earth Earth)
        {
            this.Earth = Earth;
        }
    }

    /// <summary>
    /// Bigger
    /// </summary>
    public class BiggerPowerUp : BaseEarthPowerUp
    {
        public BiggerPowerUp(Earth Earth) : base(Earth)
        {

        }

        public override void Activate()
        { 
            Instantiate(Earth.transformationEffect, Earth.transform);
            Earth.myAnimator.SetTrigger("BiggerIn");
        }
        
        public override void Deactivate()
        { 
            Instantiate(Earth.transformationEffect, Earth.transform);
            Earth.myAnimator.SetTrigger("BiggerOut");
        }
    }

    /// <summary>
    /// Smaller
    /// </summary>
    public class SmallerPowerUp : BaseEarthPowerUp
    {
        public SmallerPowerUp(Earth Earth) : base(Earth)
        {
             
        }
        
        public override void Activate()
        {
            Earth.GetComponent<CapsuleCollider2D>().enabled = true;
            Earth.GetComponent<CircleCollider2D>().enabled = false;
            Instantiate(Earth.transformationEffect, Earth.transform);
            Earth.myAnimator.SetTrigger("SmallerIn");
        }

        public override void Deactivate()
        { 
            Earth.GetComponent<CapsuleCollider2D>().enabled = false;
            Earth.GetComponent<CircleCollider2D>().enabled = true;
            Instantiate(Earth.transformationEffect, Earth.transform);
            Earth.myAnimator.SetTrigger("SmallerOut");
        }
    }


    /// <summary>
    /// Smaller
    /// </summary>
    public class LowShieldPowerUp : BaseEarthPowerUp
    {
        public LowShieldPowerUp(Earth Earth) : base(Earth)
        {
        }

        public override void Activate()
        {
            Earth.lowerShield.SetActive(true);
            Earth.StartCoroutine(AnimationController.GoIn(Earth.lowerShield, 0.1f));
            Earth.StartCoroutine(Earth.shieldLoop);
        }

        public override void Deactivate()
        {
            Earth.lowerShield.SetActive(false);
            Earth.StopCoroutine(Earth.shieldLoop);
        }
    }
     
    /// <summary>
    /// Smaller
    /// </summary>
    public class DoubleBulletsPowerUp : BaseEarthPowerUp
    {
        public DoubleBulletsPowerUp(Earth Earth) : base(Earth)
        {

        }
        public override void Activate()
        {
            Instantiate(Earth.transformationEffect, Earth.transform);
            Earth.myAnimator.SetTrigger("GunIn");
            Instance.StartShooting();
        }

        public override void Deactivate()
        { 
            Instantiate(Earth.transformationEffect, Earth.transform);
            Earth.myAnimator.SetTrigger("GunOut");
            Instance.StopShooting();
        }
    }
    #endregion

}
