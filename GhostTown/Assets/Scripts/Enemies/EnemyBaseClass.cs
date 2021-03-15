using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour
{
    // Shared variables all enemies have.
    [SerializeField] protected float speed;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float attackRange;
    protected HealthBar healthBar;

    // Properties that other scripts can use to read variables of enemy.
    public float Speed {get{return speed;} protected set{speed = value;}}
    public float MaxHealth {get{return maxHealth;} protected set{maxHealth = value;}}
    public float CurrenHealth {get{return currentHealth;} set{currentHealth = value;}}
    public float AttackRange {get{return attackRange;} protected set{attackRange = value;}}
    public HealthBar HealthBar {get{return healthBar;}}
    
    // All states enemies can have.
    public EnemyState enemyState;
    public enum EnemyState{
        Idle,
        Move,
        Attack,
        Damaged,
        Death,
    }

    private void Awake() {
        // Get healthbar for this enemy.
        healthBar = transform.Find("HealthBar").GetComponent<HealthBar>();
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }
    
}
