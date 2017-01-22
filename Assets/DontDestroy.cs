using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class DontDestroy : MonoBehaviour {
	public static bool alreadyExists = false;
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (transform.gameObject);
		SceneManager.sceneLoaded += OnSceneLoaded;
		if (alreadyExists) {
			Destroy (this.gameObject);
		}
		alreadyExists = true;
	}

	void OnDestroy(){
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode){
		Debug.Log ("scene " + scene.name);
		if (scene.name == "Main") {
			alreadyExists = false;
			Destroy (this.gameObject);
		}


	}

}
