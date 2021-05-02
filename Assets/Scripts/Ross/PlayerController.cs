using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 2;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem dustF;
    [SerializeField] private ParticleSystem dustB;


    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
            animator.SetBool("Forward_R", false);
            animator.SetBool("Backward_L", true);
            //UPDATE ANIMATION
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
            animator.SetBool("Forward_R", true);
            animator.SetBool("Backward_L", false);
            //UPDATE ANIMATION
        }
        else
        {
            animator.SetBool("Forward_R", false);
            animator.SetBool("Backward_L", false);
        }
    }

    public void PlayDustFEvent()
    {
        dustF.Play();
    }

        public void PlayDustBEvent()
    {
        dustB.Play();
    }
}
