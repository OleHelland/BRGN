using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{

    [SerializeField] Transform rainTransform;
    [SerializeField] float _rainControlSpeed;
    [SerializeField] float _angleLerpFactor;
    [SerializeField] float _perlinFrequency;
    [SerializeField] float _perlinAmplitude;


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
        _targetAngle = Mathf.LerpAngle(_targetAngle, deg + 90f, _angleLerpFactor);

        float _perlinRotationAngle = (Mathf.PerlinNoise(Time.time * _perlinFrequency, 0.0f)*2-1) * _perlinAmplitude;

        rainTransform.rotation = Quaternion.AngleAxis(_targetAngle, Vector3.forward) * Quaternion.AngleAxis(_perlinRotationAngle, Vector3.forward);

    }

    public float GetAngle()
    {
        float angle = (((rainTransform.rotation.eulerAngles.z + 90) % 360) + 360) % 360;
        return angle;
    }
}
