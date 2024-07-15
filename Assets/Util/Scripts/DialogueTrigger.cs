using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
public class DialogueTrigger : MonoBehaviour {

	public bool OnTop = false;
	public bool StartOnTriggerEnter = false;
	public bool StartOnEnable = false;

	public bool StartedDialogue = false;

	public bool PowerupInTheEnd = false;
	[ShowIf("PowerupInTheEnd")]
	public bool Key = false;
	public bool Boots = false;

	public Dialogue Dialogue;
	private bool _canTrigger = true;

	public UnityEvent OnDialogueEnded;

	private void OnTriggerEnter2D(Collider2D collider) {
		if(collider.CompareTag("WizHitBox")){
			if(StartOnTriggerEnter){
				if(_canTrigger){
					_canTrigger = false;
					TriggerDialogue();
					print("triggered");

				}
			}
		}
	}

	void Update()
	{
		if(StartedDialogue && PlayerState.Instance != null)
		{
			if(PlayerState.Instance.Interacting == false)
			{
				OnDialogueEnded.Invoke();
				StartedDialogue = false;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collider) {
		if(StartOnTriggerEnter){
			_canTrigger = true;
		}
	}

	private void OnEnable(){
		if(StartOnEnable){
			Invoke("TriggerDialogue", 0.5f);
		}
	}

	public void TriggerDialogue () {
		StartedDialogue = true;
		FindObjectOfType<DialogueManager>().StartDialogue(Dialogue, OnTop);
		if(PowerupInTheEnd)
		{
			FindObjectOfType<DialogueManager>().GivePowerupInTheEnd = true;
			if(Key)
			{
				NewItem.Instance.ItemToGive = "key";
			}
		}
	}

}
