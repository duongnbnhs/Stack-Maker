using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    public GameObject menuUi;
    public GameObject finishUi;
    public void OpenMenuUI()
    {
        menuUi.SetActive(true);
        finishUi.SetActive(false);
    }
    public void OpenFinishUI()
    {
        menuUi.SetActive(false);
        finishUi.SetActive(true);
        GameManager.Instance.ChangeState(GameState.Finish);
    }
    public void PlayButton()
    {
        menuUi.SetActive(false);
        GameManager.Instance.ChangeState(GameState.Play);
        LevelManager.Instance.LoadLevel(1);
    }
    public void NextLevelButton()
    {
        finishUi.SetActive(false);
        GameManager.Instance.ChangeState(GameState.Play);
        LevelManager.Instance.NextLevel();
    }
    public void RePlayButton()
    {
        finishUi.SetActive(false);
        GameManager.Instance.ChangeState(GameState.Play);
        LevelManager.Instance.LoadLevel(LevelManager.Instance.currentLvIndex);
    }
}
