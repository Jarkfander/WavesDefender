using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject _Board;
	public float targetRotationY;
	public float speed;
	public GameObject panelBuilding;
	private GameObject Flag;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(targetRotationY - _Board.transform.rotation.eulerAngles.y)>0.01 ) {
			_Board.transform.rotation = Quaternion.Lerp (_Board.transform.rotation, Quaternion.Euler (0.0f, targetRotationY, 0.0f), Time.deltaTime*speed);
		}

		if(Input.GetMouseButtonDown(0)){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast (ray, out hit))
			{
				if(hit.transform.CompareTag("Flag"))
				{
					panelBuilding.SetActive (true);
					Flag = hit.transform.gameObject;
				}
			}
		}

		if(Input.GetMouseButtonDown(1)){
			panelBuilding.SetActive (false);
		}
	}

	public void RotateBoardClockwise(){
		targetRotationY = targetRotationY + 90;
	}

	public void RotateBoardAnticlockwise(){
		targetRotationY = targetRotationY - 90;
	}

	public void BuildBrassOnFlag(){

	}

	public void BuildPrismOnFlag(){
		Flag.GetComponent<BuildFlagController> ().build (0);
		panelBuilding.SetActive (false);
	}
}
