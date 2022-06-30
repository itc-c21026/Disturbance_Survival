using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*------------------------------------
アイテム「スタンガン」のプログラム
------------------------------------*/

public class StunShotScript : MonoBehaviour
{

    //public Rigidbody2D rgb;
    public float shotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ObjectDestroy",0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        //rgb.velocity = transform.up * shotSpeed * Time.deltaTime;
        transform.Translate(transform.up * shotSpeed * Time.deltaTime);
    }

    public void ObjectDestroy()
    {
        GameObject.Destroy(gameObject);
    }
}
