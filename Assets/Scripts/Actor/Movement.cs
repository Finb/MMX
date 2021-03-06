﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MoveStep
{
    public Vector2 position;
    public Vector2 direction;
    public MoveStep(Vector2 position, Vector2 direction)
    {
        this.position = position;
        this.direction = direction;
    }
}

public class Movement : MonoBehaviour
{
    protected float speed = 5f;
    protected Rigidbody2D rbody;
    public Animator anim;
    /// 跟随者
    private Movement _followerMovement;
    public Movement followerMovement
    {
        get
        {
            return _followerMovement;
        }
        set
        {
            _followerMovement = value;
            //取消跟随者的碰撞
            _followerMovement.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
    public Queue<MoveStep> moveSteps = new Queue<MoveStep>();
    public Vector2 lookDirection = Vector2.down;

    virtual protected void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }
    public void move(Vector2 position, Vector2 direction)
    {
        if (Mathf.Abs(direction.x) + Mathf.Abs(direction.y) > 0)
        {
            toward(direction);
            lookDirection = direction;
            anim.enabled = true;
            rbody.MovePosition(position);

        }
        else
        {
            //没有移动
            anim.enabled = false;
        }

        if (followerMovement != null)
        {
            if (direction == Vector2.zero)
            {
                followerMovement.move(Vector2.zero, Vector2.zero);
                return;
            }
            if (moveSteps.Count > 5)
            {
                var step = moveSteps.Dequeue(); ;
                followerMovement.move(step.position, step.direction);
            }
            moveSteps.Enqueue(new MoveStep(position, direction));
        }

    }

    public void toward(Vector2 direction)
    {
        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
    }
}
