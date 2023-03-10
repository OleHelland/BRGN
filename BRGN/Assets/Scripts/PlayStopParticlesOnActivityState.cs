using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStopParticlesOnActivityState : MonoBehaviour, IActiveStateListener
{
    [SerializeField] ParticleSystem _target;
    [SerializeField] GameObject _activityCommunicatorObject;
    ICommunicateActiveState _activityCommunicator;
    [SerializeField] bool inverse;

    bool _lastState = false;

    void OnEnable() {
        _activityCommunicator = _activityCommunicatorObject.GetComponent<ICommunicateActiveState>();
        UpdateActive();
    }

    void Update() { 
        if(_lastState != _activityCommunicator.IsActive()) {
            UpdateActive();
		}
    }

    public void UpdateActive()
    {
        _lastState = _activityCommunicator.IsActive();
        if(_lastState && !inverse) { 
			_target.Play();
    	}
        else { 
			_target.Stop();
		}
    }
}
