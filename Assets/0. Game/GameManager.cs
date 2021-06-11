using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    private void Start()
    {
        GameStateManager.Instance.ChangeState(GameStateManager.GameState.Boot);
    }


}