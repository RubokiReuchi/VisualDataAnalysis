using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class TestForReceiver : MonoBehaviour
{
    //0 for all users
    public uint userID = 0;

    public bool showPath = true;
    public bool showDeath = false;
    public bool showHit = false;
    public bool showAttack = true;

    string path = "Assets/Heatmap/Assets/MyData.txt";
    

    [System.Serializable]
    public class PositionData
    {
        public float x;
        public float y;
        public float z;
    }

    [System.Serializable]
    public class EventData
    {
        public PositionData Position;
        public string EventName;
    }

    public void Receive()
    {
        if (showPath)
        {
            DataReceiver.OnReceiveData += DoSomething;


            DataReceiver.receiveData(DataReceiver.DataType.PATH, userID);
        }
        else if (showDeath)
        {
            DataReceiver.OnReceiveData += DoSomething;


            DataReceiver.receiveData(DataReceiver.DataType.DEATH, userID);
        }
        else if (showHit)
        {
            DataReceiver.OnReceiveData += DoSomething;


            DataReceiver.receiveData(DataReceiver.DataType.DAMAGED, userID);
        }
        else if (showAttack)
        {
            DataReceiver.OnReceiveData += DoSomething;


            DataReceiver.receiveData(DataReceiver.DataType.HIT, userID);
        }
    }

    void DoSomething(string s)
    {
        Debug.Log(s);
        if (userID == 0 && showPath )
        {

            ConvertirAJSONFormato(s, path);
        }
        else
        {
            //Check if s is correct
            if (s.Contains("error"))
            {
                Debug.Log("Something went wrong!");
                return;
            }

            string[] rows = s.Split('\n');
            for (int i = 0; i < rows.Length - 1; i++)
            {
                string[] col = rows[i].Split(",");

                Vector3 pos = new Vector3(float.Parse(col[0], CultureInfo.InvariantCulture), float.Parse(col[1], CultureInfo.InvariantCulture), float.Parse(col[2], CultureInfo.InvariantCulture));
                if (showPath)
                {
                    SpawnData.instance.DrawData(pos, Quaternion.identity, SpawnData.instance.crossPrefab);
                }else if (showAttack)
                {
                    SpawnData.instance.DrawData(pos, Quaternion.identity, SpawnData.instance.swordPrefab);
                }
                else if (showDeath)
                {
                    SpawnData.instance.DrawData(pos, Quaternion.identity, SpawnData.instance.crossPrefab);
                }
                else if (showHit)
                {
                    SpawnData.instance.DrawData(pos, Quaternion.identity, SpawnData.instance.hitPrefab);
                }

                Debug.Log(pos);
            }
        }

    }

    void ConvertirAJSONFormato(string inputString, string ruta)
    {
        Debug.Log("TXT Created");

        string[] rows = inputString.Split('\n');
        for (int i = 0; i < rows.Length - 1; i++)
        {
            string[] col = rows[i].Split(",");
            PositionData posicion = new PositionData
            {
                x = float.Parse(col[0], CultureInfo.InvariantCulture),
                y = float.Parse(col[1], CultureInfo.InvariantCulture),
                z = float.Parse(col[2], CultureInfo.InvariantCulture)
            };

            // Crea un nuevo objeto EventData y asigna los valores
            EventData evento = new EventData
            {
                Position = posicion,
                EventName = "Object Position"
            };
            // Convierte el objeto a formato JSON
            string resultadoJSON = JsonUtility.ToJson(evento);

            Debug.Log("String converted to JSON: " + resultadoJSON);

            EscribirJSONEnArchivo(resultadoJSON, ruta);

         

        }


    }
    void EscribirJSONEnArchivo(string json, string ruta)
    {
        try
        {
            // Abre el archivo para escribir (si no existe, lo crea; si existe, sobrescribe)
            StreamWriter escritor = new StreamWriter(ruta, false);

            // Escribe el JSON en el archivo seguido de un salto de línea
            escritor.WriteLine(json);

            // Cierra el archivo
            escritor.Close();

            Debug.Log("JSON writed in: " + ruta);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error writing archive: " + ex.Message);
        }
    }
}
