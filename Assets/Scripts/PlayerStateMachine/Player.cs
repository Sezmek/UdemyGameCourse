using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsBusy {  get; private set; }
    
    [Header("Move Info")]
    public float movespeed;
    public float jumpforce;

    [Header("Dash Info")]
    public float dashSpeed;
    public float dashDuration;
    [SerializeField] private float DashCooldown;
    private float dashCooldownTime;
    public float DashDir { get; private set; }

    [Header("Collison Info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallChcekDistance;
    [SerializeField] private LayerMask WhatIsGround;

    public int Dir { get; private set; } = 1;
    private bool facingRight = true;
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    #endregion
    
    #region States
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }

    public PlayerMoveState moveState { get; private set; }

    public PlayerJumpState jumpState { get; private set; }

    public PlayerAirState airState { get; private set; }

    public PlayerDashState dashState { get; private set; }

    public PlayerWallslideState wallslideState { get; private set; }

    public PlayerWallJumpState walljumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }



    #endregion

    public void Awake()
    {
        StateMachine = new PlayerStateMachine();

        airState = new PlayerAirState(StateMachine, this, "Jump");
        jumpState = new PlayerJumpState(StateMachine, this, "Jump");
        idleState = new PlayerIdleState(StateMachine,this, "Idle");
        moveState = new PlayerMoveState(StateMachine,this, "Move");
        dashState = new PlayerDashState(StateMachine,this, "Dash");
        wallslideState = new PlayerWallslideState(StateMachine, this, "WallSlide");
        walljumpState = new PlayerWallJumpState(StateMachine, this, "Jump");
        primaryAttackState = new PlayerPrimaryAttackState(StateMachine, this, "Attack");
    }

    public void Start()
    {
        anim = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();

        StateMachine.Initialize(idleState);
    }


    private void Update()
    {
        StateMachine.currentState.Update();
        CheckForDashInput();
    }
    public IEnumerator BusyFor(float _seconds)
    {
        IsBusy = true;
        yield return new WaitForSeconds(_seconds);
        IsBusy = false;
    }
    public void AnimationTrigger() => StateMachine.currentState.AnimationFinish();
    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;

        dashCooldownTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTime < 0)
        {
            dashCooldownTime = DashCooldown;
            DashDir = Input.GetAxisRaw("Horizontal");

            if (DashDir == 0)
                DashDir = Dir;
            StateMachine.ChangeState(dashState);
        }
    }
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        Rb.velocity = new Vector2 (xVelocity, yVelocity);
        FlipControler(xVelocity);
    }

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, WhatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * Dir, wallChcekDistance, WhatIsGround);
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallChcekDistance, wallCheck.position.y)); 
    }

    public void Flip()
    {
        Dir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    public void FlipControler(float _x)
    {
        if (_x > 0 && !facingRight) Flip();
        else if (_x < 0 && facingRight) Flip();
    }
}
