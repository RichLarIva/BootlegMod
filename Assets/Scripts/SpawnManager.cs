using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;

    public float xMin;
    public float xMax;
    public float y;
    public float zMin;
    public float zMax;

    // Start is called before the first frame update
    void Awake()
    {
        float x = Random.Range(xMin, xMax + 1);
        float z = Random.Range(zMin, zMax + 1);

        Instantiate(playerPrefab, new Vector3(x, y, z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
