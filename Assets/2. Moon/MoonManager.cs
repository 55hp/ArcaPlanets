using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonManager : Singleton<MoonManager>
{
    [SerializeField] private Moon moonPrefab;
    //[0] - normal || [1] - red  || [2] - half moon || [3] - full 
    [SerializeField] Sprite[] moonSprites;


    private Moon activeMoon;

    private Rigidbody2D activeMoonRb;
    Vector3 mySize;

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
        if (!playing && activeMoon != null)
        {
            // Align ball position to the Earth position
            Vector3 paddlePosition = Earth.Instance.transform.position;
            Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + .5f, 0);
            activeMoon.transform.position = ballPosition;

            if (Input.GetMouseButtonDown(0) || Input.touches.Length > 0)
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
                activeMoon.gameObject.SetActive(false);
                break;
            case GameManager.GameState.Win:
                activeMoon.gameObject.SetActive(false);
                break;
        }
    }


    public void InitBall()
    {

        Vector3 earthPosition = Earth.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(earthPosition.x, earthPosition.y + 2f, 0);
        

        if(activeMoon == null)
        {
            activeMoon = Instantiate(moonPrefab, startingPosition, Quaternion.identity);
            mySize = activeMoon.transform.localScale;
        }
        else
        {
            activeMoon.gameObject.SetActive(true);
            ResetBall();
        }
        activeMoonRb = activeMoon.GetComponent<Rigidbody2D>();
        
    }

    int activeMoons;
    public void LoseMoon()
    {
        activeMoons--;
        if (activeMoons == 1)
        {
            ResetBall();
        }
    }
    
    public void ResetBall()
    {
        
            Vector3 paddlePosition = Earth.Instance.gameObject.transform.position;
            Vector3 startingPosition = new Vector3(paddlePosition.x, paddlePosition.y + .7f, 0);

            activeMoon.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            activeMoon.transform.position = startingPosition;
        
    }

    #region POWER UP EFFECTS METHODS

    //RED MOON
    //Cambio sprite e aumenta il danno
    public IEnumerator RedMoon(float time)
    {
        //TODO Change Sprite
        activeMoon.SetDmg(2);
        activeMoon.GetComponent<SpriteRenderer>().sprite = moonSprites[1];
        yield return new WaitForSeconds(time);
        activeMoon.SetDmg(1);
        activeMoon.GetComponent<SpriteRenderer>().sprite = moonSprites[0];
    }

    //MOON Scythes
    //Cambio sprite + Creazione di altre 2 lune con sprite specifici.
    public void MoonScythes(int howManyShythes)
    {
        activeMoon.GetComponent<SpriteRenderer>().sprite = moonSprites[1];
        //Crea 2 surrogati
        activeMoons += howManyShythes;
        for(int i = 0; i < howManyShythes; i++)
        {
            //Instantiate();
            activeMoonRb.AddForce(new Vector2(0, initialMoonSpeed));
        }

    }


    //FULL MOON
    //Raddoppio scale
    public IEnumerator FullMoon(float time)
    {
        gameObject.transform.localScale = mySize*1.5f;
        activeMoon.GetComponent<SpriteRenderer>().sprite = moonSprites[3];
        yield return new WaitForSeconds(time);
        gameObject.transform.localScale = mySize;
        activeMoon.GetComponent<SpriteRenderer>().sprite = moonSprites[0];
    }



    public void EffectsReset()
    {
        //Red moon reset
        activeMoon.SetDmg(1);

        //New moon and full moon reset
        gameObject.transform.localScale = mySize;

        //Sprite
        activeMoon.GetComponent<SpriteRenderer>().sprite = moonSprites[0];

    }
    


    #endregion

}
