using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    PlayerController player;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player= transform.parent.GetComponentInParent<PlayerController>();
        animator = transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            player.isGrounded = true;
            animator.SetBool("jump", false);
            animator.SetBool("fall", false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            player.isGrounded = false;
        }
    }
}
