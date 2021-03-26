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
        SpecialAttack,
        Emote, 
        Fall,
    }
    protected string idleAnimation;
    protected string moveAnimation;
    protected string attackAnimation;
    protected string damagedAnimation;
    protected string deathAnimation;
    protected string specialAttackAnimation;
    protected string emoteAnimation;

    public string IdleAnimation { get { return idleAnimation; } protected set { idleAnimation = value; } }
    public string MoveAnimation { get { return moveAnimation; } protected set {  moveAnimation = value; } }
    public string AttackAnimation { get { return attackAnimation; } protected set {  attackAnimation = value; } }
    public string DamagedAnimation { get { return damagedAnimation; } protected set {  damagedAnimation = value; } }
    public string DeathAnimation { get { return deathAnimation; } protected set {  deathAnimation = value; } }
    public string SpecialAttackAnimation { get { return specialAttackAnimation; } protected set { specialAttackAnimation = value; } }
    public string EmoteAnimation { get { return emoteAnimation; } protected set { emoteAnimation = value; } }

    private void Awake() {
        // Get healthbar for this enemy.
        healthBar = transform.Find("HealthBar").GetComponent<HealthBar>();
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }
    public virtual void setAnimations()
    {

    }
    
}
