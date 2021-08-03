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
    [SerializeField] GameObject body;
    [SerializeField] GameObject myWing;
    [SerializeField] GameObject propulsor;

    [SerializeField] GameObject lowerShield;

    [SerializeField] GameObject[] projectiles;
    [SerializeField] ShootingModule leftCannon;
    [SerializeField] ShootingModule rightCannon;



    [SerializeField] GameObject myWeapon;


    [SerializeField] Sprite[] earthSprites;


    bool alive;
    Vector3 mySize;


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
    }

    public void InitEarth()
    {
        myHp = 3;
        alive = true;
        lowerShield.SetActive(false);

        leftCannon.InitGun(projectiles[0], 0.4f, 0.8f);
        rightCannon.InitGun(projectiles[0], 0, 0.8f);
    }

    public void OnStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.Boot:
                InitEarth();
                gameObject.GetComponent<EarthController>().ResetEarthPosition();
                break;
            case GameManager.GameState.Play:
                gameObject.GetComponent<EarthController>().GoPlay(true);
                Invulnerability(false);
                break;
            case GameManager.GameState.Pause:
                break;
            case GameManager.GameState.Gameover:
                gameObject.GetComponent<EarthController>().GoPlay(false);
                Invulnerability(true);
                break;
            case GameManager.GameState.Win:
                gameObject.GetComponent<EarthController>().GoPlay(false);
                Invulnerability(true);
                break;
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
        }
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
        myWing.GetComponent<SpriteRenderer>().sprite = earthSprites[3];

        //Smaller effects reset
        myWing.GetComponent<SpriteRenderer>().sprite = earthSprites[3];
        propulsor.GetComponent<SpriteRenderer>().sprite = earthSprites[7];
        this.GetComponent<CapsuleCollider2D>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = true;

        //Lower Shield effect reset
        lowerShield.SetActive(false);

        //Double bullets effect reset
        myWeapon.GetComponent<SpriteRenderer>().enabled = false;
        leftCannon.TurnOff();
        rightCannon.TurnOff();

    }

    public IEnumerator Bigger(float timer)
    {
        myWing.GetComponent<SpriteRenderer>().sprite = earthSprites[4];
        this.GetComponent<PolygonCollider2D>().enabled = true;

        yield return new WaitForSeconds(timer);

        this.GetComponent<PolygonCollider2D>().enabled = false;
        myWing.GetComponent<SpriteRenderer>().sprite = earthSprites[3];
    }
    
    public IEnumerator Smaller(float timer)
    {

        myWing.GetComponent<SpriteRenderer>().sprite = null;
        propulsor.GetComponent<SpriteRenderer>().sprite = null;
        this.GetComponent<CapsuleCollider2D>().enabled = true;
        this.GetComponent<CircleCollider2D>().enabled = false;

        yield return new WaitForSeconds(timer);

        myWing.GetComponent<SpriteRenderer>().sprite = earthSprites[3];
        propulsor.GetComponent<SpriteRenderer>().sprite = earthSprites[7];
        this.GetComponent<CapsuleCollider2D>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = true;
    }

    public IEnumerator LowShield(float timer)
    {
        lowerShield.SetActive(true);
        yield return new WaitForSeconds(timer);
        lowerShield.SetActive(false);
             
    }

    public IEnumerator DoubleBullets(float timer)
    {

        myWeapon.GetComponent<SpriteRenderer>().enabled = true;
        leftCannon.TurnOn();
        rightCannon.TurnOn();

        yield return new WaitForSeconds(timer);
        myWeapon.GetComponent<SpriteRenderer>().enabled = false;
        leftCannon.TurnOff();
        rightCannon.TurnOff();
    }
    #endregion



}
