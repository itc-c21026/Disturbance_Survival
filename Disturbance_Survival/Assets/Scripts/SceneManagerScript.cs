using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*-----------------------------------------
 * ÉVÅ[ÉìëJà⁄
 ----------------------------------------*/
public class SceneManagerScript : MonoBehaviour
{
    bool fs;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            if (fs == false)
            {
                Screen.fullScreen = true;
                fs = true;
            }
            else
            {
                Screen.fullScreen = false;
                fs = false;
            }
        }
    }


    public void OnLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void Title()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void GameClear()
    {
        SceneManager.LoadScene("WinScene");
    }
}
