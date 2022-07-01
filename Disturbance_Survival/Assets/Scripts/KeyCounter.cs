using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyCounter : MonoBehaviour
{

    public int keyCount;

    // Start is called before the first frame update
    void Start()
    {
        keyCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (keyCount == 5) SceneManager.LoadScene("WinScene");
    }

    public void KeyCountPlus()
    {
        keyCount++;
    }
}
