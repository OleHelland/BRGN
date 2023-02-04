using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour, IFillable, IBeatSender, IBeatFollower
{

    [Serializable]
    public struct BeatOutput {
        public float activePercentage;
        public List<bool> activeBeats;
    }

    [SerializeField] List<BeatOutput> beatOutputs;
    [SerializeField] float MaxFill;
    [SerializeField] float StartFill;
    [SerializeField] float DrainPerSecond;
    float _currentFill;
    bool _active = false;
    BeatOutput _activeBeatOutput;

    List<IBeatFollower> beatFollowers;

    int beatIndex = 0;

    private void OnEnable()
    {
        _currentFill = StartFill;

        beatFollowers = new List<IBeatFollower>();
        GlobalBeatHolder.Instance.RegisterListener(this);
    }

    private void OnDisable()
    {
        GlobalBeatHolder.Instance.UnRegisterListener(this);
    }

    void Update()
    {
        Drain();
        UpdateActiveState();
    }

    void Drain() {
        if (_currentFill > 0) {
            _currentFill -= DrainPerSecond * Time.deltaTime;
	    }
        if(_currentFill < 0) {
            _currentFill = 0;
	    }
    }

    void UpdateActiveState()
    {
        BeatOutput selectedBeatOutput = default;
        foreach(BeatOutput beatOutput in beatOutputs)
	    {
            if (beatOutput.activePercentage > _currentFill) {
                continue;
	        }

            if (selectedBeatOutput.Equals(default(BeatOutput)))
	        {
                selectedBeatOutput = beatOutput;
                continue;
	        }
            if (selectedBeatOutput.activePercentage < beatOutput.activePercentage) {
                selectedBeatOutput = beatOutput;
	        }
	    }

        if (selectedBeatOutput.Equals(default(BeatOutput))) {
            _active = false;
        }
        else {
            _active = true;
	    }

        _activeBeatOutput = selectedBeatOutput;
    }

    public void Fill(float amount)
    {
        _currentFill += amount;
        if(_currentFill > MaxFill) {
            _currentFill = MaxFill;
    	}
    }

    public void SendBeat()
    {
        foreach(IBeatFollower beatFollower in beatFollowers) {
            beatFollower.OnBeat();
	    }
    }

    public void OnBeat()
    {
        if (_active && _activeBeatOutput.Equals(default(BeatOutput))) {
            if (_activeBeatOutput.activeBeats[beatIndex++]) {
                SendBeat();
	        }
			beatIndex %= _activeBeatOutput.activeBeats.Count;
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
