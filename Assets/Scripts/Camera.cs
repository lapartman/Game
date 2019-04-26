﻿using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] PlayerMovement player;

    private void Update()
    {
        Vector3 newCameraPosition = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        transform.position = newCameraPosition;
    }
}