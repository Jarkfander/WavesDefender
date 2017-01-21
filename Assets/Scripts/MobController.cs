using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element {
    Light = 0,
    Sound = 1
}

public class MobController : MonoBehaviour {

    [SerializeField]
    private int _healthPoints = 0;
    [SerializeField]
    private Element _element;


	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (_healthPoints <= 0)
        {
            Die();
        }

    }

    private void Die() {
        Destroy(gameObject);
    }
}
