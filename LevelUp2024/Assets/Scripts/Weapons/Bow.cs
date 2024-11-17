using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    #region Parameters
    [SerializeField] private GameObject ArrowPrefab;

    [SerializeField] private HealthObject Player;
    #endregion
    
    #region Properties
    public bool IsEquipped { get; set; }
    #endregion
    
    #region Unity Methods

    void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calculate the direction from the GameObject to the mouse position
        Vector3 direction = mousePosition - transform.position;
        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Set the rotation of the GameObject
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    #endregion
    
    #region Methods
    public void Attack()
    {
        // Spawn Arrow
        var arrow = Instantiate(ArrowPrefab, this.transform.position, this.transform.rotation);
        var arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.Initialize(Vector2.up, Player);
    }
    #endregion
}
