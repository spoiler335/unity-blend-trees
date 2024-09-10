using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    private Camera playerCamera;
    private float movementForce = 1f;
    private float jumpForce = 1f;
    public float maxSpeed { get; private set; } = 5f;
    private Rigidbody rb;
    private Vector3 forceDirection = Vector3.zero;

    private InputManager input => DI.di.inputManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Start()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        if (input.isJumpClicked) PerformJump();
        if (input.isAttackClicked) PerformAttack();
    }

    public void PerformAttack()
    {
        GetComponent<Animator>().SetTrigger("Attack");
    }

    private void FixedUpdate()
    {
        AddForceBasedOnInput();
        IncreaseDownwardVelocityWhenFallinDown();
        ResetVelocityIfNeeded();
        LookAt();
    }

    private void LookAt()
    {
        Vector3 lookDirection = rb.velocity;
        lookDirection.y = 0;
        if (input.GetMoveAxis().sqrMagnitude > 0.1f && lookDirection.sqrMagnitude > 0.1f)
            rb.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        else
            rb.angularVelocity = Vector3.zero;
    }

    private void IncreaseDownwardVelocityWhenFallinDown()
    {
        if (rb.velocity.y < 0) rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
    }

    private void ResetVelocityIfNeeded()
    {
        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
    }

    private void AddForceBasedOnInput()
    {
        forceDirection += input.GetForward() * GetCameraForward() * movementForce;
        forceDirection += input.GetRight() * GetCameraRight() * movementForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;
    }

    private Vector3 GetCameraForward()
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight()
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void PerformJump()
    {
        Debug.Log("Jump Check");
        if (IsGrounded())
        {
            Debug.Log("Jumping");
            forceDirection += Vector3.up * jumpForce;
        }
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.3f)) return true;
        return false;
    }

}
