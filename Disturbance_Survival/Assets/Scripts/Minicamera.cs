using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-----------------------------------------
 プレイヤーにカメラを追従させるプログラム
 レーダーのために使用している
-----------------------------------------*/

public class Minicamera : MonoBehaviour
{
    [SerializeField]
    PlayerController player;

    // Update is called once per frame
    void Update()
    {
        // y座標
        var pos = player.transform.position;
        pos.y = transform.position.y;
        pos.z = -10;
        transform.position = pos;
        // x座標
        var pos2 = player.transform.position;
        pos2.x = transform.position.x;
        pos2.z = -10;
        transform.position = pos2;
    }
}
