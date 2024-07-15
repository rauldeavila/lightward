using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    public Animator m_Animator;
    public SpriteRenderer m_SpriteRenderer;

    void Update()
    {
        if(MathF.Abs(WindController.Instance.windStrength) >= 10f)
        {
            m_Animator.SetBool("Windy", true);
        }
        else
        {
            m_Animator.SetBool("Windy", false);
        }

        if(WindController.Instance.windStrength < 0f){
            m_SpriteRenderer.flipX = true;
        } else if(WindController.Instance.windStrength > 0f)
        {
            m_SpriteRenderer.flipX = false;
        }
    }
}
