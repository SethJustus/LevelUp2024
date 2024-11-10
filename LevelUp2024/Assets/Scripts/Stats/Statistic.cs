using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Statistic", menuName = "Statistic")]
public class Statistic : ScriptableObject
{
    public int Experience;

    public float TickRateSeconds = 1;

    public bool SyncTickRate = false;
    
    private bool IsTicking = false;

    public void OnEnable()
    {
        this.IsTicking = false;
    }

    public void TryGainExperience(int amount)
    {
        // Don't start multiple tick coroutines at the same time
        if (this.IsTicking) return;
        GameManager.Instance.StartCoroutine(TickCoroutine(amount));
    }

    private void GainExperience(int amount)
    {
        this.Experience += amount;

        if (this.SyncTickRate)
        {
            this.TickRateSeconds = this.Experience / 100f;
        }
    }

    private IEnumerator TickCoroutine(int amount)
    {
        this.IsTicking = true;
        yield return new WaitForSeconds(TickRateSeconds);
        this.GainExperience(amount);
        this.IsTicking = false;
    }
}