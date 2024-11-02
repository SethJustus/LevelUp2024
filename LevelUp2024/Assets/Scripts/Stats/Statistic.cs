using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[CreateAssetMenu(fileName = "New Statistic", menuName = "New Statistic")]
public class Statistic : ScriptableObject
{
    public string Title;

    public int Experience;
}