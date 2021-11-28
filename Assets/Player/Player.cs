using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;
    public SpriteRenderer spriteRenderer;
    private enum State { IDLE, RUN, ATTACK };
    private State stateNow = State.IDLE;

    public Animator animator;
    private bool isAttackNow = false;
    public GameObject AttackBoxPrefab;
    private float offsetValue = 0.588f;

    void Update(){
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        switch (stateNow)
        {
            case State.IDLE:
                //Если игрок движется вправо, то не отзеркаливаем персонажа и state = RUN
                if (movement.x > 0)
                {
                    spriteRenderer.flipX = false;
                    stateNow = State.RUN;
                }
                //Если игрок движется влево, то отзеркалим персонажа и state = RUN
                else if (movement.x < 0)
                {
                    spriteRenderer.flipX = true;
                    stateNow = State.RUN;
                }
                //Если игрок движется вверх или вниз, то state = RUN
                else if (movement.y != 0){
                    stateNow = State.RUN;
                }
                if (Input.GetButtonDown("Fire1")){
                    stateNow = State.ATTACK;
                }
                break;
            case State.RUN:
                //Если игрок не движется, то state = IDLE
                if (movement.x == 0 && movement.y == 0)
                {
                    stateNow = State.IDLE;
                }
                //Если игрок движется влево, то отзеркалим персонажа
                else if (movement.x < 0)
                {
                    spriteRenderer.flipX = true;
                    stateNow = State.RUN;
                }
                //Если игрок движется вправо, то не отзеркаливаем персонажа
                else if (movement.x > 0)
                {
                    spriteRenderer.flipX = false;
                    stateNow = State.RUN;
                }
                if (Input.GetButtonDown("Fire1"))
                {
                    stateNow = State.ATTACK;
                }
                rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
                break;//
            case State.ATTACK:
                if (isAttackNow == false){
                    stateNow = State.IDLE;
                }
                rb.MovePosition(rb.position + movement * (moveSpeed * 1/2) * Time.fixedDeltaTime);
                break;//

        }
        setAnimation();
    }

    private void setAnimation(){
        switch(stateNow){
            case State.IDLE:
                animator.Play("PIdle");
                break;
            case State.RUN:
                animator.Play("PRMove");
                break;
            case State.ATTACK:
                animator.Play("PAttack");
                break;
        }
    }

    private bool isAttack(){
        return isAttackNow = !isAttackNow;
    }

    private void createAttackBox(){
        Vector3 offset = new Vector3();
        if (spriteRenderer.flipX == false){
            offset = new Vector3(transform.position.x + offsetValue, transform.position.y, transform.position.z);
        }
        else {
            offset = new Vector3(transform.position.x - offsetValue, transform.position.y, transform.position.z);
        }
        GameObject box = Instantiate(AttackBoxPrefab, offset, transform.rotation);
        box.transform.SetParent(this.transform);
        
    }
}