using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject John;

    void Update(){
        transform.position = new Vector3(John.transform.position.x, transform.position.y, transform.position.z);
    }
}
