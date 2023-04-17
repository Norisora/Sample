using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //２人のプレイヤーに合わせてカメラSize変更する
    //GameObject player;

    public float minSize = 5f;
    public float maxSize = 10f;
    public float playerMargin = 2f;

    List<Transform> players;

    void Start()
    {
        //this.player = GameObject.Find("cat");
        
        //プレイヤを取得してリストに追加
        players = new List<Transform>();
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in playerObjects) 
        {
            players.Add(playerObject.transform);
        }
    }

    //void Update()
    //{
    //    Vector3 playerPos = this.player.transform.position;
    //    transform.position = new Vector3(
    //        transform.position.x, playerPos.y, transform.position.z);
    //}

    private void LateUpdate()
    {
        //プレイヤーの中心座標の計算
        Vector3 center = Vector3.zero;
        foreach (Transform player in players)
        {
            center += player.position;
        }
        center /= players.Count;        //プレイヤーの中心座標

        //プレイヤー同士の距離を計算
        float maxDistance = 0f;
        foreach (Transform player in players)
        {
            float distance = Vector3.Distance(player.position, center); //プレイヤーとプレイヤー中心との距離
            if (distance > maxDistance)
            {
                maxDistance = distance;
            }
        }

        //カメラのSizeを計算する
        float cameraSize = Mathf.Clamp(maxDistance + playerMargin, minSize, maxSize);   //5～10の間で（距離＋マージン）とったカメラサイズ

        //カメラのSizeを設定する
        Camera.main.orthographicSize = cameraSize;
    }
}
