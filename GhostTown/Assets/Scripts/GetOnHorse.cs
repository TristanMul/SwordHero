using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOnHorse : MonoBehaviour
{
    public GameObject vortex;
    public GameObject vortexPreview;
    private bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        // Player collided with horse.
        if(other.collider.tag == "Animal"){
            TurnOffVortex();
            TurnOffMovement();
            MovePlayerToHorse(other.gameObject);
        }
    }

    private void MovePlayerToHorse(GameObject horse){
        // Position above horse.
        Transform horseMountPoint = horse.transform.Find("MountPoint").transform;
        Vector3 newPos = new Vector3(horseMountPoint.position.x, transform.position.y, horseMountPoint.position.z);

        Jump();

        // Deactivate collision.
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

        // Move player on top of horse and give player same rotation as horse.
        // transform.position = Vector3.Lerp(transform.position, newPos, 1f);
        // transform.rotation = Quaternion.Lerp(transform.rotation, horse.transform.rotation, 1f);

        // Make player child of horse gameobject.
        transform.SetParent(horse.transform);
    }

    private void Jump(){
        if(canJump){
            canJump = false;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.up * 10f;
        }
        
    }

    private void TurnOffMovement(){
        // Turn of animations.
        gameObject.GetComponent<PlayerMovement>().animator.SetBool("idle", true);
        gameObject.GetComponent<PlayerMovement>().animator.SetBool("strafeRight", false);
        gameObject.GetComponent<PlayerMovement>().animator.SetBool("strafeLeft", false);
        gameObject.GetComponent<PlayerMovement>().animator.SetBool("walkForward", false);
        gameObject.GetComponent<PlayerMovement>().animator.SetBool("walkBackward", false);

        // Turn of movement logic.
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<FootstepSmoke>().enabled = false;
    }

    private void TurnOffVortex(){
        // Turn off vortex and it's preview.
        vortex.gameObject.SetActive(false);
        vortexPreview.gameObject.SetActive(false);
    }

}
