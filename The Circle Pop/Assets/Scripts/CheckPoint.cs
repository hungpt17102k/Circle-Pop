using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public float radius = 1.85f;
    float theta = Mathf.PI / 2;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        RandomPositionOnCircle();
    }

    public void RandomPositionOnCircle()
    {
        var playTheta = player.GetComponent<Mover>().theta;

        theta = playTheta;
        theta += Random.Range(Mathf.PI/2, Mathf.PI);

        float x = radius * Mathf.Cos(theta);
        float y = radius * Mathf.Sin(theta);

        transform.position = new Vector3(x, y, 0);

        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(Vector3.forward * angle);
    }

}
