using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForReceiver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataReceiver.OnReceiveData += DoSomething;
        DataReceiver.receiveData(DataReceiver.DataType.DEATH, 0);
    }

    void DoSomething(string s)
    {
        Debug.Log("I did something!");
    }
}
