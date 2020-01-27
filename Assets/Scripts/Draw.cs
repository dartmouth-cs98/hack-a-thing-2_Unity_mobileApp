using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Draw : MonoBehaviour
{
    public GameObject stroke;
    public GameObject spacePenPoint;
    public GameObject surfacePenPoint;
    public bool mouseLookTesting;
    [HideInInspector]
    public Transform penPoint;

    public Slider[] colorSliders;

    public static bool drawing = false;

    private float pitch = 0;
    private float yaw = 0;
    private Color colorFromUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (mouseLookTesting)
        {
            yaw += 2 * Input.GetAxis("Mouse X");
            pitch -= 2 * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
        if (PenManager.drawingOnSurface)
        {
            EnableSurfacePenPoint();
        }
        else
        {
            Enable3DSpacePenPoint();
        }

        colorFromUI = new Color(colorSliders[0].value * 2, colorSliders[1].value * 2, colorSliders[2].value * 2);
    }

    void EnableSurfacePenPoint()
    {
        penPoint = surfacePenPoint.transform;

        spacePenPoint.GetComponentInChildren<MeshRenderer>().enabled = false;
        surfacePenPoint.GetComponentInChildren<MeshRenderer>().enabled = true;
        surfacePenPoint.GetComponentInChildren<Renderer>().material.color = colorFromUI;
    }

    void Enable3DSpacePenPoint()
    {
        penPoint = spacePenPoint.transform;

        surfacePenPoint.GetComponentInChildren<MeshRenderer>().enabled = false;
        spacePenPoint.GetComponentInChildren<MeshRenderer>().enabled = true;
        spacePenPoint.GetComponentInChildren<Renderer>().material.color = colorFromUI;
    }

    public void StartStroke()
    {
        GameObject currentStroke;
        drawing = true;
        currentStroke = Instantiate(stroke, penPoint.transform.position, penPoint.transform.rotation) as GameObject;
        currentStroke.GetComponent<Stroke>().strokeColor = colorFromUI;
    }

    public void EndStroke()
    {
        drawing = false;
    }
}