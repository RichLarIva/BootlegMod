using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInheritScript : MonoBehaviour
{
    public Button[] buttons;
    public SpawnProps propsMenu;

    // Start is called before the first frame update
    void Start()
    {

        propsMenu = GameObject.FindGameObjectWithTag("spawnPropsMenuTag").GetComponent<SpawnProps>();

        int indexer = 0;
        foreach (Button i in buttons)
        {
            i.onClick.AddListener(() => propsMenu.SpawnProp(indexer));
            indexer++;
            Debug.Log("test");
        }

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
