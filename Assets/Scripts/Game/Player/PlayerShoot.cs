using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] public Joystick rotateJoystick;
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float shootCooldown;
    [SerializeField] private AudioSource shootSource;
    private Animator _animator;
    private PhotonView _photonView;
    private float deltaX;
    private float deltaY;
    [HideInInspector] public bool canShoot;

    public Joystick RotateJoystick { get; set; }

    private void Awake()
    {
        canShoot = false;
        _photonView = GetComponent<PhotonView>();
        _animator = GetComponent<Animator>();
        PlayerGameConnector.Instance.onGameStarted += () => canShoot = true;
    }

    private void Update()
    {
        if (!_photonView.IsMine) return;

        if (!RotateJoystick.isDrag) return;

        deltaX = RotateJoystick.Horizontal * rotateSpeed;
        deltaY = RotateJoystick.Vertical * rotateSpeed;

        if (canShoot)
        {
            Bullet bullet = PhotonNetwork.Instantiate(bulletPref.name, new Vector2(transform.position.x, transform.position.y + 1.4f), Quaternion.identity).GetComponent<Bullet>();
            bullet.PlayerName = PhotonNetwork.LocalPlayer.NickName;
            bullet.StartFly(new Vector2(deltaX, deltaY));
            shootSource.Play();
            StartCoroutine(ShootCooldown());
        }

        _animator.SetFloat("Horizontal", deltaX);
        _animator.SetFloat("Vertical", deltaY);
    }

    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    public void SetPlayerRotateJoystick(Canvas canvas)
    {
        RotateJoystick = Instantiate(rotateJoystick, Vector2.zero, Quaternion.identity);
        RotateJoystick.transform.SetParent(canvas.transform);
        RotateJoystick.GetComponent<RectTransform>().anchoredPosition = new Vector2(-256, 256);
    }
}
