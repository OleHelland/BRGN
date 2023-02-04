using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScale : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] float scaleFactor;

    public void DoIt() {
        StartCoroutine(ScaleUp());
    }

    IEnumerator ScaleUp() {
        float elapsedTime = 0;
        while (elapsedTime < duration/2f) {
            transform.localScale.Scale(Vector3.one * scaleFactor);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(ScaleDown());
    }

    IEnumerator ScaleDown() {
        float elapsedTime = 0;
        while (elapsedTime < duration/2f) {
            transform.localScale.Scale(Vector3.one * scaleFactor);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
