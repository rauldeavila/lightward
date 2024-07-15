using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpRigidbody : MonoBehaviour {
    [SerializeField] Rigidbody2D rb;  // You can reference in inspector or get it however
    [SerializeField] float lerpTime = 1f;   // Time it takes to get to the position - acts a speed
 
    float currentLerpTime;
    bool isLerping;
    Vector3 startPos;
    Vector3 target1;
    Vector3 target2;
    Vector3 target3;
    Vector3 target4;
 
 
    void Awake()
    {
        startPos = transform.position;  // Our current position.  You can update this however.  just examples.
        // endPos = transform.position + transform.up * 5f;    // I just made this up, it's wherever you want the object to go
        currentLerpTime = 0f;
        isLerping = true;
    }
 
 
    void FixedUpdate()
    {
        // //reset when we press spacebar
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     currentLerpTime = 0f;
        //     isLerping = true;
        // }
 
        if (isLerping)
        {
            //increment timer once per frame
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
                isLerping = false;
            }
 
            //lerp
            float percentComplete = currentLerpTime / lerpTime;
            if (percentComplete == 1f) {
                print("end");
            }

            //rb.MovePosition(Vector3.Lerp(startPos, endPos, percentComplete));
        }
    }
}

