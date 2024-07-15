using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;

/* -------------- Save Manager ------------------------
- Singleton ->  Call in other scripts using: SaveManager.instance.someMethod();
- The main flag is activeSlot.save_slot
- - - - - It defines which save slot is curret being used

*/
public class SaveManager : MonoBehaviour {

    public static SaveManager instance;

    public ActiveSlot activeSlot;
    public Slot1 saveSlot1;
    public Slot2 saveSlot2;
    public Slot3 saveSlot3;
    public WizObjects wizObjects;
    private AutoSaveUI autoSaveUI;

    [HideInInspector]
    private PlayerController wiz;
    private HealthUIController healthPanel;
    private GameManager gameManager;


    private void Awake() {
        instance = this;

        wiz = FindObjectOfType<PlayerController>();
        healthPanel = FindObjectOfType<HealthUIController>();
        autoSaveUI = FindObjectOfType<AutoSaveUI>();
        gameManager = FindObjectOfType<GameManager>();
    }

    #region SAVE

    IEnumerator HeartsAnimation(){
        yield return new WaitForSeconds(0.3f);
        healthPanel.GetComponent<Animator>().Play("hearts-panel-update");
    }

    public void Save(){

        //gameManager.AddSeenRoomsToMap();

        wizObjects.wiz_health.runTimeValue = wizObjects.wiz_health.maxValue;
        wizObjects.wiz_health_yellow.runTimeValue = 0f;
        wizObjects.wiz_magic.runTimeValue = wizObjects.wiz_magic.maxValue;
        StartCoroutine(HeartsAnimation());
        wizObjects.lastFirepitSceneName.initialValue = SceneManager.GetActiveScene().name;

         if(activeSlot.save_slot.initialValue == 1){
            SaveSlot1(false);
        } else if(activeSlot.save_slot.initialValue == 2){
            SaveSlot2(false);
        } else if(activeSlot.save_slot.initialValue == 3){
            SaveSlot3(false);
        } 
        activeSlot._this_slot_has_been_saved.initialValue = true;

    }

    public void AutoSave(){

        // SHOW SAVING IN UI
        if(activeSlot.save_slot.initialValue == 1){
            SaveSlot1(true);
        } else if(activeSlot.save_slot.initialValue == 2){
            SaveSlot2(true);
        } else if(activeSlot.save_slot.initialValue == 3){
            SaveSlot3(true);
        } 
        activeSlot._this_slot_has_been_saved.initialValue = true;

    }

    private void SaveSlot1(bool autoSaving){

        autoSaveUI.ActivateAutoSaveUI();

        if(autoSaving == false){
            saveSlot1.s1_saved_game = true;
            wizObjects.wiz_facing_right.initialValue = wiz.State.FacingRight;
            wizObjects.wiz_x.initialValue= wiz.transform.position.x;
            wizObjects.wiz_y.initialValue = wiz.transform.position.y;
        }

        saveSlot1.s1_timestamp = wizObjects.savefile_timestamp.initialValue;
        saveSlot1.s1_facing_right = wizObjects.wiz_facing_right.initialValue;
        saveSlot1.s1_wiz_x = wizObjects.wiz_x.initialValue;
        saveSlot1.s1_wiz_y = wizObjects.wiz_y.initialValue;
        saveSlot1.s1_scene_name = wizObjects.lastFirepitSceneName.initialValue;
        saveSlot1.s1_wiz_max_health = wizObjects.wiz_health.maxValue;
        saveSlot1.s1_wiz_health = wizObjects.wiz_health.maxValue;
        saveSlot1.s1_has_magic = wizObjects.wiz_has_magic.initialValue;
        saveSlot1.s1_wiz_magic = wizObjects.wiz_magic.initialValue;
        saveSlot1.s1_wiz_gold = wizObjects.wiz_gold.initialValue;
        saveSlot1.s1_wiz_keys = wizObjects.wiz_keys.initialValue;
        saveSlot1.s1_wiz_emptyBottles = wizObjects.wiz_emptyBottles.initialValue;

        saveSlot1.s1_magic_slot1 = wizObjects.magic_slot1.initialValue;
        saveSlot1.s1_magic_slot2 = wizObjects.magic_slot2.initialValue;

        saveSlot1.s1_wiz_attack3 = wizObjects.wiz_attack3_powerup.initialValue;
        saveSlot1.s1_wiz_wall_jump = wizObjects.wiz_wall_jump_powerup.initialValue;
        saveSlot1.s1_has_wolfwrath = wizObjects.wiz_has_wolfwrath.initialValue;
        saveSlot1.s1_has_dashingsoul = wizObjects.wiz_has_dashingsoul.initialValue;
        saveSlot1.s1_has_dashinglight = wizObjects.wiz_has_dashinglight.initialValue;
        saveSlot1.s1_has_timecontrol = wizObjects.wiz_has_timecontrol.initialValue;
        saveSlot1.s1_has_fireball = wizObjects.wiz_has_fireball.initialValue;
        saveSlot1.s1_has_healthrecover = wizObjects.wiz_has_healthrecover.initialValue;

        saveSlot1.s1_heart_container1 = wizObjects.heart_container1.initialValue;
        saveSlot1.s1_heart_container2 = wizObjects.heart_container2.initialValue;
        saveSlot1.s1_heart_container3 = wizObjects.heart_container3.initialValue;
        saveSlot1.s1_heart_container4 = wizObjects.heart_container4.initialValue;
        saveSlot1.s1_heart_container5 = wizObjects.heart_container5.initialValue;
        saveSlot1.s1_heart_container6 = wizObjects.heart_container6.initialValue;
        saveSlot1.s1_heart_container7 = wizObjects.heart_container7.initialValue;
        saveSlot1.s1_heart_container8 = wizObjects.heart_container8.initialValue;
        saveSlot1.s1_heart_container9 = wizObjects.heart_container9.initialValue;
        saveSlot1.s1_heart_container10 = wizObjects.heart_container10.initialValue;

        saveSlot1.s1_purified_statue1 = wizObjects.purified_statue1.initialValue;
        saveSlot1.s1_purified_statue2 = wizObjects.purified_statue2.initialValue;
        saveSlot1.s1_purified_statue3 = wizObjects.purified_statue3.initialValue;
        saveSlot1.s1_purified_statue4 = wizObjects.purified_statue4.initialValue;
        saveSlot1.s1_purified_statue5 = wizObjects.purified_statue5.initialValue;

        saveSlot1.s1_gate1 = wizObjects.gate1.initialValue;
        saveSlot1.s1_gate2 = wizObjects.gate2.initialValue;
        saveSlot1.s1_gate3 = wizObjects.gate3.initialValue;
        saveSlot1.s1_gate4 = wizObjects.gate4.initialValue;
        saveSlot1.s1_gate5 = wizObjects.gate5.initialValue;
        saveSlot1.s1_gate6 = wizObjects.gate6.initialValue;
        saveSlot1.s1_gate7 = wizObjects.gate7.initialValue;
        saveSlot1.s1_gate8 = wizObjects.gate8.initialValue;
        saveSlot1.s1_gate9 = wizObjects.gate9.initialValue;
        saveSlot1.s1_gate10 = wizObjects.gate10.initialValue;

        saveSlot1.s1_hidden1 = wizObjects.hidden1.initialValue;
        saveSlot1.s1_hidden2 = wizObjects.hidden2.initialValue;
        saveSlot1.s1_hidden3 = wizObjects.hidden3.initialValue;
        saveSlot1.s1_hidden4 = wizObjects.hidden4.initialValue;
        saveSlot1.s1_hidden5 = wizObjects.hidden5.initialValue;
        saveSlot1.s1_hidden6 = wizObjects.hidden6.initialValue;
        saveSlot1.s1_hidden7 = wizObjects.hidden7.initialValue;
        saveSlot1.s1_hidden8 = wizObjects.hidden8.initialValue;
        saveSlot1.s1_hidden9 = wizObjects.hidden9.initialValue;
        saveSlot1.s1_hidden10 = wizObjects.hidden10.initialValue;

        saveSlot1.s1_boss1 = wizObjects.boss1.initialValue;
        saveSlot1.s1_boss2 = wizObjects.boss2.initialValue;
        saveSlot1.s1_boss3 = wizObjects.boss3.initialValue;
        saveSlot1.s1_boss4 = wizObjects.boss4.initialValue;
        saveSlot1.s1_boss5 = wizObjects.boss5.initialValue;
        saveSlot1.s1_boss6 = wizObjects.boss6.initialValue;
        saveSlot1.s1_boss7 = wizObjects.boss7.initialValue;
        saveSlot1.s1_boss8 = wizObjects.boss8.initialValue;
        saveSlot1.s1_boss9 = wizObjects.boss9.initialValue;
        saveSlot1.s1_boss10 = wizObjects.boss10.initialValue;

        saveSlot1.s1_event1 = wizObjects.event1.initialValue;
        saveSlot1.s1_event2 = wizObjects.event2.initialValue;
        saveSlot1.s1_event3 = wizObjects.event3.initialValue;
        saveSlot1.s1_event4 = wizObjects.event4.initialValue;
        saveSlot1.s1_event5 = wizObjects.event5.initialValue;
        saveSlot1.s1_event6 = wizObjects.event6.initialValue;
        saveSlot1.s1_event7 = wizObjects.event7.initialValue;
        saveSlot1.s1_event8 = wizObjects.event8.initialValue;
        saveSlot1.s1_event9 = wizObjects.event9.initialValue;
        saveSlot1.s1_event10 = wizObjects.event10.initialValue;
        saveSlot1.s1_event11 = wizObjects.event11.initialValue;
        saveSlot1.s1_event12 = wizObjects.event12.initialValue;
        saveSlot1.s1_event13 = wizObjects.event13.initialValue;
        saveSlot1.s1_event14 = wizObjects.event14.initialValue;
        saveSlot1.s1_event15 = wizObjects.event15.initialValue;
        saveSlot1.s1_event16 = wizObjects.event16.initialValue;
        saveSlot1.s1_event17 = wizObjects.event17.initialValue;
        saveSlot1.s1_event18 = wizObjects.event18.initialValue;
        saveSlot1.s1_event19 = wizObjects.event19.initialValue;
        saveSlot1.s1_event20 = wizObjects.event20.initialValue;
        saveSlot1.s1_event21 = wizObjects.event21.initialValue;
        saveSlot1.s1_event22 = wizObjects.event22.initialValue;
        saveSlot1.s1_event23 = wizObjects.event23.initialValue;
        saveSlot1.s1_event24 = wizObjects.event24.initialValue;
        saveSlot1.s1_event25 = wizObjects.event25.initialValue;
        saveSlot1.s1_event26 = wizObjects.event26.initialValue;
        saveSlot1.s1_event27 = wizObjects.event27.initialValue;
        saveSlot1.s1_event28 = wizObjects.event28.initialValue;
        saveSlot1.s1_event29 = wizObjects.event29.initialValue;
        saveSlot1.s1_event30 = wizObjects.event30.initialValue;

        saveSlot1.s1_currentScene = wizObjects.currentScene.initialValue;
        saveSlot1.s1_previousScene = wizObjects.previousScene.initialValue;

        saveSlot1.s1_seenArea1 = wizObjects.seenArea1.initialValue;
        saveSlot1.s1_seenArea2 = wizObjects.seenArea2.initialValue;
        saveSlot1.s1_seenArea3 = wizObjects.seenArea3.initialValue;
        saveSlot1.s1_seenArea4 = wizObjects.seenArea4.initialValue;
        saveSlot1.s1_seenArea5 = wizObjects.seenArea5.initialValue;
        saveSlot1.s1_seenArea6 = wizObjects.seenArea6.initialValue;
        saveSlot1.s1_seenArea7 = wizObjects.seenArea7.initialValue;
        saveSlot1.s1_seenArea8 = wizObjects.seenArea8.initialValue;
        saveSlot1.s1_seenArea9 = wizObjects.seenArea9.initialValue;
        saveSlot1.s1_seenArea10 = wizObjects.seenArea10.initialValue;

        saveSlot1.s1_room1 = wizObjects.room1.initialValue;
        saveSlot1.s1_room2 = wizObjects.room2.initialValue;
        saveSlot1.s1_room3 = wizObjects.room3.initialValue;
        saveSlot1.s1_room4 = wizObjects.room4.initialValue;
        saveSlot1.s1_room5 = wizObjects.room5.initialValue;
        saveSlot1.s1_room6 = wizObjects.room6.initialValue;
        saveSlot1.s1_room7 = wizObjects.room7.initialValue;
        saveSlot1.s1_room8 = wizObjects.room8.initialValue;
        saveSlot1.s1_room9 = wizObjects.room9.initialValue;
        saveSlot1.s1_room10 = wizObjects.room10.initialValue;
        saveSlot1.s1_room11 = wizObjects.room11.initialValue;
        saveSlot1.s1_room12 = wizObjects.room12.initialValue;
        saveSlot1.s1_room13 = wizObjects.room13.initialValue;
        saveSlot1.s1_room14 = wizObjects.room14.initialValue;
        saveSlot1.s1_room15 = wizObjects.room15.initialValue;
        saveSlot1.s1_room16 = wizObjects.room16.initialValue;
        saveSlot1.s1_room17 = wizObjects.room17.initialValue;
        saveSlot1.s1_room18 = wizObjects.room18.initialValue;
        saveSlot1.s1_room19 = wizObjects.room19.initialValue;
        saveSlot1.s1_room20 = wizObjects.room20.initialValue;
        saveSlot1.s1_room21 = wizObjects.room21.initialValue;
        saveSlot1.s1_room22 = wizObjects.room22.initialValue;
        saveSlot1.s1_room23 = wizObjects.room23.initialValue;
        saveSlot1.s1_room24 = wizObjects.room24.initialValue;
        saveSlot1.s1_room25 = wizObjects.room25.initialValue;
        saveSlot1.s1_room26 = wizObjects.room26.initialValue;
        saveSlot1.s1_room27 = wizObjects.room27.initialValue;
        saveSlot1.s1_room28 = wizObjects.room28.initialValue;
        saveSlot1.s1_room29 = wizObjects.room29.initialValue;
        saveSlot1.s1_room30 = wizObjects.room30.initialValue;


        string dataPath = Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(Slot1));
        var stream = new FileStream(dataPath + "/"  + "save1.wiz", FileMode.Create);
        serializer.Serialize(stream, saveSlot1);
        stream.Close();
        autoSaveUI.DeactivateautoSaveUI();
    }

    private void SaveSlot2(bool autoSaving){

        autoSaveUI.ActivateAutoSaveUI();
        if(autoSaving == false){
            saveSlot2.s2_saved_game = true;
            wizObjects.wiz_facing_right.initialValue = wiz.State.FacingRight;
            wizObjects.wiz_x.initialValue= wiz.transform.position.x;
            wizObjects.wiz_y.initialValue = wiz.transform.position.y;
        } 
        
        saveSlot2.s2_timestamp = wizObjects.savefile_timestamp.initialValue;
        saveSlot2.s2_facing_right = wizObjects.wiz_facing_right.initialValue;
        saveSlot2.s2_wiz_x = wizObjects.wiz_x.initialValue;
        saveSlot2.s2_wiz_y = wizObjects.wiz_y.initialValue;
        saveSlot2.s2_scene_name = wizObjects.lastFirepitSceneName.initialValue;
        saveSlot2.s2_wiz_max_health = wizObjects.wiz_health.maxValue;
        saveSlot2.s2_wiz_health = wizObjects.wiz_health.maxValue;
        saveSlot2.s2_has_magic = wizObjects.wiz_has_magic.initialValue;
        saveSlot2.s2_wiz_magic = wizObjects.wiz_magic.initialValue;
        saveSlot2.s2_wiz_gold = wizObjects.wiz_gold.initialValue;
        saveSlot2.s2_wiz_keys = wizObjects.wiz_keys.initialValue;
        saveSlot2.s2_wiz_emptyBottles = wizObjects.wiz_emptyBottles.initialValue;

        saveSlot2.s2_magic_slot1 = wizObjects.magic_slot1.initialValue;
        saveSlot2.s2_magic_slot2 = wizObjects.magic_slot2.initialValue;        

        saveSlot2.s2_wiz_attack3 = wizObjects.wiz_attack3_powerup.initialValue;
        saveSlot2.s2_wiz_wall_jump = wizObjects.wiz_wall_jump_powerup.initialValue;
        saveSlot2.s2_has_wolfwrath = wizObjects.wiz_has_wolfwrath.initialValue;
        saveSlot2.s2_has_dashingsoul = wizObjects.wiz_has_dashingsoul.initialValue;
        saveSlot2.s2_has_dashinglight = wizObjects.wiz_has_dashinglight.initialValue;
        saveSlot2.s2_has_timecontrol = wizObjects.wiz_has_timecontrol.initialValue;
        saveSlot2.s2_has_fireball = wizObjects.wiz_has_fireball.initialValue;
        saveSlot2.s2_has_healthrecover = wizObjects.wiz_has_healthrecover.initialValue;

        saveSlot2.s2_heart_container1 = wizObjects.heart_container1.initialValue;
        saveSlot2.s2_heart_container2 = wizObjects.heart_container2.initialValue;
        saveSlot2.s2_heart_container3 = wizObjects.heart_container3.initialValue;
        saveSlot2.s2_heart_container4 = wizObjects.heart_container4.initialValue;
        saveSlot2.s2_heart_container5 = wizObjects.heart_container5.initialValue;
        saveSlot2.s2_heart_container6 = wizObjects.heart_container6.initialValue;
        saveSlot2.s2_heart_container7 = wizObjects.heart_container7.initialValue;
        saveSlot2.s2_heart_container8 = wizObjects.heart_container8.initialValue;
        saveSlot2.s2_heart_container9 = wizObjects.heart_container9.initialValue;
        saveSlot2.s2_heart_container10 = wizObjects.heart_container10.initialValue;

        saveSlot2.s2_purified_statue1 = wizObjects.purified_statue1.initialValue;
        saveSlot2.s2_purified_statue2 = wizObjects.purified_statue2.initialValue;
        saveSlot2.s2_purified_statue3 = wizObjects.purified_statue3.initialValue;
        saveSlot2.s2_purified_statue4 = wizObjects.purified_statue4.initialValue;
        saveSlot2.s2_purified_statue5 = wizObjects.purified_statue5.initialValue;

        saveSlot2.s2_gate1 = wizObjects.gate1.initialValue;
        saveSlot2.s2_gate2 = wizObjects.gate2.initialValue;
        saveSlot2.s2_gate3 = wizObjects.gate3.initialValue;
        saveSlot2.s2_gate4 = wizObjects.gate4.initialValue;
        saveSlot2.s2_gate5 = wizObjects.gate5.initialValue;
        saveSlot2.s2_gate6 = wizObjects.gate6.initialValue;
        saveSlot2.s2_gate7 = wizObjects.gate7.initialValue;
        saveSlot2.s2_gate8 = wizObjects.gate8.initialValue;
        saveSlot2.s2_gate9 = wizObjects.gate9.initialValue;
        saveSlot2.s2_gate10 = wizObjects.gate10.initialValue;

        saveSlot2.s2_hidden1 = wizObjects.hidden1.initialValue;
        saveSlot2.s2_hidden2 = wizObjects.hidden2.initialValue;
        saveSlot2.s2_hidden3 = wizObjects.hidden3.initialValue;
        saveSlot2.s2_hidden4 = wizObjects.hidden4.initialValue;
        saveSlot2.s2_hidden5 = wizObjects.hidden5.initialValue;
        saveSlot2.s2_hidden6 = wizObjects.hidden6.initialValue;
        saveSlot2.s2_hidden7 = wizObjects.hidden7.initialValue;
        saveSlot2.s2_hidden8 = wizObjects.hidden8.initialValue;
        saveSlot2.s2_hidden9 = wizObjects.hidden9.initialValue;
        saveSlot2.s2_hidden10 = wizObjects.hidden10.initialValue;

        saveSlot2.s2_boss1 = wizObjects.boss1.initialValue;
        saveSlot2.s2_boss2 = wizObjects.boss2.initialValue;
        saveSlot2.s2_boss3 = wizObjects.boss3.initialValue;
        saveSlot2.s2_boss4 = wizObjects.boss4.initialValue;
        saveSlot2.s2_boss5 = wizObjects.boss5.initialValue;
        saveSlot2.s2_boss6 = wizObjects.boss6.initialValue;
        saveSlot2.s2_boss7 = wizObjects.boss7.initialValue;
        saveSlot2.s2_boss8 = wizObjects.boss8.initialValue;
        saveSlot2.s2_boss9 = wizObjects.boss9.initialValue;
        saveSlot2.s2_boss10 = wizObjects.boss10.initialValue;

        saveSlot2.s2_event1 = wizObjects.event1.initialValue;
        saveSlot2.s2_event2 = wizObjects.event2.initialValue;
        saveSlot2.s2_event3 = wizObjects.event3.initialValue;
        saveSlot2.s2_event4 = wizObjects.event4.initialValue;
        saveSlot2.s2_event5 = wizObjects.event5.initialValue;
        saveSlot2.s2_event6 = wizObjects.event6.initialValue;
        saveSlot2.s2_event7 = wizObjects.event7.initialValue;
        saveSlot2.s2_event8 = wizObjects.event8.initialValue;
        saveSlot2.s2_event9 = wizObjects.event9.initialValue;
        saveSlot2.s2_event10 = wizObjects.event10.initialValue;
        saveSlot2.s2_event11 = wizObjects.event11.initialValue;
        saveSlot2.s2_event12 = wizObjects.event12.initialValue;
        saveSlot2.s2_event13 = wizObjects.event13.initialValue;
        saveSlot2.s2_event14 = wizObjects.event14.initialValue;
        saveSlot2.s2_event15 = wizObjects.event15.initialValue;
        saveSlot2.s2_event16 = wizObjects.event16.initialValue;
        saveSlot2.s2_event17 = wizObjects.event17.initialValue;
        saveSlot2.s2_event18 = wizObjects.event18.initialValue;
        saveSlot2.s2_event19 = wizObjects.event19.initialValue;
        saveSlot2.s2_event20 = wizObjects.event20.initialValue;
        saveSlot2.s2_event21 = wizObjects.event21.initialValue;
        saveSlot2.s2_event22 = wizObjects.event22.initialValue;
        saveSlot2.s2_event23 = wizObjects.event23.initialValue;
        saveSlot2.s2_event24 = wizObjects.event24.initialValue;
        saveSlot2.s2_event25 = wizObjects.event25.initialValue;
        saveSlot2.s2_event26 = wizObjects.event26.initialValue;
        saveSlot2.s2_event27 = wizObjects.event27.initialValue;
        saveSlot2.s2_event28 = wizObjects.event28.initialValue;
        saveSlot2.s2_event29 = wizObjects.event29.initialValue;
        saveSlot2.s2_event30 = wizObjects.event30.initialValue;

        saveSlot2.s2_currentScene = wizObjects.currentScene.initialValue;
        saveSlot2.s2_previousScene = wizObjects.previousScene.initialValue;

        saveSlot2.s2_seenArea1 = wizObjects.seenArea1.initialValue;
        saveSlot2.s2_seenArea2 = wizObjects.seenArea2.initialValue;
        saveSlot2.s2_seenArea3 = wizObjects.seenArea3.initialValue;
        saveSlot2.s2_seenArea4 = wizObjects.seenArea4.initialValue;
        saveSlot2.s2_seenArea5 = wizObjects.seenArea5.initialValue;
        saveSlot2.s2_seenArea6 = wizObjects.seenArea6.initialValue;
        saveSlot2.s2_seenArea7 = wizObjects.seenArea7.initialValue;
        saveSlot2.s2_seenArea8 = wizObjects.seenArea8.initialValue;
        saveSlot2.s2_seenArea9 = wizObjects.seenArea9.initialValue;
        saveSlot2.s2_seenArea10 = wizObjects.seenArea10.initialValue;

        saveSlot2.s2_room1 = wizObjects.room1.initialValue;
        saveSlot2.s2_room2 = wizObjects.room2.initialValue;
        saveSlot2.s2_room3 = wizObjects.room3.initialValue;
        saveSlot2.s2_room4 = wizObjects.room4.initialValue;
        saveSlot2.s2_room5 = wizObjects.room5.initialValue;
        saveSlot2.s2_room6 = wizObjects.room6.initialValue;
        saveSlot2.s2_room7 = wizObjects.room7.initialValue;
        saveSlot2.s2_room8 = wizObjects.room8.initialValue;
        saveSlot2.s2_room9 = wizObjects.room9.initialValue;
        saveSlot2.s2_room10 = wizObjects.room10.initialValue;
        saveSlot2.s2_room11 = wizObjects.room11.initialValue;
        saveSlot2.s2_room12 = wizObjects.room12.initialValue;
        saveSlot2.s2_room13 = wizObjects.room13.initialValue;
        saveSlot2.s2_room14 = wizObjects.room14.initialValue;
        saveSlot2.s2_room15 = wizObjects.room15.initialValue;
        saveSlot2.s2_room16 = wizObjects.room16.initialValue;
        saveSlot2.s2_room17 = wizObjects.room17.initialValue;
        saveSlot2.s2_room18 = wizObjects.room18.initialValue;
        saveSlot2.s2_room19 = wizObjects.room19.initialValue;
        saveSlot2.s2_room20 = wizObjects.room20.initialValue;
        saveSlot2.s2_room21 = wizObjects.room21.initialValue;
        saveSlot2.s2_room22 = wizObjects.room22.initialValue;
        saveSlot2.s2_room23 = wizObjects.room23.initialValue;
        saveSlot2.s2_room24 = wizObjects.room24.initialValue;
        saveSlot2.s2_room25 = wizObjects.room25.initialValue;
        saveSlot2.s2_room26 = wizObjects.room26.initialValue;
        saveSlot2.s2_room27 = wizObjects.room27.initialValue;
        saveSlot2.s2_room28 = wizObjects.room28.initialValue;
        saveSlot2.s2_room29 = wizObjects.room29.initialValue;
        saveSlot2.s2_room30 = wizObjects.room30.initialValue;


        string dataPath = Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(Slot2));
        var stream = new FileStream(dataPath + "/"  + "save2.wiz", FileMode.Create);
        serializer.Serialize(stream, saveSlot2);
        stream.Close();
        autoSaveUI.DeactivateautoSaveUI();
    }

    private void SaveSlot3(bool autoSaving){

        autoSaveUI.ActivateAutoSaveUI();
        if(autoSaving == false){
            saveSlot3.s3_saved_game = true;
            wizObjects.wiz_facing_right.initialValue = wiz.State.FacingRight;
            wizObjects.wiz_x.initialValue= wiz.transform.position.x;
            wizObjects.wiz_y.initialValue = wiz.transform.position.y;
        }

        saveSlot3.s3_timestamp = wizObjects.savefile_timestamp.initialValue;
        saveSlot3.s3_facing_right = wizObjects.wiz_facing_right.initialValue;
        saveSlot3.s3_wiz_x = wizObjects.wiz_x.initialValue;
        saveSlot3.s3_wiz_y = wizObjects.wiz_y.initialValue;
        saveSlot3.s3_scene_name = wizObjects.lastFirepitSceneName.initialValue;
        saveSlot3.s3_wiz_max_health = wizObjects.wiz_health.maxValue;
        saveSlot3.s3_wiz_health = wizObjects.wiz_health.maxValue;
        saveSlot3.s3_has_magic = wizObjects.wiz_has_magic.initialValue;
        saveSlot3.s3_wiz_magic = wizObjects.wiz_magic.initialValue;
        saveSlot3.s3_wiz_gold = wizObjects.wiz_gold.initialValue;
        saveSlot3.s3_wiz_keys = wizObjects.wiz_keys.initialValue;
        saveSlot3.s3_wiz_emptyBottles = wizObjects.wiz_emptyBottles.initialValue;

        saveSlot3.s3_magic_slot1 = wizObjects.magic_slot1.initialValue;
        saveSlot3.s3_magic_slot2 = wizObjects.magic_slot2.initialValue;

        saveSlot3.s3_wiz_attack3 = wizObjects.wiz_attack3_powerup.initialValue;
        saveSlot3.s3_wiz_wall_jump = wizObjects.wiz_wall_jump_powerup.initialValue;
        saveSlot3.s3_has_wolfwrath = wizObjects.wiz_has_wolfwrath.initialValue;
        saveSlot3.s3_has_dashingsoul = wizObjects.wiz_has_dashingsoul.initialValue;
        saveSlot3.s3_has_dashinglight = wizObjects.wiz_has_dashinglight.initialValue;
        saveSlot3.s3_has_timecontrol = wizObjects.wiz_has_timecontrol.initialValue;
        saveSlot3.s3_has_fireball = wizObjects.wiz_has_fireball.initialValue;
        saveSlot3.s3_has_healthrecover = wizObjects.wiz_has_healthrecover.initialValue;

        saveSlot3.s3_heart_container1 = wizObjects.heart_container1.initialValue;
        saveSlot3.s3_heart_container2 = wizObjects.heart_container2.initialValue;
        saveSlot3.s3_heart_container3 = wizObjects.heart_container3.initialValue;
        saveSlot3.s3_heart_container4 = wizObjects.heart_container4.initialValue;
        saveSlot3.s3_heart_container5 = wizObjects.heart_container5.initialValue;
        saveSlot3.s3_heart_container6 = wizObjects.heart_container6.initialValue;
        saveSlot3.s3_heart_container7 = wizObjects.heart_container7.initialValue;
        saveSlot3.s3_heart_container8 = wizObjects.heart_container8.initialValue;
        saveSlot3.s3_heart_container9 = wizObjects.heart_container9.initialValue;
        saveSlot3.s3_heart_container10 = wizObjects.heart_container10.initialValue;

        saveSlot3.s3_purified_statue1 = wizObjects.purified_statue1.initialValue;
        saveSlot3.s3_purified_statue2 = wizObjects.purified_statue2.initialValue;
        saveSlot3.s3_purified_statue3 = wizObjects.purified_statue3.initialValue;
        saveSlot3.s3_purified_statue4 = wizObjects.purified_statue4.initialValue;
        saveSlot3.s3_purified_statue5 = wizObjects.purified_statue5.initialValue;

        saveSlot3.s3_gate1 = wizObjects.gate1.initialValue;
        saveSlot3.s3_gate2 = wizObjects.gate2.initialValue;
        saveSlot3.s3_gate3 = wizObjects.gate3.initialValue;
        saveSlot3.s3_gate4 = wizObjects.gate4.initialValue;
        saveSlot3.s3_gate5 = wizObjects.gate5.initialValue;
        saveSlot3.s3_gate6 = wizObjects.gate6.initialValue;
        saveSlot3.s3_gate7 = wizObjects.gate7.initialValue;
        saveSlot3.s3_gate8 = wizObjects.gate8.initialValue;
        saveSlot3.s3_gate9 = wizObjects.gate9.initialValue;
        saveSlot3.s3_gate10 = wizObjects.gate10.initialValue;

        saveSlot3.s3_hidden1 = wizObjects.hidden1.initialValue;
        saveSlot3.s3_hidden2 = wizObjects.hidden2.initialValue;
        saveSlot3.s3_hidden3 = wizObjects.hidden3.initialValue;
        saveSlot3.s3_hidden4 = wizObjects.hidden4.initialValue;
        saveSlot3.s3_hidden5 = wizObjects.hidden5.initialValue;
        saveSlot3.s3_hidden6 = wizObjects.hidden6.initialValue;
        saveSlot3.s3_hidden7 = wizObjects.hidden7.initialValue;
        saveSlot3.s3_hidden8 = wizObjects.hidden8.initialValue;
        saveSlot3.s3_hidden9 = wizObjects.hidden9.initialValue;
        saveSlot3.s3_hidden10 = wizObjects.hidden10.initialValue;

        saveSlot3.s3_boss1 = wizObjects.boss1.initialValue;
        saveSlot3.s3_boss2 = wizObjects.boss2.initialValue;
        saveSlot3.s3_boss3 = wizObjects.boss3.initialValue;
        saveSlot3.s3_boss4 = wizObjects.boss4.initialValue;
        saveSlot3.s3_boss5 = wizObjects.boss5.initialValue;
        saveSlot3.s3_boss6 = wizObjects.boss6.initialValue;
        saveSlot3.s3_boss7 = wizObjects.boss7.initialValue;
        saveSlot3.s3_boss8 = wizObjects.boss8.initialValue;
        saveSlot3.s3_boss9 = wizObjects.boss9.initialValue;
        saveSlot3.s3_boss10 = wizObjects.boss10.initialValue;

        saveSlot3.s3_event1 = wizObjects.event1.initialValue;
        saveSlot3.s3_event2 = wizObjects.event2.initialValue;
        saveSlot3.s3_event3 = wizObjects.event3.initialValue;
        saveSlot3.s3_event4 = wizObjects.event4.initialValue;
        saveSlot3.s3_event5 = wizObjects.event5.initialValue;
        saveSlot3.s3_event6 = wizObjects.event6.initialValue;
        saveSlot3.s3_event7 = wizObjects.event7.initialValue;
        saveSlot3.s3_event8 = wizObjects.event8.initialValue;
        saveSlot3.s3_event9 = wizObjects.event9.initialValue;
        saveSlot3.s3_event10 = wizObjects.event10.initialValue;
        saveSlot3.s3_event11 = wizObjects.event11.initialValue;
        saveSlot3.s3_event12 = wizObjects.event12.initialValue;
        saveSlot3.s3_event13 = wizObjects.event13.initialValue;
        saveSlot3.s3_event14 = wizObjects.event14.initialValue;
        saveSlot3.s3_event15 = wizObjects.event15.initialValue;
        saveSlot3.s3_event16 = wizObjects.event16.initialValue;
        saveSlot3.s3_event17 = wizObjects.event17.initialValue;
        saveSlot3.s3_event18 = wizObjects.event18.initialValue;
        saveSlot3.s3_event19 = wizObjects.event19.initialValue;
        saveSlot3.s3_event20 = wizObjects.event20.initialValue;
        saveSlot3.s3_event21 = wizObjects.event21.initialValue;
        saveSlot3.s3_event22 = wizObjects.event22.initialValue;
        saveSlot3.s3_event23 = wizObjects.event23.initialValue;
        saveSlot3.s3_event24 = wizObjects.event24.initialValue;
        saveSlot3.s3_event25 = wizObjects.event25.initialValue;
        saveSlot3.s3_event26 = wizObjects.event26.initialValue;
        saveSlot3.s3_event27 = wizObjects.event27.initialValue;
        saveSlot3.s3_event28 = wizObjects.event28.initialValue;
        saveSlot3.s3_event29 = wizObjects.event29.initialValue;
        saveSlot3.s3_event30 = wizObjects.event30.initialValue;

        saveSlot3.s3_currentScene = wizObjects.currentScene.initialValue;
        saveSlot3.s3_previousScene = wizObjects.previousScene.initialValue;

        saveSlot3.s3_seenArea1 = wizObjects.seenArea1.initialValue;
        saveSlot3.s3_seenArea2 = wizObjects.seenArea2.initialValue;
        saveSlot3.s3_seenArea3 = wizObjects.seenArea3.initialValue;
        saveSlot3.s3_seenArea4 = wizObjects.seenArea4.initialValue;
        saveSlot3.s3_seenArea5 = wizObjects.seenArea5.initialValue;
        saveSlot3.s3_seenArea6 = wizObjects.seenArea6.initialValue;
        saveSlot3.s3_seenArea7 = wizObjects.seenArea7.initialValue;
        saveSlot3.s3_seenArea8 = wizObjects.seenArea8.initialValue;
        saveSlot3.s3_seenArea9 = wizObjects.seenArea9.initialValue;
        saveSlot3.s3_seenArea10 = wizObjects.seenArea10.initialValue;

        saveSlot3.s3_room1 = wizObjects.room1.initialValue;
        saveSlot3.s3_room2 = wizObjects.room2.initialValue;
        saveSlot3.s3_room3 = wizObjects.room3.initialValue;
        saveSlot3.s3_room4 = wizObjects.room4.initialValue;
        saveSlot3.s3_room5 = wizObjects.room5.initialValue;
        saveSlot3.s3_room6 = wizObjects.room6.initialValue;
        saveSlot3.s3_room7 = wizObjects.room7.initialValue;
        saveSlot3.s3_room8 = wizObjects.room8.initialValue;
        saveSlot3.s3_room9 = wizObjects.room9.initialValue;
        saveSlot3.s3_room10 = wizObjects.room10.initialValue;
        saveSlot3.s3_room11 = wizObjects.room11.initialValue;
        saveSlot3.s3_room12 = wizObjects.room12.initialValue;
        saveSlot3.s3_room13 = wizObjects.room13.initialValue;
        saveSlot3.s3_room14 = wizObjects.room14.initialValue;
        saveSlot3.s3_room15 = wizObjects.room15.initialValue;
        saveSlot3.s3_room16 = wizObjects.room16.initialValue;
        saveSlot3.s3_room17 = wizObjects.room17.initialValue;
        saveSlot3.s3_room18 = wizObjects.room18.initialValue;
        saveSlot3.s3_room19 = wizObjects.room19.initialValue;
        saveSlot3.s3_room20 = wizObjects.room20.initialValue;
        saveSlot3.s3_room21 = wizObjects.room21.initialValue;
        saveSlot3.s3_room22 = wizObjects.room22.initialValue;
        saveSlot3.s3_room23 = wizObjects.room23.initialValue;
        saveSlot3.s3_room24 = wizObjects.room24.initialValue;
        saveSlot3.s3_room25 = wizObjects.room25.initialValue;
        saveSlot3.s3_room26 = wizObjects.room26.initialValue;
        saveSlot3.s3_room27 = wizObjects.room27.initialValue;
        saveSlot3.s3_room28 = wizObjects.room28.initialValue;
        saveSlot3.s3_room29 = wizObjects.room29.initialValue;
        saveSlot3.s3_room30 = wizObjects.room30.initialValue;

         string dataPath = Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(Slot3));
        var stream = new FileStream(dataPath + "/"  + "save3.wiz", FileMode.Create);
        serializer.Serialize(stream, saveSlot3);
        stream.Close();
        autoSaveUI.DeactivateautoSaveUI();

    }

    #endregion

    #region LOAD

    public bool Load(){

        if(activeSlot.save_slot.initialValue == 1){
            return LoadSlot1();
        } else if(activeSlot.save_slot.initialValue == 2){
            return LoadSlot2();
        } else if(activeSlot.save_slot.initialValue == 3){
            return LoadSlot3();
        } else {
            return false;
        }
    }

    private bool LoadSlot1(){
                

        string dataPath = Application.persistentDataPath;

        if(System.IO.File.Exists(dataPath + "/" + "save1.wiz")){
            var serializer = new XmlSerializer(typeof(Slot1));
            var stream = new FileStream(dataPath + "/" + "save1.wiz", FileMode.Open);
            saveSlot1 = serializer.Deserialize(stream) as Slot1;
            stream.Close();

            wizObjects.savefile_timestamp.initialValue = saveSlot1.s1_timestamp;
            wizObjects.lastFirepitSceneName.initialValue = saveSlot1.s1_scene_name;
            wizObjects.wiz_x.initialValue = saveSlot1.s1_wiz_x;
            wizObjects.wiz_y.initialValue = saveSlot1.s1_wiz_y;
            wizObjects.wiz_facing_right.initialValue = saveSlot1.s1_facing_right;
            wizObjects.wiz_health.maxValue = saveSlot1.s1_wiz_max_health;
            wizObjects.wiz_health.initialValue = saveSlot1.s1_wiz_health;
            wizObjects.wiz_health.runTimeValue = saveSlot1.s1_wiz_health;
            wizObjects.wiz_has_magic.initialValue = saveSlot1.s1_has_magic;
            wizObjects.wiz_magic.initialValue = saveSlot1.s1_wiz_magic;
            wizObjects.wiz_attack3_powerup.initialValue = saveSlot1.s1_wiz_attack3;
            wizObjects.wiz_wall_jump_powerup.initialValue = saveSlot1.s1_wiz_wall_jump;
            wizObjects.wiz_has_wolfwrath.initialValue = saveSlot1.s1_has_wolfwrath;
            wizObjects.wiz_has_dashingsoul.initialValue = saveSlot1.s1_has_dashingsoul;
            wizObjects.wiz_has_dashinglight.initialValue = saveSlot1.s1_has_dashinglight;
            wizObjects.wiz_has_timecontrol.initialValue = saveSlot1.s1_has_timecontrol;
            wizObjects.wiz_has_fireball.initialValue = saveSlot1.s1_has_fireball;
            wizObjects.wiz_has_healthrecover.initialValue = saveSlot1.s1_has_healthrecover;
            wizObjects.wiz_gold.initialValue = saveSlot1.s1_wiz_gold;
            wizObjects.wiz_keys.initialValue = saveSlot1.s1_wiz_keys;
            wizObjects.wiz_emptyBottles.initialValue = saveSlot1.s1_wiz_emptyBottles;

            wizObjects.magic_slot1.initialValue = saveSlot1.s1_magic_slot1;
            wizObjects.magic_slot2.initialValue = saveSlot1.s1_magic_slot2;

            wizObjects.heart_container1.initialValue = saveSlot1.s1_heart_container1;
            wizObjects.heart_container2.initialValue = saveSlot1.s1_heart_container2;
            wizObjects.heart_container3.initialValue = saveSlot1.s1_heart_container3;
            wizObjects.heart_container4.initialValue = saveSlot1.s1_heart_container4;
            wizObjects.heart_container5.initialValue = saveSlot1.s1_heart_container5;
            wizObjects.heart_container6.initialValue = saveSlot1.s1_heart_container6;
            wizObjects.heart_container7.initialValue = saveSlot1.s1_heart_container7;
            wizObjects.heart_container8.initialValue = saveSlot1.s1_heart_container8;
            wizObjects.heart_container9.initialValue = saveSlot1.s1_heart_container9;
            wizObjects.heart_container10.initialValue = saveSlot1.s1_heart_container10;
            
            wizObjects.purified_statue1.initialValue = saveSlot1.s1_purified_statue1;
            wizObjects.purified_statue2.initialValue = saveSlot1.s1_purified_statue2;
            wizObjects.purified_statue3.initialValue = saveSlot1.s1_purified_statue3;
            wizObjects.purified_statue4.initialValue = saveSlot1.s1_purified_statue4;
            wizObjects.purified_statue5.initialValue = saveSlot1.s1_purified_statue5;

            wizObjects.gate1.initialValue = saveSlot1.s1_gate1;
            wizObjects.gate2.initialValue = saveSlot1.s1_gate2;
            wizObjects.gate3.initialValue = saveSlot1.s1_gate3;
            wizObjects.gate4.initialValue = saveSlot1.s1_gate4;
            wizObjects.gate5.initialValue = saveSlot1.s1_gate5;
            wizObjects.gate6.initialValue = saveSlot1.s1_gate6;
            wizObjects.gate7.initialValue = saveSlot1.s1_gate7;
            wizObjects.gate8.initialValue = saveSlot1.s1_gate8;
            wizObjects.gate9.initialValue = saveSlot1.s1_gate9;
            wizObjects.gate10.initialValue = saveSlot1.s1_gate10;

            wizObjects.hidden1.initialValue = saveSlot1.s1_hidden1;
            wizObjects.hidden2.initialValue = saveSlot1.s1_hidden2;
            wizObjects.hidden3.initialValue = saveSlot1.s1_hidden3;
            wizObjects.hidden4.initialValue = saveSlot1.s1_hidden4;
            wizObjects.hidden5.initialValue = saveSlot1.s1_hidden5;
            wizObjects.hidden6.initialValue = saveSlot1.s1_hidden6;
            wizObjects.hidden7.initialValue = saveSlot1.s1_hidden7;
            wizObjects.hidden8.initialValue = saveSlot1.s1_hidden8;
            wizObjects.hidden9.initialValue = saveSlot1.s1_hidden9;
            wizObjects.hidden10.initialValue = saveSlot1.s1_hidden10;

            wizObjects.boss1.initialValue = saveSlot1.s1_boss1;
            wizObjects.boss2.initialValue = saveSlot1.s1_boss2;
            wizObjects.boss3.initialValue = saveSlot1.s1_boss3;
            wizObjects.boss4.initialValue = saveSlot1.s1_boss4;
            wizObjects.boss5.initialValue = saveSlot1.s1_boss5;
            wizObjects.boss6.initialValue = saveSlot1.s1_boss6;
            wizObjects.boss7.initialValue = saveSlot1.s1_boss7;
            wizObjects.boss8.initialValue = saveSlot1.s1_boss8;
            wizObjects.boss9.initialValue = saveSlot1.s1_boss9;
            wizObjects.boss10.initialValue = saveSlot1.s1_boss10;

            wizObjects.event1.initialValue = saveSlot1.s1_event1;
            wizObjects.event2.initialValue = saveSlot1.s1_event2;
            wizObjects.event3.initialValue = saveSlot1.s1_event3;
            wizObjects.event4.initialValue = saveSlot1.s1_event4;
            wizObjects.event5.initialValue = saveSlot1.s1_event5;
            wizObjects.event6.initialValue = saveSlot1.s1_event6;
            wizObjects.event7.initialValue = saveSlot1.s1_event7;
            wizObjects.event8.initialValue = saveSlot1.s1_event8;
            wizObjects.event9.initialValue = saveSlot1.s1_event9;
            wizObjects.event10.initialValue = saveSlot1.s1_event10;
            wizObjects.event11.initialValue = saveSlot1.s1_event11;
            wizObjects.event12.initialValue = saveSlot1.s1_event12;
            wizObjects.event13.initialValue = saveSlot1.s1_event13;
            wizObjects.event14.initialValue = saveSlot1.s1_event14;
            wizObjects.event15.initialValue = saveSlot1.s1_event15;
            wizObjects.event16.initialValue = saveSlot1.s1_event16;
            wizObjects.event17.initialValue = saveSlot1.s1_event17;
            wizObjects.event18.initialValue = saveSlot1.s1_event18;
            wizObjects.event19.initialValue = saveSlot1.s1_event19;
            wizObjects.event20.initialValue = saveSlot1.s1_event20;
            wizObjects.event21.initialValue = saveSlot1.s1_event21;
            wizObjects.event22.initialValue = saveSlot1.s1_event22;
            wizObjects.event23.initialValue = saveSlot1.s1_event23;
            wizObjects.event24.initialValue = saveSlot1.s1_event24;
            wizObjects.event25.initialValue = saveSlot1.s1_event25;
            wizObjects.event26.initialValue = saveSlot1.s1_event26;
            wizObjects.event27.initialValue = saveSlot1.s1_event27;
            wizObjects.event28.initialValue = saveSlot1.s1_event28;
            wizObjects.event29.initialValue = saveSlot1.s1_event29;
            wizObjects.event30.initialValue = saveSlot1.s1_event30;

            wizObjects.currentScene.initialValue = saveSlot1.s1_currentScene;
            wizObjects.previousScene.initialValue = saveSlot1.s1_previousScene;

            wizObjects.seenArea1.initialValue = saveSlot1.s1_seenArea1;
            wizObjects.seenArea2.initialValue = saveSlot1.s1_seenArea2;
            wizObjects.seenArea3.initialValue = saveSlot1.s1_seenArea3;
            wizObjects.seenArea4.initialValue = saveSlot1.s1_seenArea4;
            wizObjects.seenArea5.initialValue = saveSlot1.s1_seenArea5;
            wizObjects.seenArea6.initialValue = saveSlot1.s1_seenArea6;
            wizObjects.seenArea7.initialValue = saveSlot1.s1_seenArea7;
            wizObjects.seenArea8.initialValue = saveSlot1.s1_seenArea8;
            wizObjects.seenArea9.initialValue = saveSlot1.s1_seenArea9;
            wizObjects.seenArea10.initialValue = saveSlot1.s1_seenArea10;


            wizObjects.room1.initialValue = saveSlot1.s1_room1;
            wizObjects.room2.initialValue = saveSlot1.s1_room2;
            wizObjects.room3.initialValue = saveSlot1.s1_room3;
            wizObjects.room4.initialValue = saveSlot1.s1_room4;
            wizObjects.room5.initialValue = saveSlot1.s1_room5;
            wizObjects.room6.initialValue = saveSlot1.s1_room6;
            wizObjects.room7.initialValue = saveSlot1.s1_room7;
            wizObjects.room8.initialValue = saveSlot1.s1_room8;
            wizObjects.room9.initialValue = saveSlot1.s1_room9;
            wizObjects.room10.initialValue = saveSlot1.s1_room10;
            wizObjects.room11.initialValue = saveSlot1.s1_room11;
            wizObjects.room12.initialValue = saveSlot1.s1_room12;
            wizObjects.room13.initialValue = saveSlot1.s1_room13;
            wizObjects.room14.initialValue = saveSlot1.s1_room14;
            wizObjects.room15.initialValue = saveSlot1.s1_room15;
            wizObjects.room16.initialValue = saveSlot1.s1_room16;
            wizObjects.room17.initialValue = saveSlot1.s1_room17;
            wizObjects.room18.initialValue = saveSlot1.s1_room18;
            wizObjects.room19.initialValue = saveSlot1.s1_room19;
            wizObjects.room20.initialValue = saveSlot1.s1_room20;
            wizObjects.room21.initialValue = saveSlot1.s1_room21;
            wizObjects.room22.initialValue = saveSlot1.s1_room22;
            wizObjects.room23.initialValue = saveSlot1.s1_room23;
            wizObjects.room24.initialValue = saveSlot1.s1_room24;
            wizObjects.room25.initialValue = saveSlot1.s1_room25;
            wizObjects.room26.initialValue = saveSlot1.s1_room26;
            wizObjects.room27.initialValue = saveSlot1.s1_room27;
            wizObjects.room28.initialValue = saveSlot1.s1_room28;
            wizObjects.room29.initialValue = saveSlot1.s1_room29;
            wizObjects.room30.initialValue = saveSlot1.s1_room30;




            return true;
        } else {

            SetWizObjectsToNewGame();

            return false;
        }

    }

    private bool LoadSlot2(){
                
        string dataPath = Application.persistentDataPath;

        if(System.IO.File.Exists(dataPath + "/" + "save2.wiz")){
            var serializer = new XmlSerializer(typeof(Slot2));
            var stream = new FileStream(dataPath + "/" + "save2.wiz", FileMode.Open);
            saveSlot2 = serializer.Deserialize(stream) as Slot2;
            stream.Close();

            wizObjects.savefile_timestamp.initialValue = saveSlot2.s2_timestamp;
            wizObjects.lastFirepitSceneName.initialValue = saveSlot2.s2_scene_name;
            wizObjects.wiz_x.initialValue = saveSlot2.s2_wiz_x;
            wizObjects.wiz_y.initialValue = saveSlot2.s2_wiz_y;
            wizObjects.wiz_facing_right.initialValue = saveSlot2.s2_facing_right;
            wizObjects.wiz_health.maxValue = saveSlot2.s2_wiz_max_health;
            wizObjects.wiz_health.initialValue = saveSlot2.s2_wiz_health;
            wizObjects.wiz_health.runTimeValue = saveSlot2.s2_wiz_health;
            wizObjects.wiz_has_magic.initialValue = saveSlot2.s2_has_magic;
            wizObjects.wiz_magic.initialValue = saveSlot2.s2_wiz_magic;
            wizObjects.wiz_attack3_powerup.initialValue = saveSlot2.s2_wiz_attack3;
            wizObjects.wiz_wall_jump_powerup.initialValue = saveSlot2.s2_wiz_wall_jump;
            wizObjects.wiz_has_wolfwrath.initialValue = saveSlot2.s2_has_wolfwrath;
            wizObjects.wiz_has_dashingsoul.initialValue = saveSlot2.s2_has_dashingsoul;
            wizObjects.wiz_has_dashinglight.initialValue = saveSlot2.s2_has_dashinglight;
            wizObjects.wiz_has_timecontrol.initialValue = saveSlot2.s2_has_timecontrol;
            wizObjects.wiz_has_fireball.initialValue = saveSlot2.s2_has_fireball;
            wizObjects.wiz_has_healthrecover.initialValue = saveSlot2.s2_has_healthrecover;
            wizObjects.wiz_gold.initialValue = saveSlot2.s2_wiz_gold;
            wizObjects.wiz_keys.initialValue = saveSlot2.s2_wiz_keys;
            wizObjects.wiz_emptyBottles.initialValue = saveSlot2.s2_wiz_emptyBottles;           

            wizObjects.magic_slot1.initialValue = saveSlot2.s2_magic_slot1;
            wizObjects.magic_slot2.initialValue = saveSlot2.s2_magic_slot2; 



            wizObjects.heart_container1.initialValue = saveSlot2.s2_heart_container1;
            wizObjects.heart_container2.initialValue = saveSlot2.s2_heart_container2;
            wizObjects.heart_container3.initialValue = saveSlot2.s2_heart_container3;
            wizObjects.heart_container4.initialValue = saveSlot2.s2_heart_container4;
            wizObjects.heart_container5.initialValue = saveSlot2.s2_heart_container5;
            wizObjects.heart_container6.initialValue = saveSlot2.s2_heart_container6;
            wizObjects.heart_container7.initialValue = saveSlot2.s2_heart_container7;
            wizObjects.heart_container8.initialValue = saveSlot2.s2_heart_container8;
            wizObjects.heart_container9.initialValue = saveSlot2.s2_heart_container9;
            wizObjects.heart_container10.initialValue = saveSlot2.s2_heart_container10;
            
            wizObjects.purified_statue1.initialValue = saveSlot2.s2_purified_statue1;
            wizObjects.purified_statue2.initialValue = saveSlot2.s2_purified_statue2;
            wizObjects.purified_statue3.initialValue = saveSlot2.s2_purified_statue3;
            wizObjects.purified_statue4.initialValue = saveSlot2.s2_purified_statue4;
            wizObjects.purified_statue5.initialValue = saveSlot2.s2_purified_statue5;

            wizObjects.gate1.initialValue = saveSlot2.s2_gate1;
            wizObjects.gate2.initialValue = saveSlot2.s2_gate2;
            wizObjects.gate3.initialValue = saveSlot2.s2_gate3;
            wizObjects.gate4.initialValue = saveSlot2.s2_gate4;
            wizObjects.gate5.initialValue = saveSlot2.s2_gate5;
            wizObjects.gate6.initialValue = saveSlot2.s2_gate6;
            wizObjects.gate7.initialValue = saveSlot2.s2_gate7;
            wizObjects.gate8.initialValue = saveSlot2.s2_gate8;
            wizObjects.gate9.initialValue = saveSlot2.s2_gate9;
            wizObjects.gate10.initialValue = saveSlot2.s2_gate10;

            wizObjects.hidden1.initialValue = saveSlot2.s2_hidden1;
            wizObjects.hidden2.initialValue = saveSlot2.s2_hidden2;
            wizObjects.hidden3.initialValue = saveSlot2.s2_hidden3;
            wizObjects.hidden4.initialValue = saveSlot2.s2_hidden4;
            wizObjects.hidden5.initialValue = saveSlot2.s2_hidden5;
            wizObjects.hidden6.initialValue = saveSlot2.s2_hidden6;
            wizObjects.hidden7.initialValue = saveSlot2.s2_hidden7;
            wizObjects.hidden8.initialValue = saveSlot2.s2_hidden8;
            wizObjects.hidden9.initialValue = saveSlot2.s2_hidden9;
            wizObjects.hidden10.initialValue = saveSlot2.s2_hidden10;

            wizObjects.boss1.initialValue = saveSlot2.s2_boss1;
            wizObjects.boss2.initialValue = saveSlot2.s2_boss2;
            wizObjects.boss3.initialValue = saveSlot2.s2_boss3;
            wizObjects.boss4.initialValue = saveSlot2.s2_boss4;
            wizObjects.boss5.initialValue = saveSlot2.s2_boss5;
            wizObjects.boss6.initialValue = saveSlot2.s2_boss6;
            wizObjects.boss7.initialValue = saveSlot2.s2_boss7;
            wizObjects.boss8.initialValue = saveSlot2.s2_boss8;
            wizObjects.boss9.initialValue = saveSlot2.s2_boss9;
            wizObjects.boss10.initialValue = saveSlot2.s2_boss10;

            wizObjects.event1.initialValue = saveSlot2.s2_event1;
            wizObjects.event2.initialValue = saveSlot2.s2_event2;
            wizObjects.event3.initialValue = saveSlot2.s2_event3;
            wizObjects.event4.initialValue = saveSlot2.s2_event4;
            wizObjects.event5.initialValue = saveSlot2.s2_event5;
            wizObjects.event6.initialValue = saveSlot2.s2_event6;
            wizObjects.event7.initialValue = saveSlot2.s2_event7;
            wizObjects.event8.initialValue = saveSlot2.s2_event8;
            wizObjects.event9.initialValue = saveSlot2.s2_event9;
            wizObjects.event10.initialValue = saveSlot2.s2_event10;
            wizObjects.event11.initialValue = saveSlot2.s2_event11;
            wizObjects.event12.initialValue = saveSlot2.s2_event12;
            wizObjects.event13.initialValue = saveSlot2.s2_event13;
            wizObjects.event14.initialValue = saveSlot2.s2_event14;
            wizObjects.event15.initialValue = saveSlot2.s2_event15;
            wizObjects.event16.initialValue = saveSlot2.s2_event16;
            wizObjects.event17.initialValue = saveSlot2.s2_event17;
            wizObjects.event18.initialValue = saveSlot2.s2_event18;
            wizObjects.event19.initialValue = saveSlot2.s2_event19;
            wizObjects.event20.initialValue = saveSlot2.s2_event20;
            wizObjects.event21.initialValue = saveSlot2.s2_event21;
            wizObjects.event22.initialValue = saveSlot2.s2_event22;
            wizObjects.event23.initialValue = saveSlot2.s2_event23;
            wizObjects.event24.initialValue = saveSlot2.s2_event24;
            wizObjects.event25.initialValue = saveSlot2.s2_event25;
            wizObjects.event26.initialValue = saveSlot2.s2_event26;
            wizObjects.event27.initialValue = saveSlot2.s2_event27;
            wizObjects.event28.initialValue = saveSlot2.s2_event28;
            wizObjects.event29.initialValue = saveSlot2.s2_event29;
            wizObjects.event30.initialValue = saveSlot2.s2_event30;

            wizObjects.currentScene.initialValue = saveSlot2.s2_currentScene;
            wizObjects.previousScene.initialValue = saveSlot2.s2_previousScene;

            wizObjects.seenArea1.initialValue = saveSlot2.s2_seenArea1;
            wizObjects.seenArea2.initialValue = saveSlot2.s2_seenArea2;
            wizObjects.seenArea3.initialValue = saveSlot2.s2_seenArea3;
            wizObjects.seenArea4.initialValue = saveSlot2.s2_seenArea4;
            wizObjects.seenArea5.initialValue = saveSlot2.s2_seenArea5;
            wizObjects.seenArea6.initialValue = saveSlot2.s2_seenArea6;
            wizObjects.seenArea7.initialValue = saveSlot2.s2_seenArea7;
            wizObjects.seenArea8.initialValue = saveSlot2.s2_seenArea8;
            wizObjects.seenArea9.initialValue = saveSlot2.s2_seenArea9;
            wizObjects.seenArea10.initialValue = saveSlot2.s2_seenArea10;


            wizObjects.room1.initialValue = saveSlot2.s2_room1;
            wizObjects.room2.initialValue = saveSlot2.s2_room2;
            wizObjects.room3.initialValue = saveSlot2.s2_room3;
            wizObjects.room4.initialValue = saveSlot2.s2_room4;
            wizObjects.room5.initialValue = saveSlot2.s2_room5;
            wizObjects.room6.initialValue = saveSlot2.s2_room6;
            wizObjects.room7.initialValue = saveSlot2.s2_room7;
            wizObjects.room8.initialValue = saveSlot2.s2_room8;
            wizObjects.room9.initialValue = saveSlot2.s2_room9;
            wizObjects.room10.initialValue = saveSlot2.s2_room10;
            wizObjects.room11.initialValue = saveSlot2.s2_room11;
            wizObjects.room12.initialValue = saveSlot2.s2_room12;
            wizObjects.room13.initialValue = saveSlot2.s2_room13;
            wizObjects.room14.initialValue = saveSlot2.s2_room14;
            wizObjects.room15.initialValue = saveSlot2.s2_room15;
            wizObjects.room16.initialValue = saveSlot2.s2_room16;
            wizObjects.room17.initialValue = saveSlot2.s2_room17;
            wizObjects.room18.initialValue = saveSlot2.s2_room18;
            wizObjects.room19.initialValue = saveSlot2.s2_room19;
            wizObjects.room20.initialValue = saveSlot2.s2_room20;
            wizObjects.room21.initialValue = saveSlot2.s2_room21;
            wizObjects.room22.initialValue = saveSlot2.s2_room22;
            wizObjects.room23.initialValue = saveSlot2.s2_room23;
            wizObjects.room24.initialValue = saveSlot2.s2_room24;
            wizObjects.room25.initialValue = saveSlot2.s2_room25;
            wizObjects.room26.initialValue = saveSlot2.s2_room26;
            wizObjects.room27.initialValue = saveSlot2.s2_room27;
            wizObjects.room28.initialValue = saveSlot2.s2_room28;
            wizObjects.room29.initialValue = saveSlot2.s2_room29;
            wizObjects.room30.initialValue = saveSlot2.s2_room30;

            

            return true;
        } else {

            SetWizObjectsToNewGame();

            return false;
        }

    }

    private bool LoadSlot3(){
                
        string dataPath = Application.persistentDataPath;

        if(System.IO.File.Exists(dataPath + "/" + "save3.wiz")){
            var serializer = new XmlSerializer(typeof(Slot3));
            var stream = new FileStream(dataPath + "/" + "save3.wiz", FileMode.Open);
            saveSlot3 = serializer.Deserialize(stream) as Slot3;
            stream.Close();

            wizObjects.savefile_timestamp.initialValue = saveSlot3.s3_timestamp;
            wizObjects.lastFirepitSceneName.initialValue = saveSlot3.s3_scene_name;
            wizObjects.wiz_x.initialValue = saveSlot3.s3_wiz_x;
            wizObjects.wiz_y.initialValue = saveSlot3.s3_wiz_y;
            wizObjects.wiz_facing_right.initialValue = saveSlot3.s3_facing_right;
            wizObjects.wiz_health.maxValue = saveSlot3.s3_wiz_max_health;
            wizObjects.wiz_health.initialValue = saveSlot3.s3_wiz_health;
            wizObjects.wiz_health.runTimeValue = saveSlot3.s3_wiz_health;
            wizObjects.wiz_has_magic.initialValue = saveSlot3.s3_has_magic;
            wizObjects.wiz_magic.initialValue = saveSlot3.s3_wiz_magic;
            wizObjects.wiz_attack3_powerup.initialValue = saveSlot3.s3_wiz_attack3;
            wizObjects.wiz_wall_jump_powerup.initialValue = saveSlot3.s3_wiz_wall_jump;
            wizObjects.wiz_has_wolfwrath.initialValue = saveSlot3.s3_has_wolfwrath;
            wizObjects.wiz_has_dashingsoul.initialValue = saveSlot3.s3_has_dashingsoul;
            wizObjects.wiz_has_dashinglight.initialValue = saveSlot3.s3_has_dashinglight;
            wizObjects.wiz_has_timecontrol.initialValue = saveSlot3.s3_has_timecontrol;
            wizObjects.wiz_has_fireball.initialValue = saveSlot3.s3_has_fireball;
            wizObjects.wiz_has_healthrecover.initialValue = saveSlot3.s3_has_healthrecover;
            wizObjects.wiz_gold.initialValue = saveSlot3.s3_wiz_gold;
            wizObjects.wiz_keys.initialValue = saveSlot3.s3_wiz_keys;
            wizObjects.wiz_emptyBottles.initialValue = saveSlot3.s3_wiz_emptyBottles;

            wizObjects.magic_slot1.initialValue = saveSlot3.s3_magic_slot1;
            wizObjects.magic_slot2.initialValue = saveSlot3.s3_magic_slot2;


            wizObjects.heart_container1.initialValue = saveSlot3.s3_heart_container1;
            wizObjects.heart_container2.initialValue = saveSlot3.s3_heart_container2;
            wizObjects.heart_container3.initialValue = saveSlot3.s3_heart_container3;
            wizObjects.heart_container4.initialValue = saveSlot3.s3_heart_container4;
            wizObjects.heart_container5.initialValue = saveSlot3.s3_heart_container5;
            wizObjects.heart_container6.initialValue = saveSlot3.s3_heart_container6;
            wizObjects.heart_container7.initialValue = saveSlot3.s3_heart_container7;
            wizObjects.heart_container8.initialValue = saveSlot3.s3_heart_container8;
            wizObjects.heart_container9.initialValue = saveSlot3.s3_heart_container9;
            wizObjects.heart_container10.initialValue = saveSlot3.s3_heart_container10;
            
            wizObjects.purified_statue1.initialValue = saveSlot3.s3_purified_statue1;
            wizObjects.purified_statue2.initialValue = saveSlot3.s3_purified_statue2;
            wizObjects.purified_statue3.initialValue = saveSlot3.s3_purified_statue3;
            wizObjects.purified_statue4.initialValue = saveSlot3.s3_purified_statue4;
            wizObjects.purified_statue5.initialValue = saveSlot3.s3_purified_statue5;

            wizObjects.gate1.initialValue = saveSlot3.s3_gate1;
            wizObjects.gate2.initialValue = saveSlot3.s3_gate2;
            wizObjects.gate3.initialValue = saveSlot3.s3_gate3;
            wizObjects.gate4.initialValue = saveSlot3.s3_gate4;
            wizObjects.gate5.initialValue = saveSlot3.s3_gate5;
            wizObjects.gate6.initialValue = saveSlot3.s3_gate6;
            wizObjects.gate7.initialValue = saveSlot3.s3_gate7;
            wizObjects.gate8.initialValue = saveSlot3.s3_gate8;
            wizObjects.gate9.initialValue = saveSlot3.s3_gate9;
            wizObjects.gate10.initialValue = saveSlot3.s3_gate10;

            wizObjects.hidden1.initialValue = saveSlot3.s3_hidden1;
            wizObjects.hidden2.initialValue = saveSlot3.s3_hidden2;
            wizObjects.hidden3.initialValue = saveSlot3.s3_hidden3;
            wizObjects.hidden4.initialValue = saveSlot3.s3_hidden4;
            wizObjects.hidden5.initialValue = saveSlot3.s3_hidden5;
            wizObjects.hidden6.initialValue = saveSlot3.s3_hidden6;
            wizObjects.hidden7.initialValue = saveSlot3.s3_hidden7;
            wizObjects.hidden8.initialValue = saveSlot3.s3_hidden8;
            wizObjects.hidden9.initialValue = saveSlot3.s3_hidden9;
            wizObjects.hidden10.initialValue = saveSlot3.s3_hidden10;

            wizObjects.boss1.initialValue = saveSlot3.s3_boss1;
            wizObjects.boss2.initialValue = saveSlot3.s3_boss2;
            wizObjects.boss3.initialValue = saveSlot3.s3_boss3;
            wizObjects.boss4.initialValue = saveSlot3.s3_boss4;
            wizObjects.boss5.initialValue = saveSlot3.s3_boss5;
            wizObjects.boss6.initialValue = saveSlot3.s3_boss6;
            wizObjects.boss7.initialValue = saveSlot3.s3_boss7;
            wizObjects.boss8.initialValue = saveSlot3.s3_boss8;
            wizObjects.boss9.initialValue = saveSlot3.s3_boss9;
            wizObjects.boss10.initialValue = saveSlot3.s3_boss10;

            wizObjects.event1.initialValue = saveSlot3.s3_event1;
            wizObjects.event2.initialValue = saveSlot3.s3_event2;
            wizObjects.event3.initialValue = saveSlot3.s3_event3;
            wizObjects.event4.initialValue = saveSlot3.s3_event4;
            wizObjects.event5.initialValue = saveSlot3.s3_event5;
            wizObjects.event6.initialValue = saveSlot3.s3_event6;
            wizObjects.event7.initialValue = saveSlot3.s3_event7;
            wizObjects.event8.initialValue = saveSlot3.s3_event8;
            wizObjects.event9.initialValue = saveSlot3.s3_event9;
            wizObjects.event10.initialValue = saveSlot3.s3_event10;
            wizObjects.event11.initialValue = saveSlot3.s3_event11;
            wizObjects.event12.initialValue = saveSlot3.s3_event12;
            wizObjects.event13.initialValue = saveSlot3.s3_event13;
            wizObjects.event14.initialValue = saveSlot3.s3_event14;
            wizObjects.event15.initialValue = saveSlot3.s3_event15;
            wizObjects.event16.initialValue = saveSlot3.s3_event16;
            wizObjects.event17.initialValue = saveSlot3.s3_event17;
            wizObjects.event18.initialValue = saveSlot3.s3_event18;
            wizObjects.event19.initialValue = saveSlot3.s3_event19;
            wizObjects.event20.initialValue = saveSlot3.s3_event20;
            wizObjects.event21.initialValue = saveSlot3.s3_event21;
            wizObjects.event22.initialValue = saveSlot3.s3_event22;
            wizObjects.event23.initialValue = saveSlot3.s3_event23;
            wizObjects.event24.initialValue = saveSlot3.s3_event24;
            wizObjects.event25.initialValue = saveSlot3.s3_event25;
            wizObjects.event26.initialValue = saveSlot3.s3_event26;
            wizObjects.event27.initialValue = saveSlot3.s3_event27;
            wizObjects.event28.initialValue = saveSlot3.s3_event28;
            wizObjects.event29.initialValue = saveSlot3.s3_event29;
            wizObjects.event30.initialValue = saveSlot3.s3_event30;

            wizObjects.currentScene.initialValue = saveSlot3.s3_currentScene;
            wizObjects.previousScene.initialValue = saveSlot3.s3_previousScene;

            wizObjects.seenArea1.initialValue = saveSlot3.s3_seenArea1;
            wizObjects.seenArea2.initialValue = saveSlot3.s3_seenArea2;
            wizObjects.seenArea3.initialValue = saveSlot3.s3_seenArea3;
            wizObjects.seenArea4.initialValue = saveSlot3.s3_seenArea4;
            wizObjects.seenArea5.initialValue = saveSlot3.s3_seenArea5;
            wizObjects.seenArea6.initialValue = saveSlot3.s3_seenArea6;
            wizObjects.seenArea7.initialValue = saveSlot3.s3_seenArea7;
            wizObjects.seenArea8.initialValue = saveSlot3.s3_seenArea8;
            wizObjects.seenArea9.initialValue = saveSlot3.s3_seenArea9;
            wizObjects.seenArea10.initialValue = saveSlot3.s3_seenArea10;


            wizObjects.room1.initialValue = saveSlot3.s3_room1;
            wizObjects.room2.initialValue = saveSlot3.s3_room2;
            wizObjects.room3.initialValue = saveSlot3.s3_room3;
            wizObjects.room4.initialValue = saveSlot3.s3_room4;
            wizObjects.room5.initialValue = saveSlot3.s3_room5;
            wizObjects.room6.initialValue = saveSlot3.s3_room6;
            wizObjects.room7.initialValue = saveSlot3.s3_room7;
            wizObjects.room8.initialValue = saveSlot3.s3_room8;
            wizObjects.room9.initialValue = saveSlot3.s3_room9;
            wizObjects.room10.initialValue = saveSlot3.s3_room10;
            wizObjects.room11.initialValue = saveSlot3.s3_room11;
            wizObjects.room12.initialValue = saveSlot3.s3_room12;
            wizObjects.room13.initialValue = saveSlot3.s3_room13;
            wizObjects.room14.initialValue = saveSlot3.s3_room14;
            wizObjects.room15.initialValue = saveSlot3.s3_room15;
            wizObjects.room16.initialValue = saveSlot3.s3_room16;
            wizObjects.room17.initialValue = saveSlot3.s3_room17;
            wizObjects.room18.initialValue = saveSlot3.s3_room18;
            wizObjects.room19.initialValue = saveSlot3.s3_room19;
            wizObjects.room20.initialValue = saveSlot3.s3_room20;
            wizObjects.room21.initialValue = saveSlot3.s3_room21;
            wizObjects.room22.initialValue = saveSlot3.s3_room22;
            wizObjects.room23.initialValue = saveSlot3.s3_room23;
            wizObjects.room24.initialValue = saveSlot3.s3_room24;
            wizObjects.room25.initialValue = saveSlot3.s3_room25;
            wizObjects.room26.initialValue = saveSlot3.s3_room26;
            wizObjects.room27.initialValue = saveSlot3.s3_room27;
            wizObjects.room28.initialValue = saveSlot3.s3_room28;
            wizObjects.room29.initialValue = saveSlot3.s3_room29;
            wizObjects.room30.initialValue = saveSlot3.s3_room30;


            return true;
        } else {

            SetWizObjectsToNewGame();

            return false;
        }

    }


    #endregion

    #region DELETE

    public bool Delete(){

        if(activeSlot.save_slot.initialValue == 1){
            return DeleteSlot1();
        } else if(activeSlot.save_slot.initialValue == 2){
            return DeleteSlot2();
        } else if(activeSlot.save_slot.initialValue == 3){
            return DeleteSlot3();
        } else {
            return false;
        }

    }

    public void DeleteAllSlots(){
        DeleteSlot1();
        DeleteSlot2();
        DeleteSlot3();
    }

    private bool DeleteSlot1(){

        string dataPath = Application.persistentDataPath;

        if(System.IO.File.Exists(dataPath + "/" + "save1.wiz")){
            saveSlot1.s1_saved_game = false;
            File.Delete(dataPath + "/" + "save1.wiz");
            return true;
        } else {
            return false;
        }
    }

    private bool DeleteSlot2(){

        string dataPath = Application.persistentDataPath;

        if(System.IO.File.Exists(dataPath + "/" + "save2.wiz")){
            saveSlot2.s2_saved_game = false;
            File.Delete(dataPath + "/" + "save2.wiz");
            return true;
        } else {
            return false;
        }
    }

    private bool DeleteSlot3(){

        string dataPath = Application.persistentDataPath;

        if(System.IO.File.Exists(dataPath + "/" + "save3.wiz")){
            saveSlot3.s3_saved_game = false;
            File.Delete(dataPath + "/" + "save3.wiz");
            return true;
        } else {
            return false;
        }
    }

    #endregion

    private void SetWizObjectsToNewGame(){
        wizObjects.savefile_timestamp.initialValue = 0f;
        wizObjects.lastFirepitSceneName.initialValue = "graveyard";
        wizObjects.currentScene.initialValue = "graveyard";
        wizObjects.previousScene.initialValue = "";
        wizObjects.wiz_x.initialValue = 0f;
        wizObjects.wiz_y.initialValue = 0f;
        wizObjects.wiz_facing_right.initialValue = true;
        wizObjects.wiz_health.maxValue = 5f;
        wizObjects.wiz_health.initialValue = 5f;
        wizObjects.wiz_health.runTimeValue = 5f;
        wizObjects.wiz_attack3_powerup.initialValue = false;
        wizObjects.wiz_magic.initialValue = 5;
        wizObjects.wiz_keys.initialValue = 0;
        wizObjects.wiz_gold.initialValue = 0;
        wizObjects.wiz_breads.initialValue = 0;
        wizObjects.wiz_emptyBottles.initialValue = 0;
        wizObjects.wiz_health_potions.initialValue = 0;
        wizObjects.wiz_bonus_potions.initialValue = 0;

        wizObjects.magic_slot1.initialValue = 5;
        wizObjects.magic_slot2.initialValue = 6;

        // forest
        wizObjects.wiz_wall_jump_powerup.initialValue = true;
        wizObjects.wiz_has_magic.initialValue = true;
        wizObjects.wiz_has_wolfwrath.initialValue = true;
        wizObjects.wiz_has_dashingsoul.initialValue = true;
        wizObjects.wiz_has_dashinglight.initialValue = true;
        wizObjects.wiz_has_timecontrol.initialValue = true;
        wizObjects.wiz_has_fireball.initialValue = true;
        wizObjects.wiz_has_healthrecover.initialValue = true;

        wizObjects.purified_statue1.initialValue = false;
        wizObjects.purified_statue2.initialValue = false;
        wizObjects.purified_statue3.initialValue = false;
        wizObjects.purified_statue4.initialValue = false;
        wizObjects.purified_statue5.initialValue = false;

        wizObjects.heart_container1.initialValue = false;
        wizObjects.heart_container2.initialValue = false;
        wizObjects.heart_container3.initialValue = false;
        wizObjects.heart_container4.initialValue = false;
        wizObjects.heart_container5.initialValue = false;
        wizObjects.heart_container6.initialValue = false;
        wizObjects.heart_container7.initialValue = false;
        wizObjects.heart_container8.initialValue = false;
        wizObjects.heart_container9.initialValue = false;
        wizObjects.heart_container10.initialValue = false;

        wizObjects.gate1.initialValue = false;
        wizObjects.gate2.initialValue = false;
        wizObjects.gate3.initialValue = false;
        wizObjects.gate4.initialValue = false;
        wizObjects.gate5.initialValue = false;
        wizObjects.gate6.initialValue = false;
        wizObjects.gate7.initialValue = false;
        wizObjects.gate8.initialValue = false;
        wizObjects.gate9.initialValue = false;
        wizObjects.gate10.initialValue = false;

        wizObjects.hidden1.initialValue = false;
        wizObjects.hidden2.initialValue = false;
        wizObjects.hidden3.initialValue = false;
        wizObjects.hidden4.initialValue = false;
        wizObjects.hidden5.initialValue = false;
        wizObjects.hidden6.initialValue = false;
        wizObjects.hidden7.initialValue = false;
        wizObjects.hidden8.initialValue = false;
        wizObjects.hidden9.initialValue = false;
        wizObjects.hidden10.initialValue = false;
        
        wizObjects.boss1.initialValue = false;
        wizObjects.boss2.initialValue = false;
        wizObjects.boss3.initialValue = false;
        wizObjects.boss4.initialValue = false;
        wizObjects.boss5.initialValue = false;
        wizObjects.boss6.initialValue = false;
        wizObjects.boss7.initialValue = false;
        wizObjects.boss8.initialValue = false;
        wizObjects.boss9.initialValue = false;
        wizObjects.boss10.initialValue = false;

        wizObjects.event1.initialValue = false;
        wizObjects.event2.initialValue = false;
        wizObjects.event3.initialValue = false;
        wizObjects.event4.initialValue = false;
        wizObjects.event5.initialValue = false;
        wizObjects.event6.initialValue = false;
        wizObjects.event7.initialValue = false;
        wizObjects.event8.initialValue = false;
        wizObjects.event9.initialValue = false;
        wizObjects.event10.initialValue = false;
        wizObjects.event11.initialValue = false;
        wizObjects.event12.initialValue = false;
        wizObjects.event13.initialValue = false;
        wizObjects.event14.initialValue = false;
        wizObjects.event15.initialValue = false;
        wizObjects.event16.initialValue = false;
        wizObjects.event17.initialValue = false;
        wizObjects.event18.initialValue = false;
        wizObjects.event19.initialValue = false;
        wizObjects.event20.initialValue = false;
        wizObjects.event21.initialValue = false;
        wizObjects.event22.initialValue = false;
        wizObjects.event23.initialValue = false;
        wizObjects.event24.initialValue = false;
        wizObjects.event25.initialValue = false;
        wizObjects.event26.initialValue = false;
        wizObjects.event27.initialValue = false;
        wizObjects.event28.initialValue = false;
        wizObjects.event29.initialValue = false;
        wizObjects.event30.initialValue = false;

        wizObjects.seenArea1.initialValue = false;
        wizObjects.seenArea2.initialValue = false;
        wizObjects.seenArea3.initialValue = false;
        wizObjects.seenArea4.initialValue = false;
        wizObjects.seenArea5.initialValue = false;
        wizObjects.seenArea6.initialValue = false;
        wizObjects.seenArea7.initialValue = false;
        wizObjects.seenArea8.initialValue = false;
        wizObjects.seenArea9.initialValue = false;
        wizObjects.seenArea10.initialValue = false;

        wizObjects.room1.initialValue = false;
        wizObjects.room2.initialValue = false;
        wizObjects.room3.initialValue = false;
        wizObjects.room4.initialValue = false;
        wizObjects.room5.initialValue = false;
        wizObjects.room6.initialValue = false;
        wizObjects.room7.initialValue = false;
        wizObjects.room8.initialValue = false;
        wizObjects.room9.initialValue = false;
        wizObjects.room10.initialValue = false;
        wizObjects.room11.initialValue = false;
        wizObjects.room12.initialValue = false;
        wizObjects.room13.initialValue = false;
        wizObjects.room14.initialValue = false;
        wizObjects.room15.initialValue = false;
        wizObjects.room16.initialValue = false;
        wizObjects.room17.initialValue = false;
        wizObjects.room18.initialValue = false;
        wizObjects.room19.initialValue = false;
        wizObjects.room20.initialValue = false;
        wizObjects.room21.initialValue = false;
        wizObjects.room22.initialValue = false;
        wizObjects.room23.initialValue = false;
        wizObjects.room24.initialValue = false;
        wizObjects.room25.initialValue = false;
        wizObjects.room26.initialValue = false;
        wizObjects.room27.initialValue = false;
        wizObjects.room28.initialValue = false;
        wizObjects.room29.initialValue = false;
        wizObjects.room30.initialValue = false;

    }

}
// ------------------------------------------
// ----------- END OF MAIN CLASS ------------
// ------------------------------------------


[System.Serializable]
public class ActiveSlot {
    public IntValue save_slot;
    public BoolValue loading_save;
    public BoolValue _this_slot_has_been_saved;
}

// ------------------------------------------

[System.Serializable]
public class Slot1 {

    public float s1_timestamp;

    public int  s1_wiz_keys;
    public int  s1_wiz_gold;
    public int  s1_wiz_breads;
    public int s1_wiz_emptyBottles;
    public int  s1_wiz_health_potions;
    public int  s1_wiz_bonus_potions;
    public float s1_wiz_x;
    public float s1_wiz_y;
    public bool s1_facing_right;
    public bool s1_saved_game;
    public string s1_scene_name;
    public float s1_wiz_health;
    public float s1_wiz_max_health;
    public bool s1_has_magic;
    public int s1_wiz_magic;

    public int s1_magic_slot1;
    public int s1_magic_slot2;

    // powerups
    public bool s1_wiz_attack3;
    public bool s1_wiz_wall_jump;
    public bool s1_has_wolfwrath;
    public bool s1_has_dashingsoul;
    public bool s1_has_dashinglight;
    public bool s1_has_timecontrol;
    public bool s1_has_fireball;
    public bool s1_has_healthrecover;


    public bool s1_heart_container1;
    public bool s1_heart_container2;
    public bool s1_heart_container3;
    public bool s1_heart_container4;
    public bool s1_heart_container5;
    public bool s1_heart_container6;
    public bool s1_heart_container7;
    public bool s1_heart_container8;
    public bool s1_heart_container9;
    public bool s1_heart_container10;

    public bool s1_purified_statue1;
    public bool s1_purified_statue2;
    public bool s1_purified_statue3;
    public bool s1_purified_statue4;
    public bool s1_purified_statue5;

    public bool s1_gate1;
    public bool s1_gate2;
    public bool s1_gate3;
    public bool s1_gate4;
    public bool s1_gate5;
    public bool s1_gate6;
    public bool s1_gate7;
    public bool s1_gate8;
    public bool s1_gate9;
    public bool s1_gate10;

    public bool s1_hidden1;
    public bool s1_hidden2;
    public bool s1_hidden3;
    public bool s1_hidden4;
    public bool s1_hidden5;
    public bool s1_hidden6;
    public bool s1_hidden7;
    public bool s1_hidden8;
    public bool s1_hidden9;
    public bool s1_hidden10;

    public bool s1_boss1;
    public bool s1_boss2;
    public bool s1_boss3;
    public bool s1_boss4;
    public bool s1_boss5;
    public bool s1_boss6;
    public bool s1_boss7;
    public bool s1_boss8;
    public bool s1_boss9;
    public bool s1_boss10;

    public bool s1_event1;
    public bool s1_event2;
    public bool s1_event3;
    public bool s1_event4;
    public bool s1_event5;
    public bool s1_event6;
    public bool s1_event7;
    public bool s1_event8;
    public bool s1_event9;
    public bool s1_event10;
    public bool s1_event11;
    public bool s1_event12;
    public bool s1_event13;
    public bool s1_event14;
    public bool s1_event15;
    public bool s1_event16;
    public bool s1_event17;
    public bool s1_event18;
    public bool s1_event19;
    public bool s1_event20;
    public bool s1_event21;
    public bool s1_event22;
    public bool s1_event23;
    public bool s1_event24;
    public bool s1_event25;
    public bool s1_event26;
    public bool s1_event27;
    public bool s1_event28;
    public bool s1_event29;
    public bool s1_event30;

    public string s1_currentScene;
    public string s1_previousScene;

    public bool s1_seenArea1;
    public bool s1_seenArea2;
    public bool s1_seenArea3;
    public bool s1_seenArea4;
    public bool s1_seenArea5;
    public bool s1_seenArea6;
    public bool s1_seenArea7;
    public bool s1_seenArea8;
    public bool s1_seenArea9;
    public bool s1_seenArea10;

    public bool s1_room1;
    public bool s1_room2;
    public bool s1_room3;
    public bool s1_room4;
    public bool s1_room5;
    public bool s1_room6;
    public bool s1_room7;
    public bool s1_room8;
    public bool s1_room9;
    public bool s1_room10;
    public bool s1_room11;
    public bool s1_room12;
    public bool s1_room13;
    public bool s1_room14;
    public bool s1_room15;
    public bool s1_room16;
    public bool s1_room17;
    public bool s1_room18;
    public bool s1_room19;
    public bool s1_room20;
    public bool s1_room21;
    public bool s1_room22;
    public bool s1_room23;
    public bool s1_room24;
    public bool s1_room25;
    public bool s1_room26;
    public bool s1_room27;
    public bool s1_room28;
    public bool s1_room29;
    public bool s1_room30;

}

// ------------------------------------------

[System.Serializable]
public class Slot2 {

    public float s2_timestamp;

    public int  s2_wiz_keys;
    public int  s2_wiz_gold;
    public int  s2_wiz_breads;
    public int s2_wiz_emptyBottles;
    public int  s2_wiz_health_potions;
    public int  s2_wiz_bonus_potions;
    public float s2_wiz_x;
    public float s2_wiz_y;
    public bool s2_facing_right;
    public bool s2_saved_game;
    public string s2_scene_name;
    public float s2_wiz_health;
    public float s2_wiz_max_health;
    public bool s2_has_magic;
    public int s2_wiz_magic;

    public int s2_magic_slot1;
    public int s2_magic_slot2;

    // powerups
    public bool s2_wiz_attack3;
    public bool s2_wiz_wall_jump;
    public bool s2_has_wolfwrath;
    public bool s2_has_dashingsoul;
    public bool s2_has_dashinglight;
    public bool s2_has_timecontrol;
    public bool s2_has_fireball;
    public bool s2_has_healthrecover;


    public bool s2_heart_container1;
    public bool s2_heart_container2;
    public bool s2_heart_container3;
    public bool s2_heart_container4;
    public bool s2_heart_container5;
    public bool s2_heart_container6;
    public bool s2_heart_container7;
    public bool s2_heart_container8;
    public bool s2_heart_container9;
    public bool s2_heart_container10;

    public bool s2_purified_statue1;
    public bool s2_purified_statue2;
    public bool s2_purified_statue3;
    public bool s2_purified_statue4;
    public bool s2_purified_statue5;

    public bool s2_gate1;
    public bool s2_gate2;
    public bool s2_gate3;
    public bool s2_gate4;
    public bool s2_gate5;
    public bool s2_gate6;
    public bool s2_gate7;
    public bool s2_gate8;
    public bool s2_gate9;
    public bool s2_gate10;

    public bool s2_hidden1;
    public bool s2_hidden2;
    public bool s2_hidden3;
    public bool s2_hidden4;
    public bool s2_hidden5;
    public bool s2_hidden6;
    public bool s2_hidden7;
    public bool s2_hidden8;
    public bool s2_hidden9;
    public bool s2_hidden10;

    public bool s2_boss1;
    public bool s2_boss2;
    public bool s2_boss3;
    public bool s2_boss4;
    public bool s2_boss5;
    public bool s2_boss6;
    public bool s2_boss7;
    public bool s2_boss8;
    public bool s2_boss9;
    public bool s2_boss10;

    public bool s2_event1;
    public bool s2_event2;
    public bool s2_event3;
    public bool s2_event4;
    public bool s2_event5;
    public bool s2_event6;
    public bool s2_event7;
    public bool s2_event8;
    public bool s2_event9;
    public bool s2_event10;
    public bool s2_event11;
    public bool s2_event12;
    public bool s2_event13;
    public bool s2_event14;
    public bool s2_event15;
    public bool s2_event16;
    public bool s2_event17;
    public bool s2_event18;
    public bool s2_event19;
    public bool s2_event20;
    public bool s2_event21;
    public bool s2_event22;
    public bool s2_event23;
    public bool s2_event24;
    public bool s2_event25;
    public bool s2_event26;
    public bool s2_event27;
    public bool s2_event28;
    public bool s2_event29;
    public bool s2_event30;

    public string s2_currentScene;
    public string s2_previousScene;

    public bool s2_seenArea1;
    public bool s2_seenArea2;
    public bool s2_seenArea3;
    public bool s2_seenArea4;
    public bool s2_seenArea5;
    public bool s2_seenArea6;
    public bool s2_seenArea7;
    public bool s2_seenArea8;
    public bool s2_seenArea9;
    public bool s2_seenArea10;

    public bool s2_room1;
    public bool s2_room2;
    public bool s2_room3;
    public bool s2_room4;
    public bool s2_room5;
    public bool s2_room6;
    public bool s2_room7;
    public bool s2_room8;
    public bool s2_room9;
    public bool s2_room10;
    public bool s2_room11;
    public bool s2_room12;
    public bool s2_room13;
    public bool s2_room14;
    public bool s2_room15;
    public bool s2_room16;
    public bool s2_room17;
    public bool s2_room18;
    public bool s2_room19;
    public bool s2_room20;
    public bool s2_room21;
    public bool s2_room22;
    public bool s2_room23;
    public bool s2_room24;
    public bool s2_room25;
    public bool s2_room26;
    public bool s2_room27;
    public bool s2_room28;
    public bool s2_room29;
    public bool s2_room30;

}

// ------------------------------------------

[System.Serializable]
public class Slot3 {

    public float s3_timestamp;

    public int  s3_wiz_keys;
    public int  s3_wiz_gold;
    public int  s3_wiz_breads;
    public int s3_wiz_emptyBottles;
    public int  s3_wiz_health_potions;
    public int  s3_wiz_bonus_potions;
    public float s3_wiz_x;
    public float s3_wiz_y;
    public bool s3_facing_right;
    public bool s3_saved_game;
    public string s3_scene_name;
    public float s3_wiz_health;
    public float s3_wiz_max_health;
    public bool s3_has_magic;
    public int s3_wiz_magic;

    public int s3_magic_slot1;
    public int s3_magic_slot2;

    // powerups
    public bool s3_wiz_attack3;
    public bool s3_wiz_wall_jump;
    public bool s3_has_wolfwrath;
    public bool s3_has_dashingsoul;
    public bool s3_has_dashinglight;
    public bool s3_has_timecontrol;
    public bool s3_has_fireball;
    public bool s3_has_healthrecover;


    public bool s3_heart_container1;
    public bool s3_heart_container2;
    public bool s3_heart_container3;
    public bool s3_heart_container4;
    public bool s3_heart_container5;
    public bool s3_heart_container6;
    public bool s3_heart_container7;
    public bool s3_heart_container8;
    public bool s3_heart_container9;
    public bool s3_heart_container10;

    public bool s3_purified_statue1;
    public bool s3_purified_statue2;
    public bool s3_purified_statue3;
    public bool s3_purified_statue4;
    public bool s3_purified_statue5;

    public bool s3_gate1;
    public bool s3_gate2;
    public bool s3_gate3;
    public bool s3_gate4;
    public bool s3_gate5;
    public bool s3_gate6;
    public bool s3_gate7;
    public bool s3_gate8;
    public bool s3_gate9;
    public bool s3_gate10;

    public bool s3_hidden1;
    public bool s3_hidden2;
    public bool s3_hidden3;
    public bool s3_hidden4;
    public bool s3_hidden5;
    public bool s3_hidden6;
    public bool s3_hidden7;
    public bool s3_hidden8;
    public bool s3_hidden9;
    public bool s3_hidden10;

    public bool s3_boss1;
    public bool s3_boss2;
    public bool s3_boss3;
    public bool s3_boss4;
    public bool s3_boss5;
    public bool s3_boss6;
    public bool s3_boss7;
    public bool s3_boss8;
    public bool s3_boss9;
    public bool s3_boss10;

    public bool s3_event1;
    public bool s3_event2;
    public bool s3_event3;
    public bool s3_event4;
    public bool s3_event5;
    public bool s3_event6;
    public bool s3_event7;
    public bool s3_event8;
    public bool s3_event9;
    public bool s3_event10;
    public bool s3_event11;
    public bool s3_event12;
    public bool s3_event13;
    public bool s3_event14;
    public bool s3_event15;
    public bool s3_event16;
    public bool s3_event17;
    public bool s3_event18;
    public bool s3_event19;
    public bool s3_event20;
    public bool s3_event21;
    public bool s3_event22;
    public bool s3_event23;
    public bool s3_event24;
    public bool s3_event25;
    public bool s3_event26;
    public bool s3_event27;
    public bool s3_event28;
    public bool s3_event29;
    public bool s3_event30;

    public string s3_currentScene;
    public string s3_previousScene;

    public bool s3_seenArea1;
    public bool s3_seenArea2;
    public bool s3_seenArea3;
    public bool s3_seenArea4;
    public bool s3_seenArea5;
    public bool s3_seenArea6;
    public bool s3_seenArea7;
    public bool s3_seenArea8;
    public bool s3_seenArea9;
    public bool s3_seenArea10;

    public bool s3_room1;
    public bool s3_room2;
    public bool s3_room3;
    public bool s3_room4;
    public bool s3_room5;
    public bool s3_room6;
    public bool s3_room7;
    public bool s3_room8;
    public bool s3_room9;
    public bool s3_room10;
    public bool s3_room11;
    public bool s3_room12;
    public bool s3_room13;
    public bool s3_room14;
    public bool s3_room15;
    public bool s3_room16;
    public bool s3_room17;
    public bool s3_room18;
    public bool s3_room19;
    public bool s3_room20;
    public bool s3_room21;
    public bool s3_room22;
    public bool s3_room23;
    public bool s3_room24;
    public bool s3_room25;
    public bool s3_room26;
    public bool s3_room27;
    public bool s3_room28;
    public bool s3_room29;
    public bool s3_room30;

}

// ------------------------------------------

[System.Serializable]
public class WizObjects {

    public FloatValue savefile_timestamp;
    public FloatValue wiz_x; //
    public FloatValue wiz_y; //
    public BoolValue wiz_facing_right; //
    public FloatValue wiz_health; //
    public FloatValue wiz_health_yellow; //
    public StringValue lastFirepitSceneName; //
    // powerups / items ------------------------------------------
    public BoolValue wiz_has_magic; //
    public IntValue wiz_magic; //
    public IntValue wiz_keys;
    public IntValue wiz_gold;
    public IntValue wiz_breads;
    public IntValue wiz_emptyBottles;
    public IntValue wiz_health_potions;
    public IntValue wiz_bonus_potions;


    public BoolValue wiz_attack3_powerup; //
    public BoolValue wiz_wall_jump_powerup; //
    public BoolValue wiz_has_wolfwrath; //
    public BoolValue wiz_has_dashingsoul; //
    public BoolValue wiz_has_dashinglight; 
    public BoolValue wiz_has_timecontrol; 
    public BoolValue wiz_has_fireball; 
    public BoolValue wiz_has_healthrecover; 


    public IntValue magic_slot1;
    public IntValue magic_slot2;
    // world events ----------------------------------------------

    public BoolValue heart_container1; //
    public BoolValue heart_container2; //
    public BoolValue heart_container3;
    public BoolValue heart_container4;
    public BoolValue heart_container5;
    public BoolValue heart_container6;
    public BoolValue heart_container7;
    public BoolValue heart_container8;
    public BoolValue heart_container9;
    public BoolValue heart_container10;

    public BoolValue purified_statue1;
    public BoolValue purified_statue2;
    public BoolValue purified_statue3;
    public BoolValue purified_statue4;
    public BoolValue purified_statue5;

    public BoolValue gate1;
    public BoolValue gate2;
    public BoolValue gate3;
    public BoolValue gate4;
    public BoolValue gate5;
    public BoolValue gate6;
    public BoolValue gate7;
    public BoolValue gate8;
    public BoolValue gate9;
    public BoolValue gate10;
    
    public BoolValue hidden1;
    public BoolValue hidden2;
    public BoolValue hidden3;
    public BoolValue hidden4;
    public BoolValue hidden5;
    public BoolValue hidden6;
    public BoolValue hidden7;
    public BoolValue hidden8;
    public BoolValue hidden9;
    public BoolValue hidden10;


    public BoolValue boss1;
    public BoolValue boss2;
    public BoolValue boss3;
    public BoolValue boss4;
    public BoolValue boss5;
    public BoolValue boss6;
    public BoolValue boss7;
    public BoolValue boss8;
    public BoolValue boss9;
    public BoolValue boss10;

    public BoolValue event1;
    public BoolValue event2;
    public BoolValue event3;
    public BoolValue event4;
    public BoolValue event5;
    public BoolValue event6;
    public BoolValue event7;
    public BoolValue event8;
    public BoolValue event9;
    public BoolValue event10;
    public BoolValue event11;
    public BoolValue event12;
    public BoolValue event13;
    public BoolValue event14;
    public BoolValue event15;
    public BoolValue event16;
    public BoolValue event17;
    public BoolValue event18;
    public BoolValue event19;
    public BoolValue event20;
    public BoolValue event21;
    public BoolValue event22;
    public BoolValue event23;
    public BoolValue event24;
    public BoolValue event25;
    public BoolValue event26;
    public BoolValue event27;
    public BoolValue event28;
    public BoolValue event29;
    public BoolValue event30;

    // MAP ------------------------------
    public StringValue currentScene;
    public StringValue previousScene;

    public BoolValue seenArea1; // MAJOR AREAS
    public BoolValue seenArea2;
    public BoolValue seenArea3;
    public BoolValue seenArea4;
    public BoolValue seenArea5;
    public BoolValue seenArea6;
    public BoolValue seenArea7;
    public BoolValue seenArea8;
    public BoolValue seenArea9;
    public BoolValue seenArea10;

    public BoolValue room1; // Individual scenes -> Use both runtime value and initial value _ SAVE ONLY INITIAL VALUE
    public BoolValue room2;
    public BoolValue room3;
    public BoolValue room4;
    public BoolValue room5;
    public BoolValue room6;
    public BoolValue room7;
    public BoolValue room8;
    public BoolValue room9;
    public BoolValue room10;
    public BoolValue room11;
    public BoolValue room12;
    public BoolValue room13;
    public BoolValue room14;
    public BoolValue room15;
    public BoolValue room16;
    public BoolValue room17;
    public BoolValue room18;
    public BoolValue room19;
    public BoolValue room20;
    public BoolValue room21;
    public BoolValue room22;
    public BoolValue room23;
    public BoolValue room24;
    public BoolValue room25;
    public BoolValue room26;
    public BoolValue room27;
    public BoolValue room28;
    public BoolValue room29;
    public BoolValue room30;


}

