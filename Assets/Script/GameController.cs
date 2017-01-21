using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject _Board;
	public float targetRotationY;
	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(targetRotationY - _Board.transform.rotation.eulerAngles.y)>0.01 ) {
			_Board.transform.rotation = Quaternion.Lerp (_Board.transform.rotation, Quaternion.Euler (0.0f, targetRotationY, 0.0f), Time.deltaTime*speed);
		}
	}

	public void RotateBoardClockwise(){
		targetRotationY = targetRotationY + 90;
	}

	public void RotateBoardAnticlockwise(){
		targetRotationY = targetRotationY - 90;
	}
}
