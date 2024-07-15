using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB_GreenoProjectiles : MonoBehaviour {

    public GameObject projectiles;
    public GameObject position1;
    public GameObject position2;
    public GameObject position3;
    public GameObject position4;
    public GameObject position5;
    public GameObject position6;
    public GameObject position7;
    public GameObject position8;
    public GameObject position9;

    private int position;
    private float x;
    private float y;
    
    public void Instantiate3Projectiles(){
        for(var i = 0; i < 3 ; i++){
            PickPosition();
            Instantiate(projectiles, new Vector2(x, y) , Quaternion.identity);
        }
    }

    public void PickPosition(){
        position = Random.Range(1, 9);
        switch (position) {
            case 1:
                x = position1.transform.position.x;
                y = position1.transform.position.y;
                break;
            case 2:
                x = position2.transform.position.x;
                y = position2.transform.position.y;
                break;
            case 3:
                x = position3.transform.position.x;
                y = position3.transform.position.y;
                break;
            case 4:
                x = position4.transform.position.x;
                y = position4.transform.position.y;
                break;
            case 5:
                x = position5.transform.position.x;
                y = position5.transform.position.y;
                break;
            case 6:
                x = position6.transform.position.x;
                y = position6.transform.position.y;
                break;
            case 7:
                x = position7.transform.position.x;
                y = position7.transform.position.y;
                break;
            case 8:
                x = position8.transform.position.x;
                y = position8.transform.position.y;
                break;
            case 9:
                x = position9.transform.position.x;
                y = position9.transform.position.y;
                break;
            default:
                break;
        }
    }



}
