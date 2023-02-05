using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketFiller : MonoBehaviour, IFillable, ICommunicateActiveState
{
    [Serializable]
    public struct Arc {
        public float a;
        public float b;
    }

    Bucket fillable;
    [SerializeField] float fillPerSecond;
    [SerializeField] List<Arc> _inputArcs;
    bool _active;

    private void Awake()
    {
        fillable = GetComponentInParent<Bucket>();
    }

    void Update() {
        CheckIfFillerIsActive();
        Fill(fillPerSecond * Time.deltaTime);
    }

    public void Fill(float amount)
    {
        if (_active) { 
		    fillable.Fill(amount);
		}
    }

    bool CheckIfFillerIsActive()
    {
        if (Rain.Instance) { 
			float rainAngle = Rain.Instance.GetAngle();
			foreach (Arc arc in _inputArcs) { 
				if(arc.a < rainAngle && arc.b > rainAngle) {
                    _active = true; ;
					return true;
				}
		    }
    	}
        _active = false;
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        if (_inputArcs == null) {
            return;
       	}
        Color[] colors = { Color.cyan, Color.green, Color.red, Color.yellow };
        int colorIndex = 0;
        foreach(Arc arc in _inputArcs) {
            Gizmos.color = colors[colorIndex];
            Vector3 aVec = Quaternion.Euler(new Vector3(0, 0, arc.a)) * Vector3.right;
            Vector3 bVec = Quaternion.Euler(new Vector3(0, 0, arc.b)) * Vector3.right;
            Gizmos.DrawLine(transform.position, transform.position + aVec * 2f);
            Gizmos.DrawLine(transform.position, transform.position + bVec * 2f);
            colorIndex++;
            colorIndex %= colors.Length;
    	}
    }

    public bool IsActive()
    {
        return _active;
    }
}
