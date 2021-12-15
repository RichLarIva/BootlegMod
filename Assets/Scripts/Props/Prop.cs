using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public string name;
    public int healthPoints;

    public GameObject propObject;

    // Start is called before the first frame update
    void Awake()
    {
        propObject = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
