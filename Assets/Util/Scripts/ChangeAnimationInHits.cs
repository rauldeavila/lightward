using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(TakeHit))]
[RequireComponent(typeof(PlayParticlesOnAnimator))]
[RequireComponent(typeof(Animator))]
public class ChangeAnimationInHits : MonoBehaviour {

    private PlayerController player;
    private Animator animator;
    [InfoBox("Desabilitar o collider2D (que pode ser trigger ou nao) no Animator")]
    [InfoBox("Animator Trigger = 'GoToSecondAnimation'")]
    public int howManyHitsBeforeChangingAnimation = 0;
    public bool isMoneyRock = false;
    [ShowIf("isMoneyRock")]
    public float upForceMin;
    public float upForceMax;
    [ShowIf("isMoneyRock")]
    public GameObject coinPrefab;
    [ShowIf("isMoneyRock")]
    public GameObject coinPrefabAlt;

    private void Awake() {
        player = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Player")){
            if(howManyHitsBeforeChangingAnimation > 1){
                howManyHitsBeforeChangingAnimation -= 1;

                if(isMoneyRock){
                    DropGold();
                }

            } else {
                howManyHitsBeforeChangingAnimation -= 1;
                if(isMoneyRock){
                    DropLastWaveOfGold();
                }
                animator.SetTrigger("GoToSecondAnimation");
            }
        }
    }

    private void DropGold(){
        GetComponent<PlayParticlesOnAnimator>().PlayParticles2();
        RandomGenerateCoinsBetween(4, 6);
    }

    private void DropLastWaveOfGold(){
        RandomGenerateCoinsBetween(8,10);
    }

    private void RandomGenerateCoinsBetween(int min, int max){
        
        int coinsToGenerate = Random.Range(min, max);

        for(var i = 0; i < coinsToGenerate; i++){
            if(coinPrefab != null){

                 int rand = Random.Range(1,3);
                 if (rand == 1) {
                    GameObject coinObject = (GameObject)Instantiate(coinPrefab, new Vector2(this.transform.position.x, this.transform.position.y + 0.2f), transform.rotation * Quaternion.Euler (0f, 180f, 0f));
                    coinObject.transform.rotation = Quaternion.Euler(0, i % 2 == 0 ? 0 : 180, 0);
                    coinObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(upForceMin, upForceMax));
                } else if (rand == 2) {
                    GameObject coinObject = (GameObject)Instantiate(coinPrefabAlt, new Vector2(this.transform.position.x, this.transform.position.y + 0.2f), transform.rotation * Quaternion.Euler (0f, 180f, 0f));
                    coinObject.transform.rotation = Quaternion.Euler(0, i % 2 == 0 ? 0 : 180, 0);
                    coinObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(upForceMin, upForceMax));
                }

            }
        }
    }
    
}
