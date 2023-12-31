using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float playerRunSpeed = 5f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float slideVelocity = 3;
    [SerializeField] private float slopeForceDown = -10;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Animator animator;

    [SerializeField] public CharacterController player;

    private Vector3 playerInput;
    private Vector3 movePlayer;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 hitNormal;


    private float horizontalMove;
    private float verticalMove;
    private float fallVelocity;
    private float walkVelocity;
    private float originalPlayerSpeed;


    private bool isOnSlope = false;
    public bool canMove { set; get; }
    public bool isDead;
    public bool collecting;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        originalPlayerSpeed = playerSpeed;
        canMove = true;
        isDead = false;
    }
    
    void Update()
    {
        if (isDead) return;
        if (canMove)
        {
            horizontalMove = Input.GetAxis("Horizontal");
            verticalMove = Input.GetAxis("Vertical");

            playerInput = new Vector3(horizontalMove, 0, verticalMove);
            playerInput = Vector3.ClampMagnitude(playerInput, 1);
            walkVelocity = playerInput.magnitude * playerSpeed;
            animator.SetFloat("PlayerWalkVelocity", walkVelocity);

            if (walkVelocity > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Debug.Log("run");
                    audioManager.PlaySFX(audioManager.run);
                    playerSpeed = playerRunSpeed;
                    animator.SetBool("isRunning", true);
                }
                else
                {
                    Debug.Log("walk1");
                    audioManager.PlaySFX(audioManager.walk);
                }
            }
            else
            {
                Debug.Log("walk2");
                playerSpeed = originalPlayerSpeed;
                animator.SetBool("isRunning", false);
            }

            CamDirection();
            movePlayer = playerInput.x * camRight + playerInput.z * camForward;
            movePlayer = movePlayer * playerSpeed;
            player.transform.LookAt(player.transform.position + movePlayer);

            SetGravity();

            PlayerSkills();

            player.Move(movePlayer * Time.deltaTime);

        }
        
    }

    private void CamDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    private void SetGravity()
    {
        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
            animator.SetFloat("PlayerVerticalVelocity", movePlayer.y);
        }
        animator.SetBool("isGrounded", player.isGrounded);
        SlideDown();
    }

    private void PlayerSkills()
    {
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            audioManager.PlaySFX(audioManager.jump);
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
            animator.SetTrigger("PlayerJump");
        }
    }

    private void SlideDown()
    {
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;
        if (isOnSlope)
        {
            movePlayer.x += ((1f - hitNormal.y) * hitNormal.x) * slideVelocity;
            movePlayer.z += ((1f - hitNormal.y) * hitNormal.z) * slideVelocity;
            movePlayer.y += slopeForceDown;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }
    private void OnAnimatorMove(){}
}
