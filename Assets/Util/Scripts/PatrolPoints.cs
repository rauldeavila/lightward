using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoints : MonoBehaviour {

    [SerializeField] private float moveSpeed = 1.2f;
    [SerializeField] private float swooshSpeed = 2f;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] public int index;
    [SerializeField] private float rotSpeed;
    private bool isTurn;
    public bool facingRight = true;
    private Animator animator;
    public Animator scytheAnimator;
    private TakeDamage healthScript;

    void Awake(){
        animator = GetComponent<Animator>();
        healthScript = GetComponent<TakeDamage>();
    }

    public void Patrol(){
        if(healthScript.health <= 40){
            SpinScythe();
        }
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[index].position, moveSpeed * Time.deltaTime);
        if (isTurn){
            facingRight = !facingRight;
            isTurn = false;
            transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
            if(healthScript.health > 40){
                SwooshRoller();
            } else if(healthScript.health > 20){
                SwooshMagicRoller();
            }
        }
        if (Vector2.Distance(transform.position, wayPoints[index].position) <= 0.05f) {
            index++;
            isTurn = true;
            if (index > wayPoints.Length - 1) {
                index = 0;
            }
        }
    }


    public void Swoosh(){
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[index].position, swooshSpeed * Time.deltaTime);
        if (isTurn){
            facingRight = !facingRight;
            isTurn = false;
            transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
            SwooshRoller();
        }
        if (Vector2.Distance(transform.position, wayPoints[index].position) <= 0.05f) {
            index++;
            isTurn = true;
            if (index > wayPoints.Length - 1) {
                index = 0;
            }
        }
    }

    private void SwooshRoller(){
        int randomChance = Random.Range(0, 4);
        if(randomChance == 0 || randomChance == 1){
            scytheAnimator.SetBool("swoosh", true);
            animator.SetBool("patrol", false);
        }
    }

    private void SpinScythe(){
        scytheAnimator.SetBool("spinning", true);
    }

    private void SwooshMagicRoller(){
        int randomChance = Random.Range(0, 4);
        if(randomChance == 0 || randomChance == 1){
            scytheAnimator.SetBool("swoosh", true);
            animator.SetBool("patrol", false);
        } else{
            scytheAnimator.SetBool("spell", true);
            animator.SetBool("patrol", false);
        }
    }


    public void SetSwooshToTrueOnMainAnimator(){
        animator.SetBool("swoosh", true);
    }

}
