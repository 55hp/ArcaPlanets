using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonManager : Singleton<MoonManager>
{

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
                playing = false;
                break;
            case GameManager.GameState.Win:
                EffectsReset();
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
    }

    #region POWER UP EFFECTS METHODS

    //[201] RED MOON
    //Cambio sprite e aumenta il danno
    public IEnumerator RedMoon(float time)
    {
        foreach (Moon m in otherMoons)
        {
            if (m.gameObject.activeSelf)
            {
                m.SetDmg(2);
                m.ChangeMoonSprite(redMoonSkin);
            }
        }
            
        yield return new WaitForSeconds(time);
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
            for (int i = 0; i < howManyShythes; i++)
            {
                Moon scythe = Instantiate(moonPref, mainMoon.transform.position, Quaternion.identity);
                scythe.ChangeMoonSprite(halfMoonSkin);
                scythe.Spin(true);
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



    public void EffectsReset()
    {
        foreach (Moon m in otherMoons)
        {

        //Red moon reset
        m?.SetDmg(1);
            
        //Sprite reset
        m?.ChangeMoonSprite(standardMoonSkin);

        //Stop spinning
        m?.Spin(false);
            
        }
    }
    #endregion

}
