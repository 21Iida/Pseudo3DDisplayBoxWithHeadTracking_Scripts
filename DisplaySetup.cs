using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hiroki;
using System.IO;
using TMPro;

public class DisplaySetup : MonoBehaviour
{
    string filePath;
    DisplayData displayData = new DisplayData();
    [SerializeField] Camera camFront, camLeft, camRight, camBack, camTop;
    int palenMax = 5;

    private void Awake() {
        filePath = Application.persistentDataPath + "/" + ".displaySave.json";
    }
    void Start()
    {
        displayData.frontPanel = 0;
        displayData.leftPanel = 1;
        displayData.rightPanel = 2;
        displayData.backPanel = 3;
        displayData.topPanel = 4;
        
        Load();
    }
    public void FontChange(TextMeshProUGUI text)
    {
        displayData.frontPanel++;
        displayData.frontPanel = (int)Mathf.Repeat(displayData.frontPanel, palenMax);
        camSet();
        text.text = "Front" + displayData.frontPanel;
    }
    public void LeftChange(TextMeshProUGUI text)
    {
        displayData.leftPanel++;
        displayData.leftPanel = (int)Mathf.Repeat(displayData.leftPanel, palenMax);
        camSet();
        text.text = "Left" + displayData.leftPanel;
    }
    public void RightChange(TextMeshProUGUI text)
    {
        displayData.rightPanel++;
        displayData.rightPanel = (int)Mathf.Repeat(displayData.rightPanel, palenMax);
        camSet();
        text.text = "Right" + displayData.rightPanel;
    }
    public void BackChange(TextMeshProUGUI text)
    {
        displayData.backPanel++;
        displayData.backPanel = (int)Mathf.Repeat(displayData.backPanel, palenMax);
        camSet();
        text.text = "Back" + displayData.backPanel;
    }
    public void TopChange(TextMeshProUGUI text)
    {
        displayData.topPanel++;
        displayData.topPanel = (int)Mathf.Repeat(displayData.topPanel, palenMax);
        camSet();
        text.text = "Top" + displayData.topPanel;
    }
    
    void camSet()
    {
        camFront.targetDisplay = displayData.frontPanel;
        camLeft.targetDisplay = displayData.leftPanel;
        camRight.targetDisplay = displayData.rightPanel;
        camBack.targetDisplay = displayData.backPanel;
        camTop.targetDisplay = displayData.topPanel;
        Save();
    }

    //いい感じにセーブ＆ロードしようと思ったけど、まだビルド地獄なのであまり意味がない
    //そのため、かなり雑な実装
    public void Save()
    {
        string jsonDisplay = JsonUtility.ToJson(displayData);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(jsonDisplay);
        streamWriter.Flush();
        streamWriter.Close();
    }
    public void Load()
    {
        DisplayData disc = new DisplayData();
        if(File.Exists(filePath))
        {
            StreamReader streamReader = new StreamReader(filePath);
            disc = JsonUtility.FromJson<DisplayData>(streamReader.ReadToEnd());
            //Debug.Log(disc.backPanel);
            streamReader.Close();
        }
    }
}
