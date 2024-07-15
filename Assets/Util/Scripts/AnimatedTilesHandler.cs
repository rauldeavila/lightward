using UnityEngine;
using UnityEngine.Tilemaps;

public class AnimatedTilesHandler : MonoBehaviour
{
    private Tilemap tilemap;
    private bool _flag0 = false;
    private bool _flag1 = true;

    private void Awake() {
        tilemap = GetComponent<Tilemap>();
    }

    void Update(){
        if(Time.timeScale == 0){
            if(_flag0){
                _flag1 = true;
                _flag0 = false;
                tilemap.animationFrameRate = 0;
                tilemap.RefreshAllTiles();
            }
        } else {
            if(_flag1){
                _flag0 = true;
                _flag1 = false;
                tilemap.animationFrameRate = 1;
                tilemap.RefreshAllTiles();
            }
        }
    }

}
