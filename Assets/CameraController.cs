using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //�Q�l�̃v���C���[�ɍ��킹�ăJ����Size�ύX����
    //GameObject player;

    public float minSize = 5f;
    public float maxSize = 10f;
    public float playerMargin = 2f;

    List<Transform> players;

    void Start()
    {
        //this.player = GameObject.Find("cat");
        
        //�v���C�����擾���ă��X�g�ɒǉ�
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
        //�v���C���[�̒��S���W�̌v�Z
        Vector3 center = Vector3.zero;
        foreach (Transform player in players)
        {
            center += player.position;
        }
        center /= players.Count;        //�v���C���[�̒��S���W

        //�v���C���[���m�̋������v�Z
        float maxDistance = 0f;
        foreach (Transform player in players)
        {
            float distance = Vector3.Distance(player.position, center); //�v���C���[�ƃv���C���[���S�Ƃ̋���
            if (distance > maxDistance)
            {
                maxDistance = distance;
            }
        }

        //�J������Size���v�Z����
        float cameraSize = Mathf.Clamp(maxDistance + playerMargin, minSize, maxSize);   //5�`10�̊ԂŁi�����{�}�[�W���j�Ƃ����J�����T�C�Y

        //�J������Size��ݒ肷��
        Camera.main.orthographicSize = cameraSize;
    }
}
