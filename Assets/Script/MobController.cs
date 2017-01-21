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

    // Update is called once per frame
    void Update()
    {
        if (_healthPoints <= 0){
            Die();
        }
    }

    private void Die() {

        Destroy(gameObject);
    }

    public void TakeDamage(int damages,BuildingType damageElement) {

        if (damageElement.Equals(BuildingType.BRASS) && _element.Equals(Element.Sound)
                || damageElement.Equals(BuildingType.PRISM) && _element.Equals(Element.Light))
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
