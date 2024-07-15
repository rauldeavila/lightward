using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeManager : MonoBehaviour
{
//     private Volume _globalVolume;
//     private List<UnityEngine.Rendering.VolumeProfile> _profiles;
//     private VolumeProfile[] _loadedProfiles;
//     public static VolumeManager Instance;
//     void Awake()
//     {
//         if(Instance != null && Instance != this)
//         {
//             Destroy(this);
//         }
//         else
//         {
//             Instance = this;
//         }
//         LoadProfiles();
//         _globalVolume = GetComponent<Volume>();
//     }


//     void Start()
//     {
//         ApplyRoomObjectProfile();
//     }

//     void LoadProfiles()
//     {
//         print("LoadProfiles()");
//         _profiles = new List<UnityEngine.Rendering.VolumeProfile>();
//         _loadedProfiles = Resources.LoadAll<VolumeProfile>("Volumes");
//         _profiles.AddRange(_loadedProfiles);
//         foreach(VolumeProfile vol in _loadedProfiles)
//         {
//             // print("volume profile: " + vol.name);
//         }
//     }

//     void ApplyRoomObjectProfile()
//     {
//         switch(RoomConfigurations.Instance.GetProfileName())
//         {
//             case "Forest":
//                 GameManager.Instance.CRTProfile =  _profiles.FirstOrDefault(profile => profile.name == "CRT-Forest"); 
//                 if(GameState.Instance.CRT == true)
//                 {
//                     LoadProfileByName("CRT-Forest");
//                 }
//                 else
//                 {
//                     LoadProfileByName("noCRT-noFilter");
//                 }
//                 break;
//             case "Autumn":
//                 GameManager.Instance.CRTProfile =  _profiles.FirstOrDefault(profile => profile.name == "CRT-Autumn"); 
//                 if(GameState.Instance.CRT == true)
//                 {
//                     LoadProfileByName("CRT-Autumn");
//                 }
//                 else
//                 {
//                     LoadProfileByName("noCRT-noFilter");
//                 }
//                 break;
//             case "Heights":
//                 GameManager.Instance.CRTProfile =  _profiles.FirstOrDefault(profile => profile.name == "CRT-Heights"); 
//                 if(GameState.Instance.CRT == true)
//                 {
//                     LoadProfileByName("CRT-Heights");
//                 }
//                 else
//                 {
//                     LoadProfileByName("noCRT-noFilter");
//                 }
//                 break;    
//             case "Summit":
//                 GameManager.Instance.CRTProfile =  _profiles.FirstOrDefault(profile => profile.name == "CRT-Summit"); 
//                 if(GameState.Instance.CRT == true)
//                 {
//                     LoadProfileByName("CRT-Summit");
//                 }
//                 else
//                 {
//                     LoadProfileByName("noCRT-noFilter");
//                 }
//                 break;
//             case "Darkworld":
//                 GameManager.Instance.CRTProfile =  _profiles.FirstOrDefault(profile => profile.name == "CRT-Darkworld"); 
//                 if(GameState.Instance.CRT == true)
//                 {
//                     LoadProfileByName("CRT-Darkworld");
//                 }
//                 else
//                 {
//                     LoadProfileByName("noCRT-noFilter");
//                 }
//                 break;
//             default:
//                 Debug.LogError("No Profile Assigned. Create new case for the switch statement.");
//                 break;
//         }
//     }

//     public void LoadProfileByName(string profileName)
//     {
//         // Find a matching profile
//         VolumeProfile targetProfile = _profiles.FirstOrDefault(profile => profile.name == profileName); 

//         // Check if the profile was found
//         if (targetProfile != null)
//         {
//             _globalVolume.profile = targetProfile;
//             Debug.Log("Profile loaded: " + profileName);
//         }
//         else
//         {
//             Debug.LogWarning("Profile not found: " + profileName);
//         }
//     }

}
