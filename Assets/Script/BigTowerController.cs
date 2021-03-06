﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTowerController : MonoBehaviour {

	private Vector3 target;
	private LineRenderer _lr;
	public Vector3 shotspawn;

	public float fireDuration = 1.0f;
	public float fireRate = 5.0f;
	private float lastshot = 0.0f;
	// Use this for initialization
	void Start () {
		_lr = GetComponent<LineRenderer> ();
		_lr.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if (lastshot + fireDuration > Time.time) {
			_lr.SetPosition (0, shotspawn);
			_lr.SetPosition (1, new Vector3(target.x, target.y -0.5f, target.z));

			_lr.enabled = true;
		} else {
			_lr.enabled = false;
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Monster")){
			if (lastshot + fireRate < Time.time) {
				other.GetComponent<MobController> ().TakeDamage (1337, BuildingType.NONE);
				target = other.transform.position;
				lastshot = Time.time;
			}
		}
	}
}
