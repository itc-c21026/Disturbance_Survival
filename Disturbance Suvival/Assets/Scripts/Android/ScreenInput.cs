using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-------------------------------------
スワイプ操作のプログラム
-------------------------------------*/

public class ScreenInput : MonoBehaviour
{
    // スワイプ最小移動距離
    [SerializeField]
    private Vector2 SwipeMinRange = new Vector2(50.0f, 50.0f);
    // TAPをNONEに戻すまでのカウント
    [SerializeField]
    private int NoneCountMax = 2;
    private int NoneCountNow = 0;
    // スワイプ入力距離
    private Vector2 SwipeRange;
    // 入力方向記録用
    private Vector2 InputSTART;
    private Vector2 InputMOVE;
    private Vector2 InputEND;


    // スワイプの方向
    public enum SwipeDirection
    {
        NONE,
        TAP,
        UP,
        RIGHT,
        DOWN,
        LEFT,
        UP_LEFT,
        UP_RIGHT,
        DOWN_LEFT,
        DOWN_RIGHT
    }
    public SwipeDirection NowSwipe = SwipeDirection.NONE;


    // Update is called once per frame
    void Update()
    {
        GetInputVector();
        //Debug.Log(NowSwipe);
    }

    // 入力の取得
    private void GetInputVector()
    {
#if UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
        {
            InputSTART = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            InputMOVE = Input.mousePosition;
            SwipeCLC();
        }
        else if (NowSwipe != SwipeDirection.NONE)
        {
            ResetParameter();
        }
#endif

        // Unity上での操作取得
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                InputSTART = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                InputMOVE = Input.mousePosition;
                SwipeCLC();
            }
            else if (NowSwipe != SwipeDirection.NONE)
            {
                ResetParameter();
            }
        }
        // 端末上での操作取得
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];
                if (touch.phase == TouchPhase.Began)
                {
                    InputSTART = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    InputMOVE = Input.mousePosition;
                    SwipeCLC();
                }
            }
            else if (NowSwipe != SwipeDirection.NONE)
            {
                ResetParameter();
            }
        }
    }

    // 入力内容からスワイプ方向を計算
    private void SwipeCLC()
    {
        SwipeRange = new Vector2((new Vector3(InputMOVE.x, 0, 0) - new Vector3(InputSTART.x, 0, 0)).magnitude, (new Vector3(0, InputMOVE.y, 0) - new Vector3(0, InputSTART.y, 0)).magnitude);

        if (SwipeRange.x <= SwipeMinRange.x && SwipeRange.y <= SwipeMinRange.y)
        {
            NowSwipe = SwipeDirection.TAP;
        }
        else
        {
            float _angle = Mathf.Atan2(InputMOVE.y - InputSTART.y, InputMOVE.x - InputSTART.x) * Mathf.Rad2Deg;

            if (-22.5f <= _angle && _angle < 22.5f) NowSwipe = SwipeDirection.RIGHT;
            else if (22.5f <= _angle && _angle < 67.5f) NowSwipe = SwipeDirection.UP_RIGHT;
            else if (67.5f <= _angle && _angle < 112.5f) NowSwipe = SwipeDirection.UP;
            else if (112.5f <= _angle && _angle < 157.5f) NowSwipe = SwipeDirection.UP_LEFT;
            else if (157.5f <= _angle || _angle < -157.5f) NowSwipe = SwipeDirection.LEFT;
            else if (-157.5f <= _angle && _angle < -112.5f) NowSwipe = SwipeDirection.DOWN_LEFT;
            else if (-112.5f <= _angle && _angle < -67.5f) NowSwipe = SwipeDirection.DOWN;
            else if (-67.5f <= _angle && _angle < -22.5f) NowSwipe = SwipeDirection.DOWN_RIGHT;
        }
    }

    // NONEにリセット
    private void ResetParameter()
    {
        NoneCountNow++;
        if (NoneCountNow >= NoneCountMax)
        {
            NoneCountNow = 0;
            NowSwipe = SwipeDirection.NONE;
            SwipeRange = new Vector2(0, 0);
        }
    }

    // スワイプ方向の取得
    public SwipeDirection GetNowSwipe()
    {
        return NowSwipe;
    }

    // スワイプ量の取得
    public float GetSwipeRange()
    {
        if (SwipeRange.x > SwipeRange.y)
        {
            return SwipeRange.x;
        }
        else
        {
            return SwipeRange.y;
        }
    }

    // スワイプ量の取得
    public Vector2 GetSwipeRangeVec()
    {
        if (NowSwipe != SwipeDirection.NONE)
        {
            return new Vector2(InputMOVE.x - InputSTART.x, InputMOVE.y - InputSTART.y);
        }
        else
        {
            return new Vector2(0, 0);
        }
    }
}
