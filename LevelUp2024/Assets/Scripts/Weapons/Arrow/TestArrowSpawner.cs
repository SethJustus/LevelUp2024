using System.Collections;
using UnityEngine;

/// <summary>
/// This is just a test class to help me test arrows
/// </summary>
public class TestArrowSpawner : MonoBehaviour
{
    #region Fields
    private HealthObject _healthObject;
    #endregion
    
    #region Parameters
    [SerializeField] private float TimeBetweenSpawns;
    [SerializeField] private GameObject ArrowPrefab;
    #endregion
    
    void Awake()
    {
        _healthObject = this.GetComponent<HealthObject>();
        this.StartCoroutine(this.SpawnArrows());
    }
    
    IEnumerator SpawnArrows()
    {
        while (true)
        {
            var arrow = Instantiate(ArrowPrefab, this.transform.position, this.transform.rotation);
            var arrowScript = arrow.GetComponent<Arrow>();
            arrowScript.Initialize(Vector2.up, _healthObject);
            yield return new WaitForSeconds(this.TimeBetweenSpawns);
        }
    }

}
