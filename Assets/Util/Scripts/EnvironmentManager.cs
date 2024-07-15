using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour {

    private bool _playingDamageWizCoroutine = false;
    private float timeBetweenDamage = 3f;
    Coroutine _damageWizCoroutine = null;
    
    void Update(){
        if(PlayerState.Instance.Freezing && !_playingDamageWizCoroutine && PlayerStats.Instance.Cloak != "blue"){
            _damageWizCoroutine = StartCoroutine(DamageWiz());
        } else if(!PlayerState.Instance.Freezing && _playingDamageWizCoroutine){
            StopCoroutine(_damageWizCoroutine);
            _playingDamageWizCoroutine = false;
        } else if(PlayerStats.Instance.Cloak == "blue" && _playingDamageWizCoroutine){
            StopCoroutine(_damageWizCoroutine);
            _playingDamageWizCoroutine = false;
        }
    }

    IEnumerator DamageWiz(){
        print("ENVIRONMENT DAMAGE");
        _playingDamageWizCoroutine = true;
        yield return new WaitForSeconds(timeBetweenDamage);
        PlayerHealth.Instance.TakeDamage(false, Vector2.zero);

        CheckIfWizIsDead();
        yield return new WaitForSeconds(timeBetweenDamage);
        PlayerHealth.Instance.TakeDamage(false, Vector2.zero);

        CheckIfWizIsDead();
        yield return new WaitForSeconds(timeBetweenDamage);
        PlayerHealth.Instance.TakeDamage(false, Vector2.zero);

        CheckIfWizIsDead();
        yield return new WaitForSeconds(timeBetweenDamage);
        PlayerHealth.Instance.TakeDamage(false, Vector2.zero);

        CheckIfWizIsDead();
        yield return new WaitForSeconds(timeBetweenDamage);
        PlayerHealth.Instance.TakeDamage(false, Vector2.zero);

        CheckIfWizIsDead();
        yield return new WaitForSeconds(timeBetweenDamage);
        PlayerHealth.Instance.TakeDamage(false, Vector2.zero);

        CheckIfWizIsDead();
        yield return new WaitForSeconds(timeBetweenDamage);
        PlayerHealth.Instance.TakeDamage(false, Vector2.zero);

        CheckIfWizIsDead();
        yield return new WaitForSeconds(timeBetweenDamage);
        PlayerHealth.Instance.TakeDamage(false, Vector2.zero);

        CheckIfWizIsDead();
        yield return new WaitForSeconds(timeBetweenDamage);
        PlayerHealth.Instance.TakeDamage(false, Vector2.zero);

        CheckIfWizIsDead();
        yield return new WaitForSeconds(timeBetweenDamage);
        PlayerHealth.Instance.TakeDamage(false, Vector2.zero);

        CheckIfWizIsDead();
        yield return new WaitForSeconds(timeBetweenDamage);
        PlayerHealth.Instance.TakeDamage(false, Vector2.zero);

        CheckIfWizIsDead();
        yield return new WaitForSeconds(timeBetweenDamage);
        PlayerHealth.Instance.TakeDamage(false, Vector2.zero);

        CheckIfWizIsDead();
    }

    private void CheckIfWizIsDead(){
        print("check if wiz is dead does not exist!");
        // if(PlayerHealth.Instance.wiz_health.runTimeValue <= 0f){
        //     StopCoroutine(DamageWiz());
        //     _playingDamageWizCoroutine = false;
        // }
    }

}
