using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnHeadToSkelly : MonoBehaviour
{

    public bool CanCheckCollision = false;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(CanCheckCollision)
        {
            if(collider.gameObject.name.StartsWith("SkellyHead")) 
            {
                print("SkellyHead entered trigger!");
                Destroy(collider.gameObject);
                // TODO - Play animation on Skelly (returning head -> Idle)
                FindObjectOfType<Skelly>().ReturnHead();
                CanCheckCollision = false;
            }
        }
    }


}
