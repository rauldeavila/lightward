using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;

// InputPrompt.cs calls this class.
// TO-DO: Make frames 3 and 4 be used instead of frames 1 and 2 when using a Controller.
// - Check if there's or there's not an InputController that has state of the current connected controller
// - Check EnableBasedOnInput and create an InputController if there's not one yet
// - Inplenet the set and get and update everything like for the Language
[System.Serializable]
public struct Messages
{
    public string messageCode;
    public string portugueseMessage;
    public string englishMessage;

}

[System.Serializable]
public struct ButtonFrames
{
    public string buttonName;
    public Sprite frame1;
    public Sprite frame2;
    public Sprite frame3;
    public Sprite frame4;
}


public class WorldSpaceCanvas : MonoBehaviour
{
//     public GameObject textObj;
//     public GameObject inputImage;
//     public ButtonFrames[] buttonFrames; // Array of button frames
//     public Messages[] messagesArray;
//     public static WorldSpaceCanvas Instance; 
//     public bool ShowingPrompt = false;
//     private string _currentPromptCode; // for storing the message that's going to be translated on the fly!

//     void Awake(){
//         if (Instance != null && Instance != this)
//         { 
//             Destroy(this); 
//         } else { 
//             Instance = this; 
//         } 
//     }
//     void Start(){
//         LanguageController.Instance.OnLanguageChanged.AddListener(UpdateLanguage);
//     }
//     private void UpdateTextBasedOnLanguage(string promptCode)
//     {
//         TMP_Text textMesh = textObj.GetComponent<TMP_Text>();
//         if (textMesh == null) 
//         {
//             Debug.LogError("TextMeshPro component not found on the prefab.");
//             return;
//         }

//         foreach (var msg in messagesArray)
//         {
//             if (msg.messageCode == promptCode)
//             {
//                 LanguageController.Language currentLanguage = LanguageController.Instance.GetCurrentLanguage();
//                 switch (currentLanguage)
//                 {
//                     case LanguageController.Language.English:
//                         textMesh.text = msg.englishMessage;
//                         break;
//                     case LanguageController.Language.Portuguese:
//                         textMesh.text = msg.portugueseMessage;
//                         break;
//                 }
//                 break;
//             }
//         }
//     }

//     void UpdateLanguage()
//     {
//         UpdateTextBasedOnLanguage(_currentPromptCode); // Update the text when language changes
//     }

//     public void InputPrompt(string promptCode, string button, float pos_x, float pos_y, Vector2 image_offset)
//     {
//         ShowingPrompt = true;
//         _currentPromptCode = promptCode; // Store the current prompt code
//         UpdateTextBasedOnLanguage(promptCode); // Update the text based on the current language
//         print("Showing input prompt at position: " + pos_x + " " + pos_y);
//         // Set the position of the TextMeshPro object
//         textObj.transform.position = new Vector3(pos_x, pos_y, 0);
//         inputImage.transform.position = new Vector3(pos_x + image_offset.x, pos_y + image_offset.y, 0);

//         // Find and start the animation for the corresponding button
//         foreach (var bf in buttonFrames)
//         {
//             if (bf.buttonName == button)
//             {
//                 StartCoroutine(AnimateImage(bf.frame1, bf.frame2, bf.frame3, bf.frame4)); // Assuming you have an AnimateImage method
//                 StartCoroutine(UpdateTextCoroutine());
//                 break;
//             }
//         }
//     }

//     IEnumerator AnimateImage(Sprite frame1, Sprite frame2, Sprite frame3, Sprite frame4)
//     {
//         var image = inputImage.GetComponent<Image>();
//         if (image == null) yield break;

//         image.preserveAspect = true;
//         bool toggle = false;

//         while (true)
//         {
//             string activeControlScheme = Inputs.Instance.GetActiveControlScheme();

//             if (activeControlScheme == "Keyboard")
//             {
//                 image.sprite = toggle ? frame1 : frame2;
//             }
//             else if (activeControlScheme == "Gamepad")
//             {
//                 image.sprite = toggle ? frame3 : frame4;
//             }
//             image.SetNativeSize();
//             yield return new WaitForSeconds(0.5f); // Change the duration to control the speed of animation
//             toggle = !toggle; // Move the toggle after setting the sprite
//         }
//     }


//     IEnumerator UpdateTextCoroutine()
//     {
//         while (true)
//         {
//             UpdateTextBasedOnLanguage(_currentPromptCode);
//             yield return new WaitForSeconds(0.5f);
//         }
//     }
//     public void HideInputPrompt(){
//         // textObj.transform.position = new Vector3(-9999, -9999, -9999);
//         TMP_Text textMesh = textObj.GetComponent<TMP_Text>();
//         textMesh.text = "";
//         inputImage.transform.position = new Vector3(-9999, -9999, 0);
//         StopAllCoroutines();
//         ShowingPrompt = false;
//     }
}