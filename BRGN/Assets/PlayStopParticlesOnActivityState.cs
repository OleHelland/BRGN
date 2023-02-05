using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStopParticlesOnActivityState : MonoBehaviour, IActiveStateListener
{
    [SerializeField] ParticleSystem _target;
    [SerializeField] ICommunicateActiveState _activityCommunicator;
    [SerializeField] bool inverse;

    bool _lastState = false;

    void OnEnable() {
        _activityCommunicator = GetComponentInParent<ICommunicateActiveState>();
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
