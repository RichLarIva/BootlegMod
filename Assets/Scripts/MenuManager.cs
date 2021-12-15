using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] Menu[] menus;

    public static MenuManager Instance;

    public void OpenMenu(string menuName)
    {
        for(int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
                OpenMenu(menus[i]);
            else if (menus[i].open)
                CloseMenu(menus[i]);
        } 
    }

    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
            if (menus[i].open)
                CloseMenu(menus[i]);
        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
}
