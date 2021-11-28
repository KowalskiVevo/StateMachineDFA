using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characteristics : MonoBehaviour
{
    public int HP = 10;
    public GameObject parent;
    private enum State { RUN, ATTACK, HIT, DEATH, NOTHING, IDLE };
    private State state;

    private Animator animator;

    private void Start() {
        state = State.IDLE;
        animator = parent.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "AttackBox" || other.gameObject.name == "AttackBox(Clone)") {
            HP--;
            SetInvincible();
            state = State.HIT;
        }
    }

    private void Update() {
        switch(state){
            case State.HIT:
                if (HP <= 0){
                    state = State.DEATH;
                }
                break;
        }
        SetAnimation();
    }

    private void SetAnimation(){
        switch(state){
            case State.IDLE:
                animator.Play("Idle");
                break;
            case State.HIT:
                animator.Play("HitEnemy");
                break;
            case State.DEATH:
                animator.Play("DeathEnemy");
                break;
        }
    }

    private void SetStateAfterHit(){
        if (state == State.HIT){
            state = State.IDLE;
        }
    }

    private void SetInvincible(){
        gameObject.GetComponent<BoxCollider2D>().enabled = !gameObject.GetComponent<BoxCollider2D>().enabled;
    }
}