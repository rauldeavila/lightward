using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Speaker", menuName = "Speaker")]
public class Speaker : ScriptableObject {

    [HorizontalGroup("Base", 75)]
    [VerticalGroup("Base/Left"), LabelWidth(75)]
    [PreviewField(100)]
    [LabelText("Portrait")]
    public Sprite portrait; // the portrait
    
    [VerticalGroup("Base/Right"), LabelWidth(50)]
    public string name; // the name that appears on the dialogue
    [VerticalGroup("Base/Right"), LabelWidth(50)]
    public string id; // unique like wiz_happy


}
