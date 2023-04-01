using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] public Joystick rotateJoystick;
    [SerializeField] private float rotateSpeed;
    private Animator _animator;
    private PhotonView _photonView;
    private float deltaX;
    private float deltaY;

    public Joystick RotateJoystick { get; set; }

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_photonView.IsMine) return;

        if (!RotateJoystick.isDrag) return;

        deltaX = RotateJoystick.Horizontal * rotateSpeed;
        deltaY = RotateJoystick.Vertical * rotateSpeed;

        _animator.SetFloat("Horizontal", deltaX);
        _animator.SetFloat("Vertical", deltaY);
    }



    public void SetPlayerRotateJoystick(Canvas canvas)
    {
        RotateJoystick = Instantiate(rotateJoystick, Vector2.zero, Quaternion.identity);
        RotateJoystick.transform.SetParent(canvas.transform);
        RotateJoystick.GetComponent<RectTransform>().anchoredPosition = new Vector2(-256, 256);
    }
}
