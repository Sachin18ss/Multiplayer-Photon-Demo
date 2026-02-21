using JetBrains.Annotations;
using Photon.Pun;
using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPun
{
    [SerializeField] private float _walkSpeed = 2f;
    [SerializeField] private float _runSpeed = 5f;

    public GameObject playerCamera;
    public float rotationSpeed = 720f;

    CharacterController controller;
    private Animator _animator;

    private float yVelocity;
    public float gravity = -9.8f;

    private bool _isRunning = false;
    private bool _isWalking = false;



    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (!photonView.IsMine)
        {
            playerCamera.SetActive(false);
            return;
        }
    }

    void Update()
    {
        if (!photonView.IsMine) return;
        

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Mathf.Abs(z) > 0.01f)
        {
            _isWalking = true;
        }
        else
        {
            _isWalking = false;
        }


        if (Input.GetKey(KeyCode.LeftShift) && _isWalking)
        {
            _isRunning = true;
        }
        {
            _isRunning = false;
            
        }

        if (Mathf.Abs(x) > 0.01f)
        {
            transform.Rotate(Vector3.up, x * rotationSpeed * Time.deltaTime);
        }

        Vector3 move = transform.forward * z;

        

        if (controller.isGrounded)
        {
            if (yVelocity < 0)
                yVelocity = -2f; // keeps player grounded
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
        }
        move.y = yVelocity;

        _animator.SetBool("Walk", _isWalking);
        _animator.SetBool("Run", _isRunning);

        MovePlayer(move);
        
    }

    public void MovePlayer(Vector3 move)
    {
        float speed = _isRunning ? _runSpeed : _walkSpeed;

        controller.Move(move.normalized * speed * Time.deltaTime);
    }

}
