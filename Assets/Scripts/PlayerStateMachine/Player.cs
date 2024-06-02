using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
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

    protected override void Awake()
    {
        base.Awake();
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

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(idleState);
    }


    protected override void Update()
    {
        base.Update();
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
}
