using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseController : MonoBehaviour
{
    //1Pスクリプト
    [SerializeField]
    SceneController sceneController;

    Rigidbody2D myRigid2D;
    Animator animator;
    float jumpForce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    float speedx;
    int key;
    Vector2 initialPos;     //初期ポジション

    int stock = 3;       //残機設定

    // Start is called before the first frame update
    void Start()
    {
        //this.sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();

        initialPos = this.transform.position;       //初期ポジション記録

        Application.targetFrameRate = 60;
        this.myRigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();

        Debug.Log(stock);
    }

    // Update is called once per frame
    void Update()
    {
        //ジャンプする
        if (Input.GetKeyDown(KeyCode.Space) &&
                this.myRigid2D.velocity.y == 0)//多段ジャンプ禁止
        {
            this.animator.SetTrigger("JumpTrigger");
            this.myRigid2D.AddForce(transform.up * this.jumpForce);
        }

        //左右移動
        Move(key);

        //プレイヤーの速度
        speedx = Mathf.Abs(this.myRigid2D.velocity.x);

        //スピード制限
        if (speedx < this.maxWalkSpeed)
        {
            this.myRigid2D.AddForce(transform.right * key * this.walkForce);
        }

        //動く方向に応じて反転
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        //プレイヤの速度に応じてアニメーション速度を変える
        if (this.myRigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }

        //画面外に出た場合は初期ポジションへ
        //残機を減らす。残機がなければゲームオーバー
        if (transform.position.y < -10)
        {
            if (stock != 0)
            {

                stock--;
                transform.position = initialPos;

            }
            else
            {
                //this.sceneController.ChangeScene("TitleScene");
            }
            Debug.Log(stock);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("何かがあたった");
            //衝突した相手のRigid2Dを取得
            Rigidbody2D otherRigid2D = collision.gameObject.GetComponent<Rigidbody2D>();
            if (otherRigid2D != null)
            {
                float pushForce = speedx / 2.5f;

                // 自分自身の中心点と相手の中心点を取得し、その差分を計算
                Vector2 direction = (myRigid2D.position - otherRigid2D.position).normalized;

                // 自分自身に向けて力を加える
                myRigid2D.AddForce(direction * pushForce, ForceMode2D.Impulse);

                // 衝突した相手にも逆向きの力を加える
                otherRigid2D.AddForce(-direction * pushForce, ForceMode2D.Impulse);
            }
        }

    }


    //ゴールに到達
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ゴール");
        this.sceneController.ChangeScene("ClearScene");
    }

    protected virtual int Move(int key)
    {
        Debug.Log("key=" + key);
        return 0;
    }
}
