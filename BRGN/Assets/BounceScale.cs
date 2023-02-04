using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScale : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] float scaleFactor;

    Coroutine _activeRoutine;

    public void DoIt() {
        if(_activeRoutine != null) { 
			StopCoroutine(_activeRoutine);
		}
        _activeRoutine = StartCoroutine(ScaleUp());
    }

    IEnumerator ScaleUp() {
        float elapsedTime = 0;
        while (elapsedTime < duration/2f) {
            transform.localScale = transform.localScale + Vector3.one * scaleFactor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _activeRoutine = StartCoroutine(ScaleDown());
    }

    IEnumerator ScaleDown() {
        float elapsedTime = 0;
        while (elapsedTime < duration/2f) {
            transform.localScale = transform.localScale - Vector3.one * scaleFactor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
