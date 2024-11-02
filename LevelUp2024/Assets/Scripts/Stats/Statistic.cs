using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Statistic", menuName = "New Statistic")]
public class Statistic : ScriptableObject
{
    public int Experience;

    public float TickRateSeconds = 1;

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
        Debug.Log($"Gained {amount} Experience! New Experience: {this.Experience}");
    }

    private IEnumerator TickCoroutine(int amount)
    {
        Debug.Log("TickCoroutine started");
        this.IsTicking = true;
        yield return new WaitForSeconds(TickRateSeconds);
        this.GainExperience(amount);
        this.IsTicking = false;
    }
}