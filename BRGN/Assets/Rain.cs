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
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        rainTransform.rotation.SetLookRotation(new Vector3(vertical, horizontal));
    }

    public float GetAngle()
    {
        return rainTransform.rotation.eulerAngles.z % 360;
    }
}
