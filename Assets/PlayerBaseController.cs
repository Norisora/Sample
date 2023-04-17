using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseController : MonoBehaviour
{
    //1P�X�N���v�g
    [SerializeField]
    SceneController sceneController;

    Rigidbody2D myRigid2D;
    Animator animator;
    float jumpForce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    float speedx;
    int key;
    Vector2 initialPos;     //�����|�W�V����

    int stock = 3;       //�c�@�ݒ�

    // Start is called before the first frame update
    void Start()
    {
        //this.sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();

        initialPos = this.transform.position;       //�����|�W�V�����L�^

        Application.targetFrameRate = 60;
        this.myRigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();

        Debug.Log(stock);
    }

    // Update is called once per frame
    void Update()
    {
        //�W�����v����
        if (Input.GetKeyDown(KeyCode.Space) &&
                this.myRigid2D.velocity.y == 0)//���i�W�����v�֎~
        {
            this.animator.SetTrigger("JumpTrigger");
            this.myRigid2D.AddForce(transform.up * this.jumpForce);
        }

        //���E�ړ�
        Move(key);

        //�v���C���[�̑��x
        speedx = Mathf.Abs(this.myRigid2D.velocity.x);

        //�X�s�[�h����
        if (speedx < this.maxWalkSpeed)
        {
            this.myRigid2D.AddForce(transform.right * key * this.walkForce);
        }

        //���������ɉ����Ĕ��]
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        //�v���C���̑��x�ɉ����ăA�j���[�V�������x��ς���
        if (this.myRigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }

        //��ʊO�ɏo���ꍇ�͏����|�W�V������
        //�c�@�����炷�B�c�@���Ȃ���΃Q�[���I�[�o�[
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
            Debug.Log("��������������");
            //�Փ˂��������Rigid2D���擾
            Rigidbody2D otherRigid2D = collision.gameObject.GetComponent<Rigidbody2D>();
            if (otherRigid2D != null)
            {
                float pushForce = speedx / 2.5f;

                // �������g�̒��S�_�Ƒ���̒��S�_���擾���A���̍������v�Z
                Vector2 direction = (myRigid2D.position - otherRigid2D.position).normalized;

                // �������g�Ɍ����ė͂�������
                myRigid2D.AddForce(direction * pushForce, ForceMode2D.Impulse);

                // �Փ˂�������ɂ��t�����̗͂�������
                otherRigid2D.AddForce(-direction * pushForce, ForceMode2D.Impulse);
            }
        }

    }


    //�S�[���ɓ��B
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("�S�[��");
        this.sceneController.ChangeScene("ClearScene");
    }

    protected virtual int Move(int key)
    {
        Debug.Log("key=" + key);
        return 0;
    }
}
