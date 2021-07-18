using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonManager : Singleton<MoonManager>
{
    [SerializeField]
    private Moon moonPrefab;
    private Moon activeMoon;

    private Rigidbody2D activeMoonRb;

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

            if (Input.GetMouseButtonDown(0) || Input.touches.Length > 0 )
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
                break;
            case GameManager.GameState.Win:
                break;
        }
    }


    public void InitBall()
    {
        Vector3 paddlePosition = Earth.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(paddlePosition.x, paddlePosition.y + .5f, 0);
        activeMoon = Instantiate(moonPrefab, startingPosition, Quaternion.identity);
        activeMoonRb = activeMoon.GetComponent<Rigidbody2D>();
    }

    #region POWER UP EFFECTS METHODS

    //RED MOON
    //Cambio sprite e aumenta il danno
    public IEnumerator RedMoon(float time)
    {

        activeMoon.SetDmg(2);
        activeMoon.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(time);
        activeMoon.SetDmg(1);
        activeMoon.GetComponent<SpriteRenderer>().color = Color.white;
    }

    //MOON Scythes
    //Cambio sprite + Creazione di altre 2 lune con sprite specifici.

    //FULL MOON
    //Raddoppio scale

    //NEW MOON
    //Dimezza scale




    #endregion

}
