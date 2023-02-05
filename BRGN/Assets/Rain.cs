using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{

    [SerializeField] Transform rainTransform;
    [SerializeField] float _rainControlSpeed;

    static Rain _instance;
    public static Rain Instance { get { return _instance; } }

    float _targetAngle;

    private void Awake()
    {
        if(_instance == null) {
            _instance = this;
	    }
        else { 
	    }

        _targetAngle = rainTransform.rotation.eulerAngles.z;
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        float rad = Mathf.Atan2(vertical, horizontal);
        float deg = ((rad * 180f) / Mathf.PI);
        if(deg < 360f) {
            deg = 360f - deg;
	    }
        Debug.Log("Degrees: " + deg);
        _targetAngle = deg + 90f;

        rainTransform.rotation = Quaternion.Lerp(rainTransform.rotation, Quaternion.AngleAxis(_targetAngle, Vector3.forward), Time.deltaTime * _rainControlSpeed);

    }

    public float GetAngle()
    {
        float angle = (((rainTransform.rotation.eulerAngles.z + 90) % 360) + 360) % 360;
        return angle;
    }
}
