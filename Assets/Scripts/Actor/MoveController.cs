﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : Movement, InputEventInterface
{
    private Vector2 lastPostion;

    public InputControls inputs;

    Vector2? moveVect = Vector2.zero;

    private void Awake()
    {
        inputs = new InputControls();
        inputs.Player.Move.performed += ctx => moveVect = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled += ctx => moveVect = Vector2.zero;

        inputs.Player.A.performed += ctx =>
        {
            var hit = Physics2D.Raycast(gameObject.transform.position, GetComponent<Movement>().lookDirection, 1, 1 << 10 | 1 << 8);

            if (hit.collider == null)
            {
                return;
            }
            var sceneEventGraphs = hit.collider.gameObject.GetComponents<BaseSceneGraph>();
            foreach (var eventGraph in sceneEventGraphs)
            {
                eventGraph.graph.trigger(TriggerType.KeyTrigger);
            }

            var eventActions = hit.collider.gameObject.GetComponents<EventAction>();
            foreach (var action in eventActions)
            {
                if (action.startCondition == StartConditions.KeyTrigger)
                {
                    action.execute();
                    break;
                }
            }
        };

        inputs.Player.X.performed += ctx => MMX.GameManager.MainMenu.show();

        inputs.Player.Y.performed += ctx =>
        {
            StationMenuController stationMenu = StationMenuController.Create();
            stationMenu.show();
        };
    }
    override protected void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        if (moveVect == null || anim == null)
        {
            return;
        }
        else if (moveVect == Vector2.zero)
        {
            moveVect = null;
            base.move(Vector2.zero, Vector2.zero);
        }
        else
        {
            move();
        }
    }
    public void willLostFocus()
    {
        base.move(Vector2.zero, Vector2.zero);
        inputs.Disable();
    }
    public void willOnFocus()
    {
        inputs.Enable();
    }
    public void move()
    {

        Vector2 move = moveVect ?? Vector2.zero;
        float moveX = move.x;
        float moveY = move.y;

        Vector2 position = transform.position;
        Vector2 lookDirection = new Vector2();
        var currentSpeed = speed;

        if (moveX != 0 && moveY != 0)
        {
            //调整正方形对角线移动比率
            // currentSpeed /= 1.414f;

            //确定斜着走时面向的方向
            var x = moveX;
            var y = moveY;
            if (y > 0)
            {
                //斜着往上走时，朝向以左右为准
                if (x != 0)
                {
                    y = 0;
                }
            }
            else
            {
                //斜着往下走时，朝向以下为准
                x = 0;
            }
            lookDirection = new Vector2(x, y);
        }
        else
        {
            lookDirection = new Vector2(moveX, moveY);
        }

        position.x += moveX * currentSpeed * Time.deltaTime * 1.5f;
        position.y += moveY * currentSpeed * Time.deltaTime * 1.5f;

        //走不动了，就停止行走
        if (Vector2.Distance(position, lastPostion) <= 0.03)
        {
            base.move(Vector2.zero, Vector2.zero);
            return;
        }
        lastPostion = position;
        base.move(position, lookDirection);
    }
    public void inputAction()
    {


    }
}
