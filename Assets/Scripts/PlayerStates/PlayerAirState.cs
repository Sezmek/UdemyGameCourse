using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallslideState);
        if (xInput != 0)
            player.SetVelocity(player.movespeed * .8f * xInput, rb.velocity.y);
        else 
            player.SetVelocity(0,rb.velocity.y);
    }
}
