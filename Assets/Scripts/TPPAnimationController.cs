using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPPAnimationController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    private float maxSpeed = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        maxSpeed = GetComponent<PlayerController>().maxSpeed;
    }


    private void Update()
    {
        animator.SetFloat("Speed", rb.velocity.magnitude / maxSpeed);
    }
}
