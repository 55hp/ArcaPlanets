using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonManager : Singleton<MoonManager>
{
      

    [SerializeField] Moon moonPrefab;

    [SerializeField] Sprite[] moonSprites;
    [SerializeField] GameObject transformationEffect;
    
    Moon moon;

    bool playing;


    private void OnEnable()
    {
        EventManager.OnStateHaveBeenChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        EventManager.OnStateHaveBeenChanged -= OnStateChanged;
    }

    public void OnStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.Boot:
                InitMoon();
                break;
            case GameManager.GameState.Ready:

                break;
            case GameManager.GameState.Play:
                playing = true;
                break;
            case GameManager.GameState.Pause:

                break;
            case GameManager.GameState.Gameover:
                CleanScreenFromMoons();
                playing = false;
                break;
            case GameManager.GameState.Win:
                CleanScreenFromMoons();
                playing = false;
                break;
        }
    }
    

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(transformationEffect, moon.transform);
        }


        if (!playing && moon != null)
        {
            Vector3 earthPosition = Earth.Instance.gameObject.transform.position;
            Vector3 startingPosition = new Vector3(earthPosition.x, earthPosition.y + 0.5f, 0);
            moon.transform.position = startingPosition;

            if ((Input.GetMouseButtonUp(0) || Input.touches.Length > 0) && GameManager.Instance.GetState() == GameManager.GameState.Ready)
            {
                HitMoon();
                GameManager.Instance.PlayGame();
            }
        }
    }

    public void HitMoon()
    {
        moon.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 250));
        GameManager.Instance.SetState(GameManager.GameState.Play);
    }

    public void InitMoon()
    {
        Vector3 earthPosition = Earth.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(earthPosition.x, earthPosition.y + 0.5f, 0);
        moon = Instantiate(moonPrefab, startingPosition, Quaternion.identity);
    }

    
    public void MoonOutOfScreen()
    {

        EventManager.LoseLife();
        if (playing)
            RestartMainMoon();
    }

    public void RestartMainMoon()
    {

        Vector3 earthPosition = Earth.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(earthPosition.x, earthPosition.y + 0.5f, 0);
        moon = Instantiate(moonPrefab, startingPosition, Quaternion.identity);
        GameManager.Instance.GameReady();
        EffectsReset();
    }


    #region POWER UP EFFECTS METHODS

    /// <summary>
    /// Powerup di base
    /// </summary>
    public abstract class BaseMoonPowerUp : PowerUpDefinition
    {
        /// <summary>
        /// Manager per la luna associata
        /// </summary>
        public readonly MoonManager MoonManager;

        public BaseMoonPowerUp(MoonManager MoonManager)
        {
            this.MoonManager = MoonManager;
        }
    }

    //[201] RED MOON
    //Cambio sprite e aumenta il danno
    public class RedMoonPowerUp : BaseMoonPowerUp
    {
        /*
        //[201] RED MOON
        //Cambio sprite e aumenta il danno
        public IEnumerator RedMoon(float time)
        {
            moon.SetDmg(2);
            moon.ChangeMoonSprite(moonSprites[1]);

            yield return new WaitForSeconds(time);

            moon.SetDmg(1);
            moon.ChangeMoonSprite(moonSprites[0]); 
        }
        */

        /// <summary>
        /// Crea il power up
        /// </summary>
        /// <param name="MoonManager"></param>
        /// <param name="Duration"></param>
        public RedMoonPowerUp(MoonManager MoonManager) : base(MoonManager)
        {
        }

        public override void Activate()
        {
            MoonManager.moon.SetDmg(2);
            MoonManager.moon.ChangeMoonSprite(MoonManager.moonSprites[1]);
        }

        public override void Deactivate()
        { 
            MoonManager.moon.SetDmg(1);
            MoonManager.moon.ChangeMoonSprite(MoonManager.moonSprites[0]);
        }
    }


    /// <summary>
    /// [202] MOON Scythes
    //  Cambio sprite + Creazione di altre n lune con sprite specifici.
    /// </summary>
    public class MoonScythes : BaseMoonPowerUp
    {
        public MoonScythes(MoonManager MoonManager) : base(MoonManager)
        {
        }

        public override void Activate()
        {
            MoonManager.moon.MoonSpinning(true);
        }

        public override void Deactivate()
        {
            MoonManager.moon.MoonSpinning(false);
        }
    }
    /// <summary>
    ///  [203] FULL MOON aumenta di dimensione
    /// </summary>
    public class FullMoon : BaseMoonPowerUp
    {
        public FullMoon(MoonManager MoonManager) : base(MoonManager)
        {
        }

        public override void Activate()
        {
            MoonManager.moon?.ChangeMoonSprite(MoonManager.moonSprites[2]);
            MoonManager.moon.GetComponent<CircleCollider2D>().radius = 0.82f;
        }

        public override void Deactivate()
        {
            MoonManager.moon?.ChangeMoonSprite(MoonManager.moonSprites[0]);
            MoonManager.moon.GetComponent<CircleCollider2D>().radius = 0.52f;
        }
    }
      

    public void CleanScreenFromMoons()
    {
        moon.gameObject.SetActive(false);
    }

    public void EffectsReset()
    {
        moon?.SetDmg(1);
        moon?.ChangeMoonSprite(moonSprites[0]);
        moon?.MoonSpinning(false);
        moon.GetComponent<CircleCollider2D>().radius = 0.52f;
    }
    #endregion


    /*
    public float initialMoonSpeed = 250;
    [SerializeField] Moon moonPref;

    [SerializeField] Sprite standardMoonSkin;
    [SerializeField] Sprite redMoonSkin;
    [SerializeField] Sprite fullMoonSkin;
    [SerializeField] Sprite halfMoonSkin;

    Moon mainMoon;

    List<Moon> otherMoons = new List<Moon>();
    int moonsInGame;

    bool playing;


    private void OnEnable()
    {
        EventManager.OnStateHaveBeenChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        EventManager.OnStateHaveBeenChanged -= OnStateChanged;
    }
    
    public void OnStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.Boot:
                InitGame();
                break;
            case GameManager.GameState.Play:
                break;
            case GameManager.GameState.Pause:
                break;
            case GameManager.GameState.Gameover:
                EffectsReset();
                CleanScreenFromMoons();
                playing = false;
                break;
            case GameManager.GameState.Win:
                EffectsReset();
                CleanScreenFromMoons();
                playing = false;
                break;
        }
    }

    private void Update()
    {
        if (!playing && mainMoon != null)
        {
            // Align ball position to the Earth position
            Vector3 paddlePosition = Earth.Instance.transform.position;
            Vector3 moonPos = new Vector3(paddlePosition.x, paddlePosition.y + .5f, 0);
            mainMoon.transform.position = moonPos;

            if (Input.GetMouseButtonUp(0) || Input.touches.Length > 0)
            {
                mainMoon.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, initialMoonSpeed));
                EventManager.ChangeGameState(GameManager.GameState.Play);
                playing = true;
            }
        }
    }

    public void InitGame()
    {
        Vector3 earthPosition = Earth.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(earthPosition.x, earthPosition.y + 2f, 0);
        if(mainMoon == null)
        {
            mainMoon = Instantiate(moonPref, startingPosition, Quaternion.identity);
        }
        else
        {
            mainMoon.gameObject.SetActive(true);
        }
        moonsInGame = 1;
    }

    

    public void MoonOutOfScreen(Moon me)
    {

        if (me != mainMoon)
        {
            otherMoons.Remove(me);
            Destroy(me);
        }
        else
        {
            me.gameObject.SetActive(false);
        }
        
        moonsInGame--;

        if(moonsInGame == 0)
        {
            EventManager.LoseLife();
            if(playing)
            RestartMainMoon();
        }
    }
    
    public void RestartMainMoon()
    {
        
        Vector3 earthPosition = Earth.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(earthPosition.x, earthPosition.y + .7f, 0);
        mainMoon.gameObject.SetActive(true);
        moonsInGame++;
        mainMoon.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        mainMoon.transform.position = startingPosition;
        mainMoon.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, initialMoonSpeed));
        EffectsReset();
    }

    #region POWER UP EFFECTS METHODS

    //[201] RED MOON
    //Cambio sprite e aumenta il danno
    public IEnumerator RedMoon(float time)
    {
        mainMoon.SetDmg(2);
        mainMoon.ChangeMoonSprite(redMoonSkin);
        foreach (Moon m in otherMoons)
        {
            if (m.gameObject.activeSelf)
            {
                m.SetDmg(2);
                m.ChangeMoonSprite(redMoonSkin);
            }
        }
            
        yield return new WaitForSeconds(time);
        mainMoon.SetDmg(1);
        mainMoon.ChangeMoonSprite(standardMoonSkin);
        foreach (Moon m in otherMoons)
        {
            if (m.gameObject.activeSelf)
            {
                m.SetDmg(1);
                m.ChangeMoonSprite(standardMoonSkin);
            }
        }

    }

    //[202] MOON Scythes
    //Cambio sprite + Creazione di altre n lune con sprite specifici.
    public void MoonScythes(int howManyShythes)
    {
        if (moonsInGame == 1)
        {
            mainMoon.ChangeMoonSprite(halfMoonSkin);
            for (int i = 0; i < howManyShythes; i++)
            {
                Moon scythe = Instantiate(moonPref, mainMoon.transform.position, Quaternion.identity);
                scythe.ChangeMoonSprite(halfMoonSkin);
                scythe.MoonSpinning(true);
                otherMoons.Add(scythe);
                moonsInGame++;
            }
        }
    }


    //[203] FULL MOON aumenta di dimensione
    public IEnumerator FullMoon(float time)
    {
        mainMoon?.ChangeMoonSprite(fullMoonSkin);
        
        foreach (Moon m in otherMoons)
        {
            m?.ChangeMoonSprite(fullMoonSkin);
        }

        yield return new WaitForSeconds(time);

        mainMoon?.ChangeMoonSprite(standardMoonSkin);

        foreach (Moon m in otherMoons)
        {
            m?.ChangeMoonSprite(standardMoonSkin);
        }
        
    }

    public void CleanScreenFromMoons()
    {
        mainMoon.gameObject.SetActive(false);
        foreach (Moon m in otherMoons)
        {
            Destroy(m);
        }
        otherMoons.Clear();
        moonsInGame = 0;
    }

    public void EffectsReset()
    {

        mainMoon?.SetDmg(1);
        mainMoon?.ChangeMoonSprite(standardMoonSkin);
        mainMoon?.MoonSpinning(false);

        foreach (Moon m in otherMoons)
        {

        //Red moon reset
        m?.SetDmg(1);
            
        //Sprite reset
        m?.ChangeMoonSprite(standardMoonSkin);

        //Stop spinning
        m?.MoonSpinning(false);
            
        }
    }
    #endregion
    */
    }
