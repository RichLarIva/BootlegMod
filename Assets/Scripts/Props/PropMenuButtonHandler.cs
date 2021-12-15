using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PropMenuButtonHandler : MonoBehaviour
{
    public Prop[] props;
    public GameObject buttonPrefab;
    private Button test;
    private TMP_Text propName;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Prop prop in props)
        {
            test = Instantiate(buttonPrefab, transform.position, transform.rotation).GetComponent<Button>();
            test.name = "Spawn" + prop.name;
            propName = test.GetComponentInChildren<TMP_Text>();
            propName.text = prop.name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
