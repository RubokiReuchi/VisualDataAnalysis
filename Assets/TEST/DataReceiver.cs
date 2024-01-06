using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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

    private void Start()
    {
        // TEST
        receiveData(DataType.HIT, 0);        
    }

    public void receiveData(DataType dataType, uint playerInfo)
    {
        StartCoroutine(getData(dataType, playerInfo));
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
            Debug.Log("Web request SUCCESSFUL!\n\n" + www.downloadHandler.text);
        }
    }
}