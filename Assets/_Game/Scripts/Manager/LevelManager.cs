using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager> 
{
    public List<Level> levels = new List<Level>();
    public Player player;
    Level currentLevel;
    public int currentLvIndex = 1;
    /*public void Start()
    {
        LoadLevel(1);
        OnInit();
    }*/
    public void LoadLevel(int indexLevel)
    {
        
            if (currentLevel != null)
            {
                Destroy(currentLevel.gameObject);
            }
            currentLevel = Instantiate(levels[indexLevel - 1]);
            OnInit();
        
    }
    public void NextLevel()
    {
        currentLvIndex++;
        if(currentLvIndex > levels.Count)
        {
            currentLvIndex = levels.Count;
        }
        LoadLevel(currentLvIndex);
    }
    public void OnInit()
    {
        player.transform.position = currentLevel.startPoint.position;
        player.OnInit();
    }    
    public void OnFinish()
    {
        UiManager.Instance.OpenFinishUI();
        GameManager.Instance.ChangeState(GameState.Finish);
    }
}
