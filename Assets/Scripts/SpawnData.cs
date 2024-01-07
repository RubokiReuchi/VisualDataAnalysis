using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnData : MonoBehaviour
{

    [SerializeField] GameObject crossPrefab;
    [SerializeField] GameObject hitPrefab;
    [SerializeField] GameObject swordPrefab;
    [SerializeField] GameObject cubePrefab;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawData(new Vector3(0, 0, 0), Quaternion.identity, cubePrefab);
        }
    }

    void DrawData(Vector3 position, Quaternion rotation, GameObject prefab)
    {
        GameObject.Instantiate(prefab, position, rotation);
    }

}

