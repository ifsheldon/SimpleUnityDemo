using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MenuManager
{
    public static void GoToMenu(MenuNames menu)
    {
        switch (menu)
        {
            case MenuNames.Main:
                SceneManager.LoadScene(SceneNames.Main.ToString());
                break;
            case MenuNames.Chapters:
                break;
        }
    }
}