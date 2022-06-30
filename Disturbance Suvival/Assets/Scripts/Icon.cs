using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*----------------------------------------------
 ミニマップにオブジェクトを表示させるプログラム
----------------------------------------------*/

public class Icon : MonoBehaviour
{
    [SerializeField , Label("ミニマップ用カメラ")] Camera miniCamera;
    [SerializeField , Label("対象アイコン")] Transform iconTarget;　// 対象アイコンの座標
    [SerializeField , Label("対象アイコンの親オブジェクト")] Transform IconT; // アイコンの親オブジェクトの座標
    [SerializeField , Label("オフセット")] float rangeRadiusOffset = 1.0f;

    SpriteRenderer spriteRenderer;

    float minimapRangeRadius;
    float defaultPosZ;
    const float normalAlpha = 1.0f;
    const float outRangeAlpha = 0.5f;

    // スケール調整
    [SerializeField] int scl = 3;
    [SerializeField] float sclb = 8.9f;
    [SerializeField] int minSize = 4;
    // Start is called before the first frame update
    void Start()
    {
        minimapRangeRadius = miniCamera.orthographicSize;
        spriteRenderer = iconTarget.GetComponent<SpriteRenderer>();
        defaultPosZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        DispIcon();
    }

    bool CheckInsideMap()
    {
        var cameraPos = miniCamera.transform.position;
        var targetPos = IconT.position;

        cameraPos.z = targetPos.z = 0;

        return Vector3.Distance(cameraPos, targetPos) <= minimapRangeRadius - rangeRadiusOffset;
    }

    void DispIcon()
    {
        var iconPos = new Vector3(IconT.position.x, IconT.position.y, defaultPosZ);

        if (CheckInsideMap())
        {
            transform.position = iconPos;
            iconTarget.transform.localScale = new Vector3(10.4f, 10.4f, 1);
            return;
        }

        var centerPos = new Vector3(miniCamera.transform.position.x, miniCamera.transform.position.y, defaultPosZ);
        var offset = iconPos - centerPos;
        iconTarget.transform.position = centerPos + Vector3.ClampMagnitude(offset, minimapRangeRadius - rangeRadiusOffset);

        // プレイヤーとアイコンの親オブジェクトが離れるほどアイコンが小さくなる
        // プレイヤーが右にいる時
        if (offset.x <= 0)
        {
            if (offset.x > offset.y) // プレイヤーが右上にいる時
            {
                iconTarget.transform.localScale = new Vector3(offset.y / scl + sclb, offset.y / scl + sclb, 1);
                // アイコンと離れすぎてもminSizeのまま表示
                if (iconTarget.transform.localScale.x <= -minSize)
                {
                    iconTarget.transform.localScale = new Vector3(minSize, minSize, 1);
                }
            }
            else if (-offset.x < offset.y) // プレイヤーが右下にいる時
            {
                iconTarget.transform.localScale = new Vector3(offset.y / scl - sclb, offset.y / scl - sclb, 1);
                if (iconTarget.transform.localScale.x >= minSize)
                {
                    iconTarget.transform.localScale = new Vector3(minSize, minSize, 1);
                }
            }
            else // それ以外
            {
                iconTarget.transform.localScale = new Vector3(offset.x / scl + sclb, offset.x / scl + sclb, 1);
                if (iconTarget.transform.localScale.x <= -minSize)
                {
                    iconTarget.transform.localScale = new Vector3(minSize, minSize, 1);
                }
            }

            var s = iconTarget.transform.localScale;
            // アイコンがminSizeより小さくならない
            if (s.x < minSize && s.x > -minSize)
            {
                s = new Vector3(minSize, minSize, 1);
                iconTarget.transform.localScale = s;
            }
        }

        // 左にいる時
        if (offset.x > 0)
        {
            if (offset.x < offset.y) // プレイヤーが左上にいる時
            {
                iconTarget.transform.localScale = new Vector3(-offset.y / scl + sclb, -offset.y / scl + sclb, 1);
                if (iconTarget.transform.localScale.x <= -minSize)
                {
                    iconTarget.transform.localScale = new Vector3(minSize, minSize, 1);
                }
            }
            else if (-offset.x > offset.y) // プレイヤーが右上にいる時
            {
                iconTarget.transform.localScale = new Vector3(-offset.y / scl - sclb, -offset.y / scl - sclb, 1);
                if (iconTarget.transform.localScale.x >= minSize)
                {
                    iconTarget.transform.localScale = new Vector3(minSize, minSize, 1);
                }
            }
            else // それ以外
            {
                iconTarget.transform.localScale = new Vector3(-offset.x / scl + sclb, -offset.x / scl + sclb, 1);
                if (iconTarget.transform.localScale.x <= -minSize)
                {
                    iconTarget.transform.localScale = new Vector3(minSize, minSize, 1);
                }
            }

            var s = iconTarget.transform.localScale;
            if (s.x < minSize && s.x > -minSize)
            {
                s = new Vector3(minSize, minSize, 1);
                iconTarget.transform.localScale = s;
            }
        }
    }
}
