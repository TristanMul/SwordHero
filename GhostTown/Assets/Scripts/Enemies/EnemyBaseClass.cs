using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour
{
    // Shared variables all enemies have.
    public float speed;
    public float health;
    
    // All states enemies can have.
    public EnemyState enemyState;
    public enum EnemyState{
        Idle,
        Move,
        Attack,
        Damaged,
        Death,
    }
    
}
