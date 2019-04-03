using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    
    void Update()
    {
        Vector3 newCameraPosition = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        transform.position = newCameraPosition;
    }
}