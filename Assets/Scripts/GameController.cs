using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameController instance;
    public GameObject BG;

    public static bool GameControllerExist;
    public static bool BackGroundExist;
    public GameController Instance
    {
        get 
        { 
            if (instance == null) return instance = new GameController();
            else return instance;
        }
    }
    private void Awake()
    {
        if (!GameControllerExist)
        {
            DontDestroyOnLoad(this.gameObject);
            GameControllerExist = true;
        }
        else Destroy(this.gameObject);

        if (!BackGroundExist)
        {
            Vector3 temp = new Vector3(0, 0, 120);
            DontDestroyOnLoad(Instantiate(BG,temp,Quaternion.identity));
            BackGroundExist = true;
        }
        
    }
}
