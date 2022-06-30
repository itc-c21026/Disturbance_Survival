using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectTest : MonoBehaviour
{

    public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(effect, new Vector3(-13, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
