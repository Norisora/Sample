using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TitleAnimation : MonoBehaviour
{
    enum State
    {right, left, gameStart};

    State state = State.left;
    System.Action animationEnd;

    Rigidbody2D rigid2D;
    Animator animator;

    float jumpForce = 680.0f;
    float walkForce = 60.0f;
    float maxWalkSpeed = 2.0f;
    int keyCount = 2;   //ジャンプ時よこの移動量
    int cloudsCount = 0; 
    bool startFlag = false;   //ゲームスタート用フラグ
 
    private float jumpInterval = 2.0f; // ジャンプの間隔
    //private bool isJumping = false;

    public GameObject[] clouds = new GameObject[8];
    GameObject cloudPrefab;
    GameObject cat;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();

        this.cloudPrefab = GameObject.Find("cloud1");
        this.cat = GameObject.Find("cat");

        StartCoroutine(RpeatJump(1));
    }

    
    IEnumerator RpeatJump(int key)
    {
        while (!startFlag)
        {
            yield return new WaitForSeconds(jumpInterval);
            yield return Jump(key);
            yield return CloudTeleport();
            yield return new WaitForSeconds(jumpInterval);
            yield return Jump(-key);
            yield return CloudTeleport();
        }
       
        //スタートボタンが押されたら
        //ゲームシーンへ遷移
        if (startFlag)
        {
            Debug.Log("ゲームスタートまで２秒");
            yield return new WaitForSeconds(jumpInterval);
            animationEnd?.Invoke();

        }
        yield return null;
    }
    IEnumerator CloudTeleport()
    {
       
        float startY = clouds[cloudsCount].transform.position.y;
        float yOffset = 2.5f;

        float y = startY + clouds.Length * yOffset;
        clouds[cloudsCount].transform.position = new Vector2(clouds[cloudsCount].transform.position.x, y);
        if (cloudsCount < 7)
        {
            cloudsCount++;
        }
        else
        {
            cloudsCount = 0;
        }
        yield return null;
    }
    IEnumerator Jump(int key)
    {
        if (key == 1)
        { state = State.right; }
        if (key == -1)
        { state = State.left; }
        Debug.Log("state=" + state);
        //isJumping = true;   //ジャンプフラグ

        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        if (speedx < this.maxWalkSpeed)
        {
            for (int i = 0; i < keyCount; i++)
            {
                this.rigid2D.AddForce(transform.right * key * this.walkForce);
                Debug.Log("i" + i);
            }
        }
        //動く方向に応じて反転
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        if (this.rigid2D.velocity.y == 0)   //多段ジャンプ禁止
        {
            this.animator.SetTrigger("JumpTrigger");
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        //isJumping = false;

        yield return null;
    }

    public void GameStartAnimation(System.Action animationEndCallback)
    {
        Debug.Log("ゲームスタートアニメーション");
        startFlag = true;

        float catPosY = this.cat.transform.position.y;
        switch (state)
        {
            case State.left:    //雲を右側に３つ
                for (int i = 1; i <= 3; i++)
                {
                    Debug.Log("右に３つ");
                    Instantiate(cloudPrefab, new Vector2(i, catPosY + 2.0f), Quaternion.identity);
                }
                break;

            case State.right:   //雲を左側に３つ
                for (int i = 1; i <= 3; i++)
                {
                    Debug.Log("左に３つ");
                    Instantiate(cloudPrefab, new Vector2(-i, catPosY + 2.0f), Quaternion.identity);
                }
                break;
        }
        StartCoroutine(RpeatJump(1));

        //catが雲３つに乗ってからよびたい
        animationEnd = animationEndCallback;
    }
    
}
