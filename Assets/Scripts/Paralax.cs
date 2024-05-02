using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;

    private float xPositon;
    private void Start()
    {
        cam = GameObject.Find("Main Camera");

        xPositon = transform.position.x;
    }

    private void Update()
    {
        float DistanceToMove = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(xPositon + DistanceToMove, transform.position.y);
    }
}
