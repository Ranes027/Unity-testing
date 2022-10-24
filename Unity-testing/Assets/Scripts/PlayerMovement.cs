using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _speed = 12f;
    [SerializeField] private float _jumpHeight = 1f;
    [SerializeField] private float _gravity = -9.81f;
    [Space]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    private void Update()
    {
        CheckGround();
        Movement();
    }

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        Jump();
    }

    private void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 motion = transform.right * horizontalMove + transform.forward * verticalMove;
        _controller.Move(motion * _speed * Time.deltaTime);

        Gravity();
    }

    private void Gravity()
    {
        velocity.y += _gravity * Time.deltaTime;
        _controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
    }
}
