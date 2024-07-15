using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleChecker : MonoBehaviour
{
    public bool onHole = false;

    
    private void OnTriggerStay2D(Collider2D collider){
        if(collider.CompareTag("Ground")){
            onHole = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collider){
        if(collider.CompareTag("Ground")){
            onHole = true;

        }
    }

}
