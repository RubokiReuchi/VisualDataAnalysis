using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class DataReceiver: MonoBehaviour
{
    public enum DataType
    {
        DAMAGED,
        DEATH,
        HIT,
        PATH
    }

    public delegate void DataReceiverDelegate(string result);
    public static event DataReceiverDelegate OnReceiveData;

    public static void receiveData(DataType dataType, uint playerInfo)
    {
        Instance.StartCoroutine(Instance.getData(dataType, playerInfo));
    }

    //SINGLETON
    private static DataReceiver _instance;
    private static DataReceiver Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DataReceiver>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("DataReceiver");
                    _instance = go.AddComponent<DataReceiver>();
                }
            }
            return _instance;
        }
    }



    IEnumerator getData(DataType dataType, uint playerInfo)
    {

        
        WWWForm form = new WWWForm();

        string type = "";
        switch (dataType)
        {
            case DataType.DAMAGED:
                type = "damaged";
                break;
            case DataType.DEATH:
                type = "death";
                break;
            case DataType.HIT:
                type = "hit";
                break;
            case DataType.PATH:
                type = "path";
                break;
        }

        form.AddField("dataToGet", type);
        form.AddField("playerInfo", playerInfo.ToString());

        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~bielrg/_GetData.php", form);
        
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Web request went WRONG!");
        }
        else
        {
            Debug.Log("Web request SUCCESSFUL!");
            //Debug.Log("\n\n" + www.downloadHandler.text);
            OnReceiveData?.Invoke(www.downloadHandler.text);
        }
    }
}