using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Camera camera;
    public GameObject player;

    void Start()
    {
        
    }


    void Update()
    {
        Vector3 vector = new Vector3(player.transform.position.x,player.transform.position.y,-10f);
        camera.transform.position = vector;
    }
}
