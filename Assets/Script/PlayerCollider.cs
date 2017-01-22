using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{

    private GameController _gameController = null;

	[SerializeField]
	private UnityEngine.UI.Text livesText;

	private int playerLife = 3;

    void Start()
    {
		_gameController = GameObject.Find("GameController").GetComponent<GameController>();
		livesText.text = "Structure : " + playerLife;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        { 
			if(other.GetComponent<MobController>().getMonsterElement()== Element.Void){
				playerLife++;
			}else{
			playerLife--;
			}

			Destroy(other.gameObject);

			livesText.text = "Structure : " + playerLife;

			if (playerLife <= 0) {
				_gameController.GameOver ();
			}
        }
    }

}
