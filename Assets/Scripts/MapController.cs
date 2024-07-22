using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BookCurlPro;

public class MapController : MonoBehaviour
{
    private AutoFlip _autoFlip;
    public static MapController Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        { 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
        _autoFlip = GetComponent<AutoFlip>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _autoFlip.FlipLeftPage();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _autoFlip.FlipRightPage();
        }
    }


}
