using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDamage : MonoBehaviour
{
    public int hitPoints = 2;
    public float damageImpactSpeed = 10f;
    public Sprite damagedSprite;

    private SpriteRenderer spriteRenderer;
    private int currentHitPoints;
    private float damageImpactSpeedSqr;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHitPoints = hitPoints;
        damageImpactSpeedSqr = damageImpactSpeed * damageImpactSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Plank")
            return;

        currentHitPoints--;

        if(collision.relativeVelocity.sqrMagnitude < damageImpactSpeedSqr)
        {
            spriteRenderer.sprite = damagedSprite;
            return;
        }

        if (currentHitPoints < 0)
        Kill();
    }

    void Kill()
    {
        spriteRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<ParticleSystem>().Play();
    }
}
