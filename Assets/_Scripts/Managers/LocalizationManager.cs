using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager {

    private Dictionary<string, string> localizedText;
    private bool isReady = false;
    private string missingTextString = "Localized text not found";

    string fileName;
    
    //void Awake () 
    //{
    //    if (Instance == null)
    //    {
    //        {
    //            Instance = this;
    //        }
    //    }
    //    else if (Instance != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //    DontDestroyOnLoad(gameObject);
    //    LoadLocalizedText(fileName);

    //}
    public void Init()
    {
        //fileName = "eng_Translation";
        switch (Application.systemLanguage)
        {
            case SystemLanguage.German:
                fileName = "ger_Translation";
                break;
            case SystemLanguage.Korean:
                fileName = "kor_Translation";
                break;
            default:
                fileName = "eng_Translation";
                break;
        }
        LoadLocalizedText(fileName);
    }

    //리로드 할 일이 없음.
    //public void ReloadTexts(string fileName)
    //{
    //    LoadLocalizedText(fileName);
    //    foreach (LocalizedText text in FindObjectsOfType<LocalizedText>())
    //    {
    //        text.ReloadText();
    //    }
    //}
    
    public void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string, string> ();
        Debug.Log(fileName);
        TextAsset data = Managers.Resource.Load<TextAsset>($"Data/Translation/{fileName}");
        //파일 정상적으로 읽어오는지 확인
//        Debug.Log(Resources.Load<TextAsset>("Texts/"+fileName));
//        Debug.Log(fileName);
//        Debug.Log(mytxtData);
        string txt=data.text;
        if (txt!="" && txt!= null) {
            string dataAsJson = txt;
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData> (dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                //불러오는 데이터 확인
                //Debug.Log(loadedData.items[i].key + ":" + loadedData.items[i].value);
                //공백데이터가 여러개 들어가면 오류발생
                if (!localizedText.ContainsKey(loadedData.items[i].key))
                    localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            Debug.Log ("Data loaded, dictionary contains: " + localizedText.Count + " entries");
        } else 
        {
            Debug.LogError ("Cannot find file!");
        }

        isReady = true;
    }

    public string GetLocalizedValue(string key)
    {
        string result = missingTextString + $"Input Key : {key}";
        if (localizedText.ContainsKey (key)) 
        {
            result = localizedText [key].Replace("\\n","\n");
        }

        return result;

    }

    public bool GetIsReady()
    {
        return isReady;
    }
    
}