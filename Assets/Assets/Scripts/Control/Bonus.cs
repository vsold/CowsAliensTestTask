using System;
using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour
{
    public event Action<Bonus> Collected = delegate { };
    public event Action<Bonus> Destroyed = delegate { };
    
    private const float updateInterval = 1f;

    private GameSettings Settings
    {
        get
        {
            return ApplicationManager.Instance.Settings;
        }
    }

    private float LifeTIme
    {
        get
        {
            return Settings.BonusLifeTime;
        }
    }

    public void Start()
    {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        float startTime = Time.timeSinceLevelLoad;
        while (startTime + LifeTIme > Time.timeSinceLevelLoad)
        {
            yield return new WaitForSeconds(updateInterval);
        }

        Destroyed(this);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Dog")
            Collected(this);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
