using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCtrl : MonoBehaviour
{
    Coroutine animationAllCoroutine;
    
    void Start()
    {
        StartCoroutine(MoveAnimation(2.0f));
    }
    IEnumerator MoveAnimation(float speed/*, Action isEnd*/)
    {
        while (true)
        {
            while (transform.position.x <= 1.0f)
            {
                Debug.Log("コルーチン１");
                Rigidbody2D body = GetComponent<Rigidbody2D>();
                body.velocity = new Vector2(1f, 0);
              
                yield return null;
            }

            yield return new WaitForSeconds(1.0f);

            while (transform.position.x >= -1.0f)
            {
                Debug.Log("コルーチン2");
                Rigidbody2D body = GetComponent<Rigidbody2D>();
                body.velocity = new Vector2(-1f, 0);
           

                yield return null;
            }
            yield return new WaitForSeconds(1.0f);
        }
        //isEnd?.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        
        
    }
   
    

}
