using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*------------------------------
 * キャラ選択のプログラム
 -----------------------------*/

public class CharacterButton : MonoBehaviour
{
    [SerializeField]
    CharacterSelect script;

    [SerializeField]
    GameController script2;

    void Start()
    {
        script.num = 0;
    }

    public void OnClickA()
    {
        script.num = 1;
        script2.number = 1;
        PlayerPrefs.SetInt("ItemMax_NUMBER", 3);
        PlayerPrefs.SetInt("LifeMax_NUMBER", 5);
    }
    public void OnClickB()
    {
        script.num = 2;
        script2.number = 2;
        PlayerPrefs.SetInt("ItemMax_NUMBER", 2);
        PlayerPrefs.SetInt("LifeMax_NUMBER", 4);
    }
    public void OnClickC()
    {
        script.num = 3;
        script2.number = 3;
        PlayerPrefs.SetInt("ItemMax_NUMBER", 2);
        PlayerPrefs.SetInt("LifeMax_NUMBER", 6);
    }
    public void OnClickD()
    {
        script.num = 4;
        script2.number = 4;
        PlayerPrefs.SetInt("ItemMax_NUMBER", 4);
        PlayerPrefs.SetInt("LifeMax_NUMBER", 4);
    }
}
