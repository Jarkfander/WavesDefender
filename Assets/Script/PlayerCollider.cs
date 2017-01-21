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
            Destroy(other.gameObject);

			playerLife--;

			livesText.text = "Structure : " + playerLife;

			if (playerLife <= 0) {
				_gameController.GameOver ();
			}
        }
    }

}
