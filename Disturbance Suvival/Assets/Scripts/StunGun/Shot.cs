using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*--------------------------------
アイテム「スタンガン」のプログラム
--------------------------------*/

public class Shot : MonoBehaviour
{

    [SerializeField]
    private GameObject createObject; // 生成するオブジェクト

    [SerializeField]
    private int itemCount = 8; // 生成するオブジェクトの数

    [SerializeField]
    private float radius; // 半径

    [SerializeField]
    private float repeat = 1f; // 何周期するか

    float vct = 0;

    int childNumber = 3;

    public void UseStunGun() // スタンガンを使った時の関数
    {
        var oneCycle = 2.0f * Mathf.PI; // sin の周期は 2π
        vct = 0;

        for (var i = 0; i < itemCount; ++i)
        {

            var point = ((float)i / itemCount) * oneCycle; // 周期の位置 (1.0 = 100% の時 2π となる)
            var repeatPoint = point * repeat - 11; // 繰り返し位置

            var x = Mathf.Cos(repeatPoint) * radius + transform.position.x;
            var y = Mathf.Sin(repeatPoint) * radius + transform.position.y;

            var position = new Vector3(x, y);

            Instantiate(
                createObject,
                position,
                Quaternion.Euler(0, 0, vct),
                transform
            );
            vct += 11.25f;

        }
    }

}
