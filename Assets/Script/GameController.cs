using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject _Board;
	public float targetRotationY;
	public bool _rotationIsClockwise;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(targetRotationY - _Board.transform.rotation.eulerAngles.y)>0.01 ) {
			if (_rotationIsClockwise) {
				_Board.transform.rotation = Quaternion.Euler (0.0f, _Board.transform.rotation.eulerAngles.y + 0.5f, 0.0f);
			} else {
				_Board.transform.rotation = Quaternion.Euler (0.0f, _Board.transform.rotation.eulerAngles.y - 0.5f, 0.0f);
			}
		}
	}

	public void RotateBoardClockwise(){
		targetRotationY = targetRotationY + 90;
		_rotationIsClockwise = true;
	}

	public void RotateBoardAnticlockwise(){
		targetRotationY = targetRotationY - 90;
		_rotationIsClockwise = true;
	}
}
