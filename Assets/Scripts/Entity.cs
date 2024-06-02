using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Collison Info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallChcekDistance;
    [SerializeField] protected LayerMask WhatIsGround;
    public int Dir { get; private set; } = 1;
    protected bool facingRight = true;

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    #endregion

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Update()
    {

    }

    public virtual void Damage()
    {

    }
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, WhatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * Dir, wallChcekDistance, WhatIsGround);
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
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
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        Rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipControler(xVelocity);
    }
}
