using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerStateMachine _stateMachine, Player _palyer, string _animBoolName) : base(_stateMachine, _palyer, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.skill.clone.CreateClone(player.transform);

        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        player.SetVelocity(0, rb.velocity.y);
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallslideState);
        player.SetVelocity(player.dashSpeed * player.DashDir,0);
        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);
    }
}
