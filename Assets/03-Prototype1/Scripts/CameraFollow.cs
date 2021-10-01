using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    static private CameraFollow S;
    [Header("Set in Inspector")]

    public GameObject Target;
    public float easing = 0.05f;
    public float followEasing = 0.2f;
    public Vector3 offset;
    public Vector3 followOffset;
    

    [Header("Set Dynamically")]
    public float camZ;
    public bool followMode;
    public Vector3 destination;

    private void Start()
    {
        S = this;
    }
    void FixedUpdate()
    {

        if (!followMode)
        {
            
            Vector3 newAngle = Vector3.Lerp(transform.eulerAngles, Vector3.zero, easing);
            transform.eulerAngles = newAngle;
            Vector3 desiredPositon = Target.transform.position + offset;
            Vector3 initialPos = Vector3.Lerp(transform.position,desiredPositon, easing);
            transform.position = initialPos;
        }
        else
        {
            destination = Target.transform.position + followOffset;
            Vector3 followPosition = Vector3.Lerp(transform.position, destination, followEasing);
            transform.position = followPosition;
            Vector3 fRotation = new Vector3(20, 90, 0);
            Vector3 newAngle = Vector3.Lerp(transform.eulerAngles, fRotation, easing);
            transform.eulerAngles = newAngle;
        }
    }
    public static void enableFollow()
    {
        S.followMode = true;
    }
    public static void disableFollow()
    {
        S.followMode = false;
    }
}
