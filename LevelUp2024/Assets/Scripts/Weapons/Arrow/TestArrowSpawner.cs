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
    
    #region Unity Methods
    void Awake()
    {
        // base awake method seems not to be called 
        base.OnAwake();
        this.StartCoroutine(this.SpawnArrows());
    }
    #endregion
    
    #region Methods
    protected override void Die()
    {
        Destroy(this.gameObject);
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
    #endregion

}
