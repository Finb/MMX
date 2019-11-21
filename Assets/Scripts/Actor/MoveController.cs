using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : Movement, InputEventInterface
{

    TeleportController teleportController;

    private Vector2 lastPostion;

    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void willLostFocus()
    {
        base.move(Vector2.zero,Vector2.zero);
    }
    public void willOnFocus(){

    }
    public void inputAction()
    {
        if (MMX.GameManager.Input.currentTarget.name != "角色"){
            return;
        }
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");


        Vector2 position = transform.position;
        Vector2 lookDirection = new Vector2();
        var currentSpeed = speed;

        if (moveX != 0 && moveY != 0)
        {
            //调整正方形对角线移动比率
            currentSpeed /= 1.414f;

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

        position.x += moveX * currentSpeed * 0.03f;
        position.y += moveY * currentSpeed * 0.03f;

        //走不动了，就停止行走
        if (Vector2.Distance(position, lastPostion) <= 0.03)
        {
            base.move(Vector2.zero, Vector2.zero);
            return;
        }
        lastPostion = position;
        base.move(position, lookDirection);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "OffTank")
        {
            //从坦克上下来
            Dialog.shared.showText("从坦克上下来了!", "事件提醒");
        }
    }
}
