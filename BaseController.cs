using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.persistentDataPath + "/save.xml";
        Debug.Log(path);
        if(System.IO.File.Exists(path))
        {
            Load();
        }
        DataVariables.characterPoint += 1000;
        Debug.Log(DataVariables.characterPoint);
        InvokeRepeating("InvokeSave",1f,2f);
    }

    // Update is called once per frame
    public void Save()
    {
        SaveData saveData = new SaveData();
        
        saveData.Sv_characterPoint = DataVariables.characterPoint;
        saveData.Sv_isVibrateOn = DataVariables.isVibrateOn;
        
        
        string path = Application.persistentDataPath + "/save.xml";
        XmlManager.XmlSave<SaveData>(saveData,path);
    }
    public void Load()
    {
        SaveData saveData = new SaveData();

        string path = Application.persistentDataPath + "/save.xml";
        saveData = XmlManager.XmlLoad<SaveData>(path);

        DataVariables.characterPoint = saveData.Sv_characterPoint;
        DataVariables.isVibrateOn = saveData.Sv_isVibrateOn;
        DataVariables.characterPoint = 0;
    }
    private void OnApplicationQuit() {      
        Save();
    }
    void InvokeSave()
    {
        Save();
    }
    public void SaveQuitGame()
    {
        Save();
        Application.Quit(); 
    }
}
