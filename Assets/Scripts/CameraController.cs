using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoSingleton<CameraController>
{
    [SerializeField] private Transform player;

    private const float CAMERA_DISTANCE = 10f;

    private void Update()
    {
        Vector3 newPosition = new Vector3(player.position.x + CAMERA_DISTANCE * 0.5f, 0f, -CAMERA_DISTANCE);
        transform.position = newPosition;
    }
}
