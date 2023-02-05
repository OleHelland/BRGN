using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParentBeatFollower : MonoBehaviour, IBeatFollower
{

    [SerializeField] UnityEvent _action;

    public void OnBeat()
    {
        _action?.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        IBeatSender beatSender = GetComponentInParent<IBeatSender>();
        if(beatSender == null) {
            Debug.LogWarning("GameObject " + gameObject.name + " can't find parent BeatSender. Disabling component.");
            this.enabled = false;
            return;
	    }

        beatSender.RegisterListener(this);

    }
}
