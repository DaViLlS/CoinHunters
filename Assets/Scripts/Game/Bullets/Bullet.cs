using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IVisitor
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float timeDestroy;
    [SerializeField] private float damage;
    private Vector2 _difference;
    private Rigidbody2D rb;
    
    public string PlayerName { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerHealth visitable))
        {
            if (visitable.PlayerName != PlayerName)
            {
                visitable.GetVisitordamage(damage);
                Destroy(gameObject);
            }
        }
    }

    public void StartFly(Vector2 direction)
    {
        _difference = direction;
        float rotateZ = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
        rb.velocity = transform.right * bulletSpeed;
        Invoke("DestroyBullet", timeDestroy);
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public float GetDamageCount()
    {
        return damage;
    }
}
