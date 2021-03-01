using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public FixedJoystick FixedJoystick;
    public GameManager gameManager;
    public Animator animator;
    public float moveSpeed = 5f;
    public Rigidbody character;
    public GameObject vortex;
    public GameObject regularVortex;
    public int lookSpeed;
    public bool shrinkPos;
    Vector3 movement;
    GameObject enemy;
    Vector3 regularSize; 

    void Awake()
    {
        regularSize = new Vector3(0.5f, 0.5f, 0.5f);
        enemy = gameManager._enemy;
    }

    //private void Update()
    //{
    //    this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    this.gameObject.GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
    //}

    void FixedUpdate()
    {
        movement.x = FixedJoystick.Horizontal;
        movement.z = FixedJoystick.Vertical;

        FaceClosestEnemy();

        //character.MovePosition(character.position + movement * moveSpeed * Time.fixedDeltaTime);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        // Play footstep smoke effect if player is moving.
        if(movement.x != 0 || movement.z != 0){
            transform.GetComponent<FootstepSmoke>().PlayFootstepSmokeEffect();
        }
        
        animator.SetFloat("MovementX", Mathf.Abs(movement.x * 10), 0.1f, Time.deltaTime);
        animator.SetFloat("MovementZ", Mathf.Abs(movement.z * 10), 0.1f, Time.deltaTime);

        StrafeWalk();

        if (movement.x != 0 && movement.z != 0)
        {
            
            vortex.transform.localScale += new Vector3(0.1f, 0.8f, 0f) * (gameManager.vortexBuildup / 100);
            shrinkPos = true;
        }

        if ((movement.x == 0 && movement.z == 0) && shrinkPos)
        {
            StartCoroutine(VortexExpand());
            shrinkPos = false;
        }
    }

    void StrafeWalk()
    {
        float controllerDeg = Mathf.Atan2(movement.z, movement.x) * Mathf.Rad2Deg;
        float playerAngle = transform.eulerAngles.y;


        if (controllerDeg != 0)
        {
            animator.SetBool("idle", false);
            //facing north
            if ((playerAngle >= 315 && playerAngle <= 360) || ((playerAngle >= 0.1f) && (playerAngle <= 45)))
            {
                //controller north
                if (controllerDeg >= 45 && controllerDeg <= 135)
                {
                    animator.SetBool("walkForward", true);
                }
                else { animator.SetBool("walkForward", false); }

                //controller south
                if (controllerDeg <= -45 && controllerDeg >= -135)
                {
                    animator.SetBool("walkBackward", true);
                }
                else { animator.SetBool("walkBackward", false); }

                //controller east
                if (controllerDeg <= 45 && controllerDeg >= 0.1f || controllerDeg <= -0.1f && controllerDeg >= -45)
                {
                    animator.SetBool("strafeRight", true);
                }
                else { animator.SetBool("strafeRight", false); }

                //controller west
                if (controllerDeg <= -135 && controllerDeg >= -180 || controllerDeg <= 180 && controllerDeg >= 135)
                {
                    animator.SetBool("strafeLeft", true);
                }
                else { animator.SetBool("strafeLeft", false); }
            }

            //facing east
            if ((playerAngle >= 45 && playerAngle <= 135))
            {
                //controller north
                if (controllerDeg >= 45 && controllerDeg <= 135)
                {
                    animator.SetBool("strafeLeft", true);
                }
                else { animator.SetBool("strafeLeft", false); }

                //controller south
                if (controllerDeg <= -45 && controllerDeg >= -135)
                {
                    animator.SetBool("strafeRight", true);
                }
                else { animator.SetBool("strafeRight", false); }

                //controller east
                if (controllerDeg <= 45 && controllerDeg >= 0.1f || controllerDeg <= -0.1f && controllerDeg >= -45)
                {
                    animator.SetBool("walkForward", true);
                }
                else { animator.SetBool("walkForward", false); }

                //controller west
                if (controllerDeg <= -135 && controllerDeg >= -180 || controllerDeg <= 180 && controllerDeg >= 135)
                {
                    animator.SetBool("walkBackward", true);
                }
                else { animator.SetBool("walkBackward", false); }
            }

            //facing south
            if ((playerAngle >= 135 && playerAngle <= 225))
            {
                //controller north
                if (controllerDeg >= 45 && controllerDeg <= 135)
                {
                    animator.SetBool("walkBackward", true);
                }
                else { animator.SetBool("walkBackward", false); }

                //controller south
                if (controllerDeg <= -45 && controllerDeg >= -135)
                {
                    animator.SetBool("walkForward", true);
                }
                else { animator.SetBool("walkForward", false); }

                //controller east
                if (controllerDeg <= 45 && controllerDeg >= 0.1f || controllerDeg <= -0.1f && controllerDeg >= -45)
                {
                    animator.SetBool("strafeLeft", true);
                }
                else { animator.SetBool("strafeLeft", false); }

                //controller west
                if (controllerDeg <= -135 && controllerDeg >= -180 || controllerDeg <= 180 && controllerDeg >= 135)
                {
                    animator.SetBool("strafeRight", true);
                }
                else { animator.SetBool("strafeRight", false); }
            }

            //facing west
            if ((playerAngle >= 225 && playerAngle <= 315))
            {
                //controller north
                if (controllerDeg >= 45 && controllerDeg <= 135)
                {
                    animator.SetBool("strafeRight", true);
                }
                else { animator.SetBool("strafeRight", false); }

                //controller south
                if (controllerDeg <= -45 && controllerDeg >= -135)
                {
                    animator.SetBool("strafeLeft", true);
                }
                else { animator.SetBool("strafeLeft", false); }

                //controller east
                if (controllerDeg <= 45 && controllerDeg >= 0.1f || controllerDeg <= -0.1f && controllerDeg >= -45)
                {
                    animator.SetBool("walkBackward", true);
                }
                else { animator.SetBool("walkBackward", false); }

                //controller west
                if (controllerDeg <= -135 && controllerDeg >= -180 || controllerDeg <= 180 && controllerDeg >= 135)
                {
                    animator.SetBool("walkForward", true);
                }
                else { animator.SetBool("walkForward", false); }
            }
        }
        else { animator.SetBool("idle", true);
            animator.SetBool("strafeRight", false);
            animator.SetBool("strafeLeft", false);
            animator.SetBool("walkForward", false);
            animator.SetBool("walkBackward", false);
        }
    }

    void FaceClosestEnemy()
    {
        float closestEnemy = Mathf.Infinity;
        GameObject enemy = null;
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject currentEnemy in allEnemies)
        {
            var targetRotation = currentEnemy.transform.position - transform.position;
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < closestEnemy)
            {
                closestEnemy = distanceToEnemy;
                enemy = currentEnemy;
            }

        }

        if (enemy != null)
        {
            StartCoroutine(DoRotationAtTargetDirection(enemy.transform));
        }
    }

    IEnumerator DoRotationAtTargetDirection(Transform opponentPlayer)
    {
        Quaternion targetRotation = Quaternion.identity;
        do
        {
            Vector3 targetDirection = (new Vector3(opponentPlayer.position.x, 0, opponentPlayer.position.z) - new Vector3(transform.position.x, 0, transform.position.z)).normalized;
            targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * (moveSpeed * lookSpeed));
            yield return null;

        } while (Quaternion.Angle(transform.rotation, targetRotation) < 0.01f && gameObject != null);
    }

    IEnumerator VortexExpand()
    {

        float i = 0.0f;
        float j = 0.0f;
        float rate = 1f;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            regularVortex.transform.localScale = Vector3.Lerp(regularSize, new Vector3(vortex.transform.localScale.x * 2f, 
                vortex.transform.localScale.y, vortex.transform.localScale.z / 1.5f), i);
            yield return null;
        }

        yield return new WaitForSeconds(gameManager.vortexHangTime);

        while (j < 1.0f)
        {
            j += Time.deltaTime * rate;
            regularVortex.transform.localScale = Vector3.Lerp(new Vector3(vortex.transform.localScale.x * 2f, vortex.transform.localScale.y * 1.2f, 
                vortex.transform.localScale.z / 1.5f), regularSize, j);
            vortex.transform.localScale = Vector3.Lerp(vortex.transform.localScale, new Vector3(0.3f, 0.7f, 1), j);
            yield return null;
        }
    }

    public IEnumerator OnDeath()
    {
        vortex.SetActive(false);
        regularVortex.SetActive(false);
        movement = new Vector3(0, 0, 0);
        gameObject.GetComponentInChildren<Animator>().enabled = false;
        yield return null;
    }
}

