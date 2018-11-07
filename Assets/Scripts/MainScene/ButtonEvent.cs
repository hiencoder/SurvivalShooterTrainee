using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void OpenGamePlayScene(){
		SceneManager.LoadScene("GamePlayScene",LoadSceneMode.Single);
	}

	public void ExitGame(){
		Application.Quit();
	}
}
