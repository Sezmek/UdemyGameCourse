using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkillControler : MonoBehaviour
{
    [SerializeField] private float returnSpeed = 12f;
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;

    private bool canRotate = true;
    private bool IsReturning;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }
    
    public void SetupSword(Vector2 _dir, float _gravityScale, Player _player)
    {
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
        player = _player;

        anim.SetBool("Rotation", true);
    }
    public void ReturnSword()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll; 
        transform.parent = null;
        IsReturning = true;
    }
    private void Update()
    {
        if (canRotate)
            transform.right = rb.velocity;

        if (IsReturning)
        {
            anim.SetBool("Rotation", true);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);

            if(Vector2.Distance(transform.position, player.transform.position) < 1)
                player.ClearTheSword();
        }
    }
    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (IsReturning)
            return;

        anim.SetBool("Rotation", false);
        canRotate = false;
        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        transform.parent = collison.transform;
    }
}
