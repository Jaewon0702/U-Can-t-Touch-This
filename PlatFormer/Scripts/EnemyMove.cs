using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    public int nextMove;

    // Start is called before the first frame update
    void Awake(){
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move
       rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

       //Platform Check
       Vector2 FrontVec = new Vector2(rigid.position.x + nextMove * 0.6f, rigid.position.y);
       Debug.DrawRay(FrontVec, Vector3.down, new Color(0, 1, 0));

       RaycastHit2D rayHit = Physics2D.Raycast(FrontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
       if(rayHit.collider == null){
        Turn();
        }

    }

    //Monster AI
    void Think(){
        //Set Next Active
        nextMove = Random.Range(-1, 2);
        
        //Sprite Animation
        anim.SetInteger("WalkSpeed", nextMove);

        //Flip Sprite
        if(nextMove != 0){
            spriteRenderer.flipX = nextMove == 1;
        }

        //Recursive
        float nextMoveTime = Random.Range(2f, 5f);
        Invoke("Think", nextMoveTime);

    }

    void Turn(){
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;
        CancelInvoke();
        Invoke("Think", 5);

    }
    
}
