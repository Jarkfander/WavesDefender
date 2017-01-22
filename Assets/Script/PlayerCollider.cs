using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{

    private GameController _gameController = null;

	[SerializeField]
	private UnityEngine.UI.Text livesText;

	[SerializeField]
	private AudioClip gainLifeSound;

	[SerializeField]
	private AudioClip loseLifeSound;

	private int playerLife = 3;

	AudioSource audiosource;

    void Start()
    {
		_gameController = GameObject.Find("GameController").GetComponent<GameController>();
		livesText.text = "Structure : " + playerLife;
		audiosource = GetComponent<AudioSource> ();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        { 
			if(other.GetComponent<MobController>().getMonsterElement()== Element.Void){
				playerLife++;
				audiosource.clip = gainLifeSound;
				audiosource.Play();


			}else{
				playerLife--;
				audiosource.clip = loseLifeSound;
				audiosource.Play();
			}

			Destroy(other.gameObject);

			livesText.text = "Structure : " + playerLife;

			if (playerLife <= 0) {
				_gameController.GameOver ();
			}
        }
    }

}
