using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState { Menu, Play, Finish }
public class GameManager : Singleton<GameManager>
{
    private GameState gameState;
    //[SerializeField] GameObject menu;
    private void Start()
    {
        ChangeState(GameState.Menu);
        UiManager.Instance.OpenMenuUI();
    }

    public void ChangeState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public bool IsState(GameState gameState)
    {
        return this.gameState == gameState;
    }
}
