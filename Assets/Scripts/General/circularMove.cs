using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circularMove : MonoBehaviour
{

    float timeCounter;
    float speed;
    public  float diameter;
    public float height;
    Vector3 initPos;


    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime * speed;
       // float y = 0;
        float x = (Mathf.Sin(timeCounter) * height) + initPos.x;
        float z = (Mathf.Cos(timeCounter) * diameter)+ initPos.z;

        transform.position = new Vector3(x, initPos.y, z);
        
    }
}
