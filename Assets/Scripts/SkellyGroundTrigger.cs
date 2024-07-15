using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyGroundTrigger : MonoBehaviour
{
    private Skelly _skelly;

    void Awake()
    {
        _skelly = FindObjectOfType<Skelly>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Laser") && _skelly.Phase2)
        {
            FindObjectOfType<SkellyGroundToDisable>().DestroyGround2();
            Destroy(gameObject);
        }
    }

}
