using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnData : MonoBehaviour
{

    [SerializeField] public GameObject crossPrefab;
    [SerializeField] public GameObject hitPrefab;
    [SerializeField] public GameObject swordPrefab;
    [SerializeField] public GameObject cubePrefab;

    List<GameObject> instancias;

    public static SpawnData instance;
    public static GameObject instancePrefab;

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
        instancePrefab = GameObject.Instantiate(prefab, position, rotation);

    }

    public void ResetData()
    {
        
            
    }


}

