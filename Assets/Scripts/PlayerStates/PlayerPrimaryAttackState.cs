using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCunter;


    public PlayerPrimaryAttackState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (comboCunter > 1) comboCunter = 0;
        player.anim.SetInteger("ComboCunter", comboCunter);

        float attackDir = player.Dir;
        if (xInput != 0)
        {
            attackDir = xInput;
        }

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();

        comboCunter++;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            player.SetVelocity(0, 0);
        if (xInput != 0)
        {
            player.FlipControler(xInput);
        }
        if (trigger)
        {
            stateMachine.ChangeState(player.idleState);
            comboCunter = 0;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
    }
}
