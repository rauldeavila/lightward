using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour {

    private PlayerController wiz;

    public BoolValue wiz_has_magic;
    public IntValue wiz_magic;

    private void Awake(){
        wiz = FindObjectOfType<PlayerController>();
    }


}
