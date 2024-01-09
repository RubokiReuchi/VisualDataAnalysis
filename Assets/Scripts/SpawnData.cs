using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.IO;


public class SpawnData : MonoBehaviour
{
    [SerializeField] TestForReceiver receiveData;
    [SerializeField] GameObject heatmapGO;

    [SerializeField] public GameObject crossPrefab;
    [SerializeField] public GameObject hitPrefab;
    [SerializeField] public GameObject swordPrefab;
    [SerializeField] public GameObject cubePrefab;

    List<GameObject> instancias = new();

    public static SpawnData instance;
    

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawData(new Vector3(0, 0, 0), Quaternion.identity, cubePrefab);
        }
    }

    public void DrawData(Vector3 position, Quaternion rotation, GameObject prefab)
    {
       GameObject  instancePrefab = GameObject.Instantiate(prefab, position, rotation);
       instancias.Add(instancePrefab);

    }

    public void ResetData()
    {
        File.WriteAllText(receiveData.path, string.Empty);
        
        foreach (GameObject instance in instancias)
        {
            Destroy(instance);
        }
        instancias.Clear();

        ParticleSystem ps = heatmapGO.GetComponent<ParticleSystem>();
        if (ps) ps.Clear();

    }
}



