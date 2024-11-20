using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 8f;
    public Vector3 offset;

    void Update()
    {
        if(target ==  null){
            Debug.Log("No Player to follow");
            return;
        }

        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;
    }
}
