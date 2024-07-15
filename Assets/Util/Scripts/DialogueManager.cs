using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Febucci.UI;

public class DialogueManager : MonoBehaviour {

	public Image portrait;
	public TextMeshProUGUI dialogueText;
	public Image continueDialogue;
	public Image skipDialogue;
	public Image endDialogue;
	private Animator animator;
	private Queue<string> sentences;
	private Queue<Speaker> speakers;
	public bool FinalDialogue = false;
	private Speaker _currentSpeaker;

	public bool GivePowerupInTheEnd = false;

	private PlayerController controller;
	public static DialogueManager Instance;

	private TextAnimatorPlayer _textAnimator;

	public bool FinishedSentence = true; // NEES TO START TRUE

	private void Awake() {
		
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        } 
		
		_textAnimator = GetComponentInChildren<TextAnimatorPlayer>();
		animator = GetComponent<Animator>();
		controller = FindObjectOfType<PlayerController>();
	}

	void Start () {
		sentences = new Queue<string>();
		speakers = new Queue<Speaker>();
	}

	public void StartDialogue (Dialogue dialogue, bool top) {
		if(PlayerState.Instance)
		{
			PlayerState.Instance.Interacting = true;
		}
		if(InteractionsManager.Instance)
		{
			InteractionsManager.Instance.Dialogue = true;
		}
		

		animator.SetTrigger("startDialogue");


		sentences.Clear();
		if(PlayerState.Instance)
		{
			controller.Move.StopPlayer();
		}

		foreach (string sentence in dialogue.sentences) {
			sentences.Enqueue(sentence);
		}

		foreach (Speaker speaker in dialogue.speakers) {
			speakers.Enqueue(speaker);
		}


		DisplayNextSentence();

	}

	public void SetIconToTypewriter(){
		if(continueDialogue != null){
			continueDialogue.color = new Color32(255, 255, 225, 0);
		}
		if(endDialogue != null){
			endDialogue.color = new Color32( 255, 255, 225, 0);
		}
		if(skipDialogue != null){
			skipDialogue.color = new Color32( 255, 255, 225, 255);
		}
	}

	public void SetIconToNextDialogue(){
		FinishedSentence = true;
		if(FinalDialogue == false)
		{
			if(skipDialogue != null)
			{
				skipDialogue.color = new Color32( 255, 255, 225, 0);
			}
			if(endDialogue != null)
			{
				endDialogue.color = new Color32( 255, 255, 225, 0);
			}
			if(continueDialogue != null)
			{
				continueDialogue.color = new Color32( 255, 255, 225, 255);
			}
		}
		else
		{
			if(skipDialogue != null)
			{
				skipDialogue.color = new Color32( 255, 255, 225, 0);
			}
			if(continueDialogue != null)
			{
				continueDialogue.color = new Color32(255, 255, 225, 0);
			}
			if(endDialogue != null)
			{
				endDialogue.color = new Color32( 255, 255, 225, 255);
			}
		}
	}

	public void SkipTypeOrAdvance()
	{
		if(!FinishedSentence)
		{
			_textAnimator.SkipTypewriter();
			FinishedSentence = true;
			return;
		}
		else
		{
			DisplayNextSentence();
			return;
		} 
	}
	public void DisplayNextSentence () {
		FinishedSentence = false;
		if (sentences.Count == 0) {
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		Speaker speaker = speakers.Dequeue();
		_currentSpeaker = speaker;

		dialogueText.text = _currentSpeaker.name + "\n" + sentence;
		portrait.sprite = speaker.portrait;

		if(sentences.Count < 1 && !FinalDialogue){
			FinalDialogue = true;
			return;
		}
	}

	void EndDialogue() {
		if(PlayerState.Instance)
		{
			PlayerState.Instance.Interacting = false;
			if(controller.AnimatorIsPlaying("new_spell_wait")){
				controller.Animator.Play("idle");
			}
		}
		animator.SetTrigger("endDialogue");

		if(GivePowerupInTheEnd)
		{
			NewItem.Instance.GiveItem();	
			GivePowerupInTheEnd = false;
		}
	}

	public string GetSpeakerName(){
		return _currentSpeaker.name;
	}
    public void PlayTypewriterSound(){
        FMODUnity.RuntimeManager.PlayOneShot("event:/game/00_ui/ui_typewriter", transform.position);
    }


}
