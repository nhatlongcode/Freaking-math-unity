using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        //SceneManager.LoadScene(0);	
	}
	
	public void playButtonOnClick()
    {
        SceneManager.LoadScene(1);
    }
}
