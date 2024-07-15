using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkers : MonoBehaviour {


    public bool isGrounded = false;
    public bool isWalled = false;
    public bool isRoofed = false;
    public bool wizFront = false;
    public bool wizBack = false;
    public GameObject groundCheck;
    public float groundCircleRadius;
    public GameObject wallCheck;
    public float wallCircleRadius;
    public GameObject roofCheck;
    public float roofCircleRadius;
    public LayerMask groundLayer;
    public LayerMask wizLayer;
    public GameObject wizFrontCheck;
    public float wizFrontCircleRadius;
    public GameObject wizBackCheck;
    public float wizBackCircleRadius;

    private void FixedUpdate() {
        CheckIfGrounded();
    }

    private void CheckIfGrounded(){
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCircleRadius, groundLayer);
        isWalled = Physics2D.OverlapCircle(wallCheck.transform.position, wallCircleRadius, groundLayer);
        isRoofed = Physics2D.OverlapCircle(roofCheck.transform.position, roofCircleRadius, groundLayer);
        wizFront = Physics2D.OverlapCircle(wizFrontCheck.transform.position, wizFrontCircleRadius, wizLayer);
        wizBack = Physics2D.OverlapCircle(wizBackCheck.transform.position, wizBackCircleRadius, wizLayer);
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCircleRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(wallCheck.transform.position, wallCircleRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(roofCheck.transform.position, roofCircleRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(wizFrontCheck.transform.position, wizFrontCircleRadius);
 
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(wizBackCheck.transform.position, wizBackCircleRadius);

        
    }






}
