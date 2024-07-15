using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    //Parallax Scroll Variables
    public Camera cam;//the camera
    public Transform subject;//the subject (usually the player character)


    //Instance variables
    float zPosition;
    Vector2 startPos;


    //Properties
    float twoAspect => cam.aspect * 2;
    float tileWidth => (twoAspect > 3 ? twoAspect : 3);
    float viewWidth => loopSpriteRenderer.sprite.rect.width / loopSpriteRenderer.sprite.pixelsPerUnit;
    Vector2 travel => (Vector2)cam.transform.position - startPos; //2D distance travelled from our starting position
    float distanceFromSubject => transform.position.z - subject.position.z;
    float clippingPlane => (cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;


    //User options
    public bool xAxis = true; //parallax on x?
    public bool yAxis = true; //parallax on y?

    //Loop requirement
    public SpriteRenderer loopSpriteRenderer;


    // Start is called before the first frame update
    void Start() {
        cam = FindObjectOfType<MainCamera>().GetComponent<Camera>();
        subject = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        startPos = transform.position;
        zPosition = transform.position.z;
    }


    // Update is called once per frame
    void FixedUpdate() {
        Vector2 newPos = startPos + travel * parallaxFactor;
        transform.position = new Vector3(xAxis ? newPos.x : startPos.x, yAxis ? newPos.y : startPos.y, zPosition);
    }

}
