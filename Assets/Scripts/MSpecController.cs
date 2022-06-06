using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSpecController : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject sensor;

    private LineRenderer lineRenderer;

    private Ray sensorRay;
    private RaycastHit sensorHit;
    private bool blocked = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        lineRenderer = transform.Find("Line").GetComponent<LineRenderer>();
        sensor = transform.Find("Sensor").gameObject;
        sensorRay = new Ray(sensor.transform.position, gameObject.transform.forward);

        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.0f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, sensor.transform.position + new Vector3(0,1,0));
    }

    void FixedUpdate()
    {
        rb.AddForceAtPosition(sensorRay.direction/2, sensor.transform.position);
        sensorRay.origin = sensor.transform.position;
        if (blocked || !Physics.Raycast(sensorRay, out sensorHit) || sensorHit.transform.gameObject.tag == "Blocker" || sensorHit.transform.gameObject == gameObject) {
            sensorRay.direction = Quaternion.Euler(0,5,0) * sensorRay.direction;
            lineRenderer.SetPosition(0, sensor.transform.position + new Vector3(0, 1, 0));
            lineRenderer.SetPosition(1, sensor.transform.position + new Vector3(0, 1, 0) + sensorRay.direction);
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
            Debug.DrawRay(sensor.transform.position, sensorRay.direction, Color.red);
            return;
        }
        rb.AddForceAtPosition(sensorRay.direction / 2, sensor.transform.position);
        lineRenderer.SetPosition(0, sensor.transform.position + new Vector3(0, 1, 0));
        lineRenderer.SetPosition(1, sensor.transform.position + new Vector3(0, 1, 0) + sensorRay.direction);
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
        Debug.DrawRay(sensor.transform.position, sensorRay.direction, Color.green);
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Blocker")
            blocked = true;
    }

    private void OnCollisionExit(Collision collision) {
        blocked = false;
    }
}
