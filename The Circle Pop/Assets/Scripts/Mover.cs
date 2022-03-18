using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mover : MonoBehaviour
{
    public event Action OnGetPointEvent;
    public event Action OnLoseEvent;

    public Vector2 speedMinMax;
    public float rotateSpeed = 30f;
    float speed;
    int dir = 1;
    public float radius = 1.85f;
    public float theta = Mathf.PI / 2;

    Collider2D col;
    public LayerMask layerMask;

    public float timeToHoldKey = 0.3f;
    float timeHoldingKey = 0f;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        col.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveOnTheCircle();
        RotateBeMySelf();

        //InputKeyController();
        InputTouchController();
    }

    void MoveOnTheCircle()
    {
        speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, Difficulty.GetDifficultyPercent());

        float x = radius * Mathf.Cos(theta);
        float y = radius * Mathf.Sin(theta);

        transform.position = new Vector3(x, y, 0);

        theta += Time.deltaTime * speed * dir;
    }

    void RotateBeMySelf()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    void InputKeyController()
    {
        if (timeHoldingKey <= timeToHoldKey && (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)))
        {
            col.isTrigger = true;

            timeHoldingKey += Time.deltaTime;
        }
        else if (timeHoldingKey > timeToHoldKey)
        {
            col.isTrigger = false;
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            col.isTrigger = false;
            timeHoldingKey = 0f;
        }
    }

    void InputTouchController()
    {
        if (Input.touchCount > 0)
        {
            if (timeHoldingKey <= timeToHoldKey && Input.touches[0].phase == TouchPhase.Began)
            {
                col.isTrigger = true;

                timeHoldingKey += Time.deltaTime;
            }
            else if (timeHoldingKey > timeToHoldKey)
            {
                col.isTrigger = false;
            }

            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                col.isTrigger = false;
                timeHoldingKey = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Collider2D c = Physics2D.OverlapCircle(transform.position, transform.localScale.x, layerMask);

        if (other.tag == "Check Point")
        {
            dir *= -1;
            other.gameObject.GetComponent<CheckPoint>().RandomPositionOnCircle();

            if (OnGetPointEvent != null)
            {
                OnGetPointEvent();

                FindObjectOfType<AudioManager>().Play("Pop");

                print("Get Point");
            }

        }
        else if (c == null)
        {
            if (OnLoseEvent != null)
            {
                OnLoseEvent();
                dir = 0;
                print("Lose");
            }
        }
    }

}
