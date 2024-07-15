using UnityEngine;
using UnityEngine.EventSystems;

public class EnableBasedOnSelectedButton : MonoBehaviour {

    private GameObject _previousObject;

    public GameObject primaryObject;    // This object is activated when the primary button is selected
    public GameObject secondaryObject;  // This object is activated when the secondary button is selected

    public GameObject primaryButton;    // Reference to the primary button
    public GameObject secondaryButton;  // Reference to the secondary button

    void Update(){
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;

        if(currentSelected == primaryButton){
            ToggleObjects(primaryObject, secondaryObject);
            if(_previousObject == secondaryObject){
                SFXController.Instance.Play("event:/game/00_ui/ui_changeselection");
            }
            _previousObject = primaryObject;
        } else if(currentSelected == secondaryButton){
            ToggleObjects(secondaryObject, primaryObject);
            if(_previousObject == primaryObject){
                SFXController.Instance.Play("event:/game/00_ui/ui_changeselection");
            }
            _previousObject = secondaryObject;
        }
    }

    private void ToggleObjects(GameObject activeObj, GameObject inactiveObj){

        if(activeObj != null){
            activeObj.SetActive(true);
        }
        if(inactiveObj != null){
            inactiveObj.SetActive(false);
        }
    }
}
