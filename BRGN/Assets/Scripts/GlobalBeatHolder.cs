using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBeatHolder : MonoBehaviour, IBeatSender
{

    [SerializeField] float bpm;
    float _secondsSinceBeat;
    float _secondsPerBeat;

    static GlobalBeatHolder _instance;
    public static GlobalBeatHolder Instance { get { return _instance; } }

    List<IBeatFollower> beatFollowers;

    private void Awake()
    {
        if(_instance == null) {
            _instance = this;
	    }
        else {
            Debug.LogError("Attempting to start multiple global beat holders!");
	    }
        float beatsPerSecond = bpm / 60;
        _secondsPerBeat = 1/beatsPerSecond;
        beatFollowers = new List<IBeatFollower>();
    }

    private void Update()
    {
        _secondsSinceBeat += Time.deltaTime;
        if(_secondsSinceBeat > _secondsPerBeat) {
            _secondsSinceBeat -= _secondsPerBeat;
            SendBeat();
    	}
    }



    public void SendBeat()
    {
        foreach(IBeatFollower beatFollower in beatFollowers) {
            beatFollower.OnBeat();
	    }
    }

    public void RegisterListener(IBeatFollower follower)
    {
        beatFollowers.Add(follower);
    }

    public void UnRegisterListener(IBeatFollower follower)
    {
        beatFollowers.Remove(follower);
    }
}
