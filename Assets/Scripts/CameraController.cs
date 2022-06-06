using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();

        cam.transform.position = target.transform.position + new Vector3(0, 20, 0);
        cam.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = target.transform.position + new Vector3(0, 20, 0);
        cam.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
