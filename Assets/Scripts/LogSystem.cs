using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class LogSystem : MonoBehaviour {

    public static LogSystem Instance;
    private string filePath;
    private bool _flag = false;

    void Start() {
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } else { 
            Instance = this; 
        }
        
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        filePath = Path.Combine(desktopPath, "wiz_log.txt");

        if (!File.Exists(filePath)) {
            InitializeLog();
        }
    }

    void Update(){
        if(!_flag && PlayerController.Instance.AnimatorIsPlaying("lay")){
            _flag = true;
            // InitializeLog();
        }
    }
    public void InitializeLog() {
        print("Iniciando log...");
        string initialText = "WIZ LOG SYSTEM - Please email this file and the `heatmap.png` to: raul@wolfbit.games";

        // Formatando data e hora no padrão brasileiro
        string timeAndDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

        // Obtendo a versão do sistema operacional
        string osVersion = SystemInfo.operatingSystem;

        string sessionInfo = $"Playsession started at {timeAndDate} on {osVersion}";
        
        File.WriteAllText(filePath, initialText + "\n" + sessionInfo + "\n" + "+------------------------------------------------------------------------------------+");
    }

    public void AppendLog(string logMessage) {
        File.AppendAllText(filePath, "\n" + logMessage);
    }

    public void ClearLog() {
        InitializeLog();
    }

    public bool CheckLogForEntry(string entryName) {
        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines) {
            if (line.Contains(entryName)) {
                return true;
            }
        }
        return false;
    }
}