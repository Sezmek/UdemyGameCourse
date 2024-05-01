using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCunter;

    private float LastTimeAttacked;
    private float comboWindow = 1;
    public PlayerPrimaryAttackState(PlayerStateMachine _stateMachine, Player _player, string _animBoolName) : base(_stateMachine, _player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (comboCunter > 2 || Time.time - LastTimeAttacked > comboWindow) comboCunter = 0;
        player.anim.SetInteger("ComboCunter", comboCunter);

        stateTimer = 0.1f;
    }

    public override void Exit()
    {
        base.Exit();

        comboCunter++;
        LastTimeAttacked = Time.time;
        player.StartCoroutine("BusyFor", .1f);
    }

    public override void Update()
    {
        base.Update();
       
        if (stateTimer < 0)
        {
            player.SetVelocity(0, 0);
        }
        if (xInput != 0)
        {
            player.FlipControler(xInput);
        }
        if (trigger)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
