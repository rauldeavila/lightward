using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainCamera : MonoBehaviour {
    private Camera cam;
    private GameObject _graveyardCameraProps;
    private GameObject _forestCameraProps;
    private GameObject _catacombsCameraProps;
    private GameObject _autumnCameraProps;
    private GameObject _heightsCameraProps;
    private GameObject _summitCameraProps;
    private GameObject _darkWorldCameraProps;

    private Color _darkworldBackgroundColor = new Color32(80, 80, 80, 255); // #161616
    private Color _regularBackgroundColor = new Color32(0, 0, 0, 255);
    public static MainCamera Instance;
    void Awake(){
        if (Instance != null && Instance != this){ 
            Destroy(this); 
        } else { 
            Instance = this; 
        }
        _graveyardCameraProps = transform.Find("Graveyard Camera Props")?.gameObject;
        _forestCameraProps = transform.Find("Forest Camera Props")?.gameObject;
        _catacombsCameraProps = transform.Find("Catacombs Camera Props")?.gameObject;
        _autumnCameraProps = GameObject.Find("Autumn Camera Props")?.gameObject;
        _summitCameraProps = GameObject.Find("Summit Camera Props")?.gameObject;
        _heightsCameraProps = GameObject.Find("Heights Camera Props")?.gameObject; 
        _darkWorldCameraProps = GameObject.Find("Dark World Camera Props")?.gameObject;
    }
    void Start()
    {
        cam = GetComponent<Camera>();
        switch(RoomConfigurations.Instance.Data.AreaName)
        {
            case "Graveyard":
                GraveyardCam(true);
                break;
            case "Catacomb":
                CatacombsCam(true);
                break;
            case "Forest":
                ForestCam(true);
                break;
            case "Autumn":
                AutumnCam(true);
                Debug.LogError("Autumn Area Camera Props is floating on the scene!");
                break;
            case "Heights":
                HeightsCam(true);
                Debug.LogError("Heights Camera Props is floating on the scene!");
                break;
            case "Summit":
                SummitCam(true);
                Debug.LogError("Summit Camera Props is floating on the scene!");
                break;
            case "Darkworld":
                DarkWorldCam(true);
                Debug.LogError("Darkworld camera props is floating on the scene");
                break;
            default:
                Debug.LogError("NO AREA NAME ASSIGNED TO ROOM OBJECT. FIX THIS.");
                break;
        }
    }
    private void SetAllCameraPropsToFalse(){

        cam.backgroundColor = _regularBackgroundColor;
        // print("Regular");
        if(_graveyardCameraProps != null){
            _graveyardCameraProps.SetActive(false);
        }

        if(_forestCameraProps != null){
            _forestCameraProps.SetActive(false);
        }
    }

    public void GraveyardCam(bool flag)
    {
        SetAllCameraPropsToFalse();
        if(_graveyardCameraProps != null)
        {
            _graveyardCameraProps.SetActive(flag);
        }
    }
    public void CatacombsCam(bool flag)
    {
        SetAllCameraPropsToFalse();
        if(_catacombsCameraProps != null)
        {
            _catacombsCameraProps.SetActive(flag);
        }
    }
    public void ForestCam(bool flag)
    {
        SetAllCameraPropsToFalse();
        if(_forestCameraProps != null)
        {
            _forestCameraProps.SetActive(flag);
        }
    }

    public void AutumnCam(bool flag)
    {
        SetAllCameraPropsToFalse();
        if(_autumnCameraProps != null)
        {
            _autumnCameraProps.SetActive(flag);
        }
    }

    public void HeightsCam(bool flag)
    {
        SetAllCameraPropsToFalse();
        if(_heightsCameraProps != null)
        {
            _heightsCameraProps.SetActive(flag);
        }
    }
 
    public void SummitCam(bool flag)
    {
        SetAllCameraPropsToFalse();
        if(_summitCameraProps != null)
        {
            _summitCameraProps.SetActive(flag);
        }
    }

    public void DarkWorldCam(bool flag)
    {
        SetAllCameraPropsToFalse();
        if(_darkWorldCameraProps != null)
        {
            _darkWorldCameraProps.SetActive(flag);
        }
        print("Dark world");
        cam.backgroundColor = _darkworldBackgroundColor;
    }

}
