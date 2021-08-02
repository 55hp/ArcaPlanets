using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonManager : Singleton<MoonManager>
{
    [SerializeField] private Moon moonPrefab;


    //[0] - normal || [1] - red  || [2] - half moon || [3] - full 
    [SerializeField] Sprite[] moonSprites;


    private List<Moon> activeMoons = new List<Moon>();

    int moonsInStage;

    private Rigidbody2D activeMoonRb;
    Vector3 normalSize;

    public float initialMoonSpeed = 250;
    bool playing;


    private void OnEnable()
    {
        EventManager.OnStateHaveBeenChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        EventManager.OnStateHaveBeenChanged -= OnStateChanged;
    }
    
    private void Update()
    {
        if (!playing && (activeMoons.Count != 0))
        {
            // Align ball position to the Earth position
            Vector3 paddlePosition = Earth.Instance.transform.position;
            Vector3 moonPos = new Vector3(paddlePosition.x, paddlePosition.y + .5f, 0);
            activeMoons[0].transform.position = moonPos;

            if (Input.GetMouseButtonUp(0) || Input.touches.Length > 0)
            {
                activeMoonRb.AddForce(new Vector2(0, initialMoonSpeed));
                EventManager.ChangeGameState(GameManager.GameState.Play);
            }
        }
    }


    public void OnStateChanged(GameManager.GameState newState)
    {
        switch (newState)
        {
            case GameManager.GameState.Boot:
                InitBall();
                playing = false;
                break;
            case GameManager.GameState.Play:
                playing = true;
                break;
            case GameManager.GameState.Pause:
                break;
            case GameManager.GameState.Gameover:
                foreach(Moon m in activeMoons)
                m.gameObject.SetActive(false);

                //EffectsReset();
                break;
            case GameManager.GameState.Win:
                foreach (Moon m in activeMoons)
                m.gameObject.SetActive(false);

                //EffectsReset();
                break;
        }
    }


    public void InitBall()
    {

        Vector3 earthPosition = Earth.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(earthPosition.x, earthPosition.y + 2f, 0);
        

        if(activeMoons.Count == 0)
        {
            var moon = Instantiate(moonPrefab, startingPosition, Quaternion.identity);
            activeMoons.Add(moon);
            normalSize = activeMoons[0].transform.localScale;
        }
        else
        {
            activeMoons[0].gameObject.SetActive(true);
            ResetBall();
        }

        moonsInStage = activeMoons.Count;
        activeMoonRb = activeMoons[0].GetComponent<Rigidbody2D>();
        
    }

    public void MoonOutOfScreen(Moon me)
    {
        if (activeMoons.Count == 1)
        {
            ResetBall();
        }
        else
        {
            activeMoons.Remove(me);
        }
    }
    
    public void ResetBall()
    {
        foreach (Moon m in activeMoons)
        {
            if(m != null)
            {
                var lastMoon = m;
                Vector3 paddlePosition = Earth.Instance.gameObject.transform.position;
                Vector3 startingPosition = new Vector3(paddlePosition.x, paddlePosition.y + .7f, 0);

                lastMoon.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                lastMoon.transform.position = startingPosition;
                break;
            }
        }
    }

    #region POWER UP EFFECTS METHODS

    //RED MOON
    //Cambio sprite e aumenta il danno
    public IEnumerator RedMoon(float time)
    {
        //TODO Change Sprite
        foreach (Moon m in activeMoons)
        {
            m.SetDmg(2);
            m.GetComponent<SpriteRenderer>().sprite = moonSprites[1];
        }
            
        yield return new WaitForSeconds(time);
        foreach (Moon m in activeMoons)
        {
            m.SetDmg(1);
            m.GetComponent<SpriteRenderer>().sprite = moonSprites[0];
        }

    }

    //MOON Scythes
    //Cambio sprite + Creazione di altre 2 lune con sprite specifici.
    public IEnumerator MoonScythes(int howManyShythes)
    {
        foreach(Moon m in activeMoons)
        m.GetComponent<SpriteRenderer>().sprite = moonSprites[2];


        //Crea 2 surrogati
        moonsInStage += howManyShythes;
        for(int i = 0; i < howManyShythes; i++)
        {
            Moon newMoon = moonPrefab;
            Instantiate(newMoon);
            activeMoons.Add(newMoon);
            newMoon.GetComponent<SpriteRenderer>().sprite = moonSprites[2];
            newMoon.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, initialMoonSpeed));
        }
        yield return null;
    }


    //FULL MOON aumenta di dimensione
    public IEnumerator FullMoon(float time)
    {
        foreach (Moon m in activeMoons)
        {
            m.transform.localScale = normalSize * 1.5f;
            m.GetComponent<SpriteRenderer>().sprite = moonSprites[3];
            //m.GetComponent<CircleCollider2D>().radius = 0.82f;
        }

        yield return new WaitForSeconds(time);

        foreach (Moon m in activeMoons)
        {
            m.transform.localScale = normalSize;
            m.GetComponent<SpriteRenderer>().sprite = moonSprites[0];
            //m.GetComponent<CircleCollider2D>().radius = 0.52f;
        }
    }



    public void EffectsReset()
    {
        foreach (Moon m in activeMoons)
        {

            //Red moon reset
            m.SetDmg(1);

            //Size reset
            m.transform.localScale = normalSize;

            //Sprite reset
            m.GetComponent<SpriteRenderer>().sprite = moonSprites[0];
        }

        //Collider reset
        //activeMoon.GetComponent<CircleCollider2D>().radius = 0.52f;
    }
    


    #endregion

}
