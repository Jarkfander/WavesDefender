using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element {
    Light = 0,
    Sound = 1,
	Void = 2
}

public class MobController : MonoBehaviour {

    [SerializeField]
    private int _healthPoints = 0;
    [SerializeField]
    private Element _element;

	[SerializeField]
	private int goldValue = 0;

	public GameObject _deathAnim;

	private GameController _gameController;

    // Update is called once per frame
    void Update()
    {
        if (_healthPoints <= 0){
            Die();
        }

		_gameController = GameObject.Find ("GameController").GetComponent <GameController> ();
    }

    private void Die() {

		_gameController.AddGold (goldValue);
		Instantiate (_deathAnim,this.transform.position, this.transform.rotation);
        Destroy(gameObject);
    }

    public void TakeDamage(int damages,BuildingType damageElement) {

		if (damageElement.Equals(BuildingType.BRASS) && _element.Equals(Element.Light)
			|| damageElement.Equals(BuildingType.PRISM) && _element.Equals(Element.Sound))
        {
            ReinforceMonster(damages);
            return;
        }
        else {
            if (_healthPoints - damages < 0){
                _healthPoints = 0;
            }
            else{
                _healthPoints -= damages;
            }
        }
    }

    public void ReinforceMonster(int damages) {
        if(_healthPoints < 30) {
            _healthPoints+=damages;
        }
    }

    public Element getMonsterElement()
    {
        return _element;
    }
}
