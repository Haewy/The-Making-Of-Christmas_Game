using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // set a target the camera will follow 
    public Transform target;
    private float distance = 6f;
    private float height = 5f;

    private Transform camPos;

    // Start is called before the first frame update
    void Start()
    {
        camPos = GetComponent<Transform>();   
    }

    // Update is called once per frame
    void Update()
    {
        // Set camera position
        camPos.position = target.position - (1 * Vector3.forward * distance) + (Vector3.up * height);
        camPos.LookAt(target);
        //https://blog.naver.com/zeeoe/220773343567
    }
}
