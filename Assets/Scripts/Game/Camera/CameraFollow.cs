using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public PlayerCamera Player { get; set; }

    private void Update()
    {
        if (Player == null) return;
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
    }
}
