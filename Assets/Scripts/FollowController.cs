using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowController : Movement
{
    // Start is called before the first frame update
    public GameObject followedBody;
    Rigidbody2D fBody;
    MoveController moveController;

    private Vector2 lastPostion;
    override protected void Start()
    {
        base.Start();
        
        if (followedBody == null) {
            return;
        }
        
        // fBody = followedBody.GetComponent<Rigidbody2D>();
        // Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(),followedBody.GetComponent<BoxCollider2D>());
        // moveController = followedBody.GetComponent<MoveController>();
    }

    // Update is called once per frame
    // void Update()
    // {
        // if (followedBody == null) {
        //     return;
        // }

        // if (moveController.isMoving && moveController.moveSteps.Count >= 10) {
        //     var a = moveController.moveSteps.ToArray()[1];
        //     move(a.position, a.direction);
        // }
        // else{
        //     move(rbody.transform.position, Vector2.zero);
        // }
        
    // }
}
