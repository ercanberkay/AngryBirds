using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resetter : MonoBehaviour
{
    public Rigidbody2D projectile;
    public float resetSpeed = 0.025f;


    private SpringJoint2D spring;
    private float resetSpeedSqr;

    private void Start()
    {
        resetSpeedSqr = resetSpeed * resetSpeed;
        spring = projectile.GetComponent<SpringJoint2D>();
    }

    private void Update()
    {
        if(projectile.velocity.sqrMagnitude < resetSpeedSqr && spring == null)
        {
            Reset();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Rigidbody2D>() == projectile)
        {
            Invoke("Reset",3f);
        }
    }

    private void Reset()
    {
        SceneManager.LoadScene("level_01");
    }

}
