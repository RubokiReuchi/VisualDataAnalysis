using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class TestForReceiver : MonoBehaviour
{
    //0 for all users
    public uint userID = 0;


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

    // Start is called before the first frame update
    void Start()
    {
        DataReceiver.OnReceiveData += DoSomething;

        //poner aqui lo de la ui
        DataReceiver.receiveData(DataReceiver.DataType.PATH, userID);
    }

    void DoSomething(string s)
    {
        if(userID == 0 ) //tambien solo si es PATH
        {

            ConvertirAJSONFormato(s, path);
        }


        
        //Check if s is correct
        if (s.Contains("error"))
        {
            Debug.Log("Something went wrong!");
            return;
        }

        string[] rows = s.Split('\n');
        for (int i = 0; i < rows.Length - 1; i++) {
            string[] col = rows[i].Split(",");

            Vector3 pos = new Vector3(float.Parse(col[0]), float.Parse(col[1]), float.Parse(col[2]));
            Debug.Log(pos);
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
                x = float.Parse(col[0]),
                y = float.Parse(col[1]),
                z = float.Parse(col[2])
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
            StreamWriter escritor = new StreamWriter(ruta, true);

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
