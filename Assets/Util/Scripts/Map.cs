using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    // public Camera mapCam;
    // public GameObject objectToFollow;
    // public GameObject wizMark;
    // private PlayerController controller;
    // private Vector3 zeroedVelocity;
    // private Vector2 direction;
    // public float maxSpeed = 100f;

    // private void Awake(){
    //     zeroedVelocity = Vector3.zero;
    //     controller = FindObjectOfType<PlayerController>();
    // }

    // private void Start(){
    //     SetWizMarkPosition();
    //     controller.Controls.Game.Move.performed += ctx => direction = ctx.ReadValue<Vector2>();
    //     CenterCameraOnWizMarker();
    // }

    // private void OnEnable() {
    //     CenterCameraOnWizMarker();
    // }

    // private void OnDisable() {
    //     CenterCameraOnWizMarker();
    // }

    // private void Update(){
    //     MoveCamera(direction.x, direction.y);
    // }

    // private void MoveCamera(float horizontal, float vertical){

    //     Vector2 targetVelocity = new Vector2(horizontal * maxSpeed, vertical * maxSpeed);
    //     objectToFollow.GetComponent<Rigidbody2D>().velocity = Vector3.SmoothDamp(objectToFollow.GetComponent<Rigidbody2D>().velocity, targetVelocity, ref zeroedVelocity, 0.05f);

    // }

    // private void SetWizMarkPosition(){
    //     // See where is Wiz now
    //     string scene = controller.GetComponent<WizPlaceInTheWorldController>().GetCurrentScene();

    //     // Set Wiz Marker Position based on his real pos
    //     switch(scene){
    //         case "tutorial":
    //             wizMark.transform.localPosition = new Vector3(1364.5f, 591.6f, 0f);
    //             break;
    //         case "tutorial_two":
    //             wizMark.transform.localPosition = new Vector3(1313.9f, 610.6f, 0f);
    //             break;
    //         case "graveyard0":
    //             wizMark.transform.localPosition = new Vector3(1274.9f, 550.1f, 0f);
    //             break;
    //         case "graveyard5":
    //             wizMark.transform.localPosition = new Vector3(1300.5f, 474.8f, 0f);
    //             break;
    //         case "graveyard6":
    //             wizMark.transform.localPosition = new Vector3(1363.4f, 474.8f, 0f);
    //             break;
    //         case "graveyard7":
    //             wizMark.transform.localPosition = new Vector3(1328f, 430.6f, 0f);
    //             break;
    //         case "graveyard8":
    //             wizMark.transform.localPosition = new Vector3(1341.6f, 522f, 0f);
    //             break;
    //         case "graveyard9":
    //             wizMark.transform.localPosition = new Vector3(1396.5f, 548.8f, 0f);
    //             break;
    //         case "graveyard_village":
    //             wizMark.transform.localPosition = new Vector3(1544, 459.3f, 0f);
    //             break;
    //         default:
    //             break;
    //     }

    //     CenterCameraOnWizMarker();

    // }

    // private void CenterCameraOnWizMarker(){
    //     objectToFollow.transform.position = new Vector3(wizMark.transform.position.x, wizMark.transform.position.y, 0f);
    // }










}
