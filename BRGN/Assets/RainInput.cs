using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainInput : MonoBehaviour
{

    float _vertical;
    float _horizontal;


    public bool HasValues() {
        return _vertical != 0 || _horizontal != 0f;
    }

    void Update()
    {
        _vertical = Input.GetAxisRaw("Vertical");
        _horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.W)) {
            _vertical = -1f;
	    }
        if (Input.GetKey(KeyCode.A)) {
            _horizontal = -1f;
	    }
        if (Input.GetKey(KeyCode.S)) { 
            _vertical = 1f;
	    }
        if (Input.GetKey(KeyCode.D)) { 
            _horizontal = 1f;
	    }
    }

    public float GetVertical() {
        return _vertical;
    }

    public float GetHorizontal() {
        return _horizontal;
    }
}
