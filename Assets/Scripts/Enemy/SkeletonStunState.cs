using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunState : EnemyState
{
    SkeletonEnemy enemy;
    public SkeletonStunState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName, SkeletonEnemy enemy) : base(_enemy, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.stunDuration;
        enemy.fx.InvokeRepeating("RedColorBlink",0, 0.2f);
        rb.velocity = new Vector2(-enemy.Dir * enemy.stunDirection.x, enemy.stunDirection.y);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.fx.Invoke("CancelRedBlink",0);
    }


    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.idleState);
    }
}
