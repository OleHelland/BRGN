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
    [SerializeField] float _perlinMaxFrequency;
    [SerializeField] float _perlinAmplitude;


    static Rain _instance;
    public static Rain Instance { get { return _instance; } }

    float _targetAngle;

    RainInput _rainInput;

    private void Awake()
    {
        if(_instance == null) {
            _instance = this;
	    }
        else { 
	    }

        _targetAngle = rainTransform.rotation.eulerAngles.z;

        _rainInput = GetComponent<RainInput>();
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    void Update()
    {
        if (_rainInput == null) {
        }

        float vertical = 0f;
        float horizontal = 0f;

        if (_rainInput.HasValues()) {
            vertical = _rainInput.GetVertical();
            horizontal = _rainInput.GetHorizontal();
		}

        float rad = Mathf.Atan2(vertical, horizontal);
        float deg = ((rad * 180f) / Mathf.PI);
        if(deg < 360f) {
            deg = 360f - deg;
	    }
        _targetAngle = Mathf.LerpAngle(_targetAngle, deg + 90f, _angleLerpFactor);

        float windFrequency = Mathf.PerlinNoise(Time.time * _perlinFrequency, 1.0f) * _perlinMaxFrequency;
        float _perlinRotationAngle = (Mathf.PerlinNoise(Time.time * _perlinFrequency, 0.0f)*2-1) * _perlinAmplitude * windFrequency;

        rainTransform.rotation = Quaternion.AngleAxis(_targetAngle, Vector3.forward) * Quaternion.AngleAxis(_perlinRotationAngle, Vector3.forward);

    }

    public float GetAngle()
    {
        float angle = (((rainTransform.rotation.eulerAngles.z + 90) % 360) + 360) % 360;
        return angle;
    }
}
