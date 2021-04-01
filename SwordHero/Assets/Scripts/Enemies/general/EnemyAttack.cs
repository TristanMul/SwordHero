using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBaseClass))]
public class EnemyAttack : MonoBehaviour
{
    protected EnemyBaseClass controllerClass;  // The controller class of this enemy object.
    protected GameObject player;
}
