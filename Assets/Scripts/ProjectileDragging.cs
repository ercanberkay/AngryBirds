using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDragging : MonoBehaviour
{
    private bool clickedOn;
    private SpringJoint2D spring;
    private Vector2 prevVelocity;
    private Ray leftCatapultToProjectile;
    private float circleRadius;
    private Transform catapult;
    private Ray rayToMouse;

    public LineRenderer catapultLineFront;
    public LineRenderer catapultLineBack;

    private void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        catapult = spring.connectedBody.transform;
    }

    private void Start()
    {
        LineRendererSetup();
        rayToMouse = new Ray(catapult.position, Vector3.zero);
        leftCatapultToProjectile = new Ray(catapultLineFront.transform.position, Vector3.zero);
        CircleCollider2D circle = GetComponent<CircleCollider2D>();
        circleRadius = circle.radius;
    }

    private void OnMouseDown()
    {
        clickedOn = true;
    }

    private void OnMouseUp()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        clickedOn = false;

    }

    private void Update()
    {
        if (clickedOn)
            Dragging();
        if (spring != null)
        {
            if (!GetComponent<Rigidbody2D>().isKinematic &&
                prevVelocity.sqrMagnitude > GetComponent<Rigidbody2D>().velocity.sqrMagnitude)
            {
                Destroy(spring);
                GetComponent<Rigidbody2D>().velocity = prevVelocity;
            }
        }
        else
        {
            catapultLineFront.enabled = false;
            catapultLineBack.enabled = false;
        }
        prevVelocity = GetComponent<Rigidbody2D>().velocity;

        LineRendererUpdate();
               
    }

    void LineRendererUpdate()
    {
        Vector2 catapultToProjectile = transform.position - catapultLineFront.transform.position;
        leftCatapultToProjectile.direction = catapultToProjectile;
        Vector3 holdPoint = leftCatapultToProjectile.GetPoint(catapultToProjectile.magnitude + circleRadius);
        catapultLineFront.SetPosition(1, holdPoint);
        catapultLineBack.SetPosition(1, holdPoint);
    }

    void Dragging()
    {

        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 catapultToMouse = mouseWorldPoint - catapult.position;
        if (catapultToMouse.sqrMagnitude > 9.0f)
        {
            rayToMouse.direction = catapultToMouse;
            mouseWorldPoint = rayToMouse.GetPoint(6.0f);
        }
        mouseWorldPoint.z = 0f;
        transform.position = mouseWorldPoint;

    }


    void LineRendererSetup() 
    {
        catapultLineFront.SetPosition(0, catapultLineFront.transform.position);
        catapultLineBack.SetPosition(0, catapultLineBack.transform.position);

        catapultLineFront.sortingLayerName = "Foreground";
        catapultLineBack.sortingLayerName = "Foreground";

        catapultLineFront.sortingOrder = 3;
        catapultLineFront.sortingOrder = 1;


    }
}
