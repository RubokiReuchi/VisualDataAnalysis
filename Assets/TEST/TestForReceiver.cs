using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestForReceiver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataReceiver.OnReceiveData += DoSomething;
        DataReceiver.receiveData(DataReceiver.DataType.PATH, 0);
    }

    void DoSomething(string s)
    {
        //Check if s is correct
        if (s.Contains("error"))
        {
            Debug.Log("Something went wrong!");
            return;
        }

        string[] rows = s.Split('\n');
        for (int i = 0; i < rows.Length - 1; i++) {
            string[] col = rows[i].Split(",");

            Vector3 pos = new Vector3(float.Parse(col[0]), float.Parse(col[1]), float.Parse(col[2]))
            Debug.Log(pos);
        }
    }
}
