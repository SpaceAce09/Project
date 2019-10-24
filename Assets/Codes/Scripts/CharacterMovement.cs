﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 6.0f;
    public float rotationSpeed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float jumpUpTime = 0.5f;
    public float gravity = 10.0f;

    [HideInInspector]
    public Vector2 motion;
    public bool isjumping = false;

    CharacterController cc;

    Vector3 targetDirection = new Vector3(0, 0, 0);

    public void Init()
    {
        cc = GetComponent<CharacterController>();
    }

    public void updateMontion()
    {
        updateRotation();

        float tmpSpeed = Mathf.Abs(motion.x) + Mathf.Abs(motion.y);
        tmpSpeed = Mathf.Clamp(tmpSpeed, 0, 1f);
        Vector3 moveVec = transform.forward * tmpSpeed * moveSpeed;

        if (isjumping)
        {
            jumpCountDown -= Time.deltaTime;
            if (jumpCountDown <= 0.0f)
                isjumping = false;
            moveVec.y = jumpSpeed;
        }
        
        moveVec.y -= gravity * Time.deltaTime;

        cc.Move(moveVec);
        
    }

    void updateRotation()
    {
        //Debug.Log(targetDirection);
        if (targetDirection.magnitude <= 0)
            return;
        Vector3 lookDirection = targetDirection.normalized;
        Quaternion rotationAngle = Quaternion.LookRotation(lookDirection, transform.up);

        float diffRotation = rotationAngle.eulerAngles.y - transform.eulerAngles.y;
        float eulerY = Mathf.Abs(diffRotation) > 0 ? rotationAngle.eulerAngles.y : transform.eulerAngles.y;
        Vector3 euler = new Vector3(transform.eulerAngles.x, eulerY, transform.eulerAngles.z);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(euler), rotationSpeed * Time.deltaTime);
    }

    public void updateAnimation()
    {

    }

    private float jumpCountDown = 0.0f;
    public void Jump()
    {
        if (!cc.isGrounded)
            return;
        isjumping = true;
        jumpCountDown = jumpUpTime;
    }

    public void updateTargetDirection(Transform referenceTrans)
    {
        Vector3 forward = referenceTrans.TransformDirection(Vector3.forward);
        forward.y = 0;

        Vector3 right = referenceTrans.TransformDirection(Vector3.right);

        targetDirection = motion.x * right + motion.y * forward;
    }

}
