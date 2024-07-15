using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour {

    public GameObject upObject;
    public GameObject downObject;

    public bool unlockAlert;
    public bool talkAlert;
    public bool readAlert;
    public bool saveAlert;

    public bool up;
    public bool down;
    
    public Sprite unlock;
    public Sprite abrir;

    public Sprite talk;
    public Sprite falar;

    public Sprite read;
    public Sprite ler;
    
    public Sprite save;
    public Sprite salvar;

    private SpriteRenderer sr;

    void Awake(){
        sr = GetComponentInChildren<SpriteRenderer>();

        if(up){
            upObject.SetActive(true);
            downObject.SetActive(false);
        } else if(down){
            downObject.SetActive(true);
            upObject.SetActive(false);
        }
    }

    void Update(){
        if(unlockAlert){
            if(PlayerPrefs.GetInt("Language", 0) == 0){
                sr.sprite = unlock;
            } else if(PlayerPrefs.GetInt("Language", 0) == 1){
                sr.sprite = abrir;
            }
        } else if(talkAlert){
            if(PlayerPrefs.GetInt("Language", 0) == 0){
                sr.sprite = talk;
            } else if(PlayerPrefs.GetInt("Language", 0) == 1){
                sr.sprite = falar;
            }
        } else if(readAlert){
            if(PlayerPrefs.GetInt("Language", 0) == 0){
                sr.sprite = read;
            } else if(PlayerPrefs.GetInt("Language", 0) == 1){
                sr.sprite = ler;
            }
        } else if(saveAlert){
            if(PlayerPrefs.GetInt("Language", 0) == 0){
                sr.sprite = save;
            } else if(PlayerPrefs.GetInt("Language", 0) == 1){
                sr.sprite = salvar;
            }
        }
    }

}
