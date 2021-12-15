using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QMenuManager : MonoBehaviour
{
    public GameObject qMenu;
    public bool inQmenu = false;

    public GameObject hpObject;

    public MouseLook mouseLook;

    // Start is called before the first frame update
    void Awake()
    {
        qMenu.SetActive(false);
        mouseLook = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.F1))
        {
            if (inQmenu)
            {
                ExitQMenu();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                EnterQMenu();
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }


    void EnterQMenu()
    {
        qMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        inQmenu = true;
        Debug.Log("IN QMENU");
        hpObject.SetActive(false);
        mouseLook.enabled = false;
    }

    void ExitQMenu()
    {
        qMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        inQmenu = false;
        Debug.Log("NOT IN QMENU");
        hpObject.SetActive(true);
        mouseLook.enabled = true;
    }
}
