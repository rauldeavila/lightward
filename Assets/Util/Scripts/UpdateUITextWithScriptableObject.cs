using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateUITextWithScriptableObject : MonoBehaviour {

    public bool UpdateColorWhenFreezing;
    public Color FreezingColor;
    public Color DefaultColor;
	public TextMeshProUGUI UIText;
    public StringValue text;
    public IntValue intValue;
    public FloatValue floatValue;
    public bool useMaxValue = false;
    public bool useInitialValue = false;
    public bool keepUpdating = false;

    void Start(){
        if(text != null){
            if(useInitialValue){
                UIText.text = text.initialValue;
            } else{
                UIText.text = text.runTimeValue;
            }
        }
        if(intValue != null){
            if(useMaxValue){
                UIText.text = intValue.maxValue.ToString();
            } else if(useInitialValue){
                UIText.text = intValue.initialValue.ToString();
            } else {
                UIText.text = intValue.runTimeValue.ToString();
            }
        }
        if(floatValue != null){
            if(useMaxValue){
                UIText.text = floatValue.maxValue.ToString();
            } else if(useInitialValue){
                UIText.text = floatValue.initialValue.ToString();
            } else{
                UIText.text = floatValue.runTimeValue.ToString();
            }
        }

    }

    void Update(){
        if(UpdateColorWhenFreezing){
            if(PlayerState.Instance.Freezing){
                UIText.color = FreezingColor;
            } else{
                UIText.color = DefaultColor;
            }
        }
        if(keepUpdating){
            if(text != null){
                if(useInitialValue){
                    UIText.text = text.initialValue;
                } else{
                    UIText.text = text.runTimeValue;
                }
            }
            if(intValue != null){
                if(useMaxValue){
                    UIText.text = intValue.maxValue.ToString();
                } else if(useInitialValue){
                    UIText.text = intValue.initialValue.ToString();
                } else {
                    UIText.text = intValue.runTimeValue.ToString();
                }
            }
            if(floatValue != null){
                if(useMaxValue){
                    UIText.text = floatValue.maxValue.ToString();
                } else if(useInitialValue){
                    UIText.text = floatValue.initialValue.ToString();
                } else{
                    UIText.text = floatValue.runTimeValue.ToString();
                }
            }
        }
    }




}
