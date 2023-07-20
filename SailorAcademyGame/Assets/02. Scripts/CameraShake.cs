using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float dur, float mag) {
        Vector2 originalPos = transform.localPosition;
        float elapsed = 0;

        while (elapsed < dur) {
            float x = Random.Range(-1, 1) * mag;
            float y = Random.Range(-1, 1) * mag;

            transform.localPosition = new Vector2(x, y);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;

    }
}
