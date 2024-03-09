using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public BulletData bulletData;

    public Vector2 startPosition;
    public float conquaredDistance = 0;
    private Rigidbody2D rb;

    public UnityEvent OnHit = new UnityEvent();
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

    public void Initialize(BulletData bulletData)
    {
        this.bulletData = bulletData;
        startPosition = transform.position;
        rb.velocity = transform.up * this.bulletData.speed;
    }

	private void Update()
	{
        conquaredDistance = Vector2.Distance(transform.position, startPosition);
        if(conquaredDistance > this.bulletData.maxDistance)
        {
            DisableObject();
        }
	}

    private void DisableObject()
    {
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        Debug.Log("Collider " + collision.name);
        OnHit?.Invoke();
        var damagable = collision.GetComponent<Damagable>();
        if (damagable != null)
        {
            damagable.Hit(this.bulletData.damage);
        }

        DisableObject();
	}
}
