using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSConfig : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 30;
    }
}
