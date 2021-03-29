using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventManager : MonoBehaviour
{
    Attack attack;
    Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
    }

    public void OnAttackEnd() {
        weapon.StopAttack();
    }
}
