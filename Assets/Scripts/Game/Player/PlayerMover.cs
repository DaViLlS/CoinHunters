using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour, IPunObservable
{
    [SerializeField] public Joystick moveJoystick;
    [SerializeField] private float moveSpeed;
    private PhotonView _photonView;
    private Animator _animator;
    private Rigidbody2D rb;
    private float deltaX;
    private float deltaY;

    public Joystick MoveJoystick { get; set; }

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!_photonView.IsMine) return;

        deltaX = MoveJoystick.Horizontal * moveSpeed;
        deltaY = MoveJoystick.Vertical * moveSpeed;

        _animator.SetFloat("Speed", new Vector2(deltaX, deltaY).magnitude);

        rb.velocity = new Vector2(deltaX, deltaY);
    }

    public Vector3 GetPlayerMovement()
    {
        return new Vector3(deltaX, deltaY, 0);
    }

    public float GetPlayerSpeed()
    {
        return moveSpeed;
    }

    public void SetPlayerMoveJoystick(Canvas canvas)
    {
        MoveJoystick = Instantiate(moveJoystick, Vector2.zero, Quaternion.identity);
        MoveJoystick.transform.SetParent(canvas.transform);
        MoveJoystick.GetComponent<RectTransform>().anchoredPosition = new Vector2(256, 256);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
