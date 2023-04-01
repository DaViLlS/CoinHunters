using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();

        if (_photonView.Owner.IsLocal)
        {
            Camera.main.GetComponent<CameraFollow>().Player = this;
        }
    }
}
