using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance = null;
    void Awake()
    {
        Instance = this;
    }
	
    public void CheckWin()
    {
        if(MapController.Instance.CheckWin())
        {
            UIManager.Instance.LevelAnim();
        }
    }
}
