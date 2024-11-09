using System.Collections;
using UnityEngine;

/// <summary>
/// This is just a test class to help me test arrows
/// </summary>
public class TestArrowSpawner : HealthObject
{
    #region Parameters
    [SerializeField] private float TimeBetweenSpawns;
    [SerializeField] private GameObject ArrowPrefab;
    #endregion
    
    void Awake()
    {
        this.StartCoroutine(this.SpawnArrows());
    }
    
    IEnumerator SpawnArrows()
    {
        while (true)
        {
            var arrow = Instantiate(ArrowPrefab, this.transform.position, this.transform.rotation);
            var arrowScript = arrow.GetComponent<Arrow>();
            arrowScript.Initialize(Vector2.up, this);
            yield return new WaitForSeconds(this.TimeBetweenSpawns);
        }
    }

}
