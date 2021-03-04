using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour
{

    public float speed;
    public float health;
    
    public EnemyState enemyState;
    public enum EnemyState{
        Idle,
        Move,
        Attack,
        Damaged,
        Death,
    }
    
}
