using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{

    [SerializeField] Transform rainTransform;

    static Rain _instance;
    public static Rain Instance { get { return _instance; } }

    private void Awake()
    {
        if(_instance == null) {
            _instance = this;
	    }
        else { 
	    }
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    void Update()
    {
        ////float vertical = Input.GetAxis("Vertical");
        ////float horizontal = Input.GetAxis("Horizontal");

        //rainTransform.rotation.SetLookRotation(new Vector3(vertical, horizontal));
    }

    public float GetAngle()
    {
        float angle = (((rainTransform.rotation.eulerAngles.z + 90) % 360) + 360) % 360;
        Debug.Log("Rain Angle: " + angle);
        return angle;
    }
}
