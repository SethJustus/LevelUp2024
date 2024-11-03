using UnityEngine;
using UnityEngine.UI;

public class TestHealthBar : MonoBehaviour, IHealthBar
{
        [SerializeField] private Image HealthBarSprite;
        
        public void UpdateUI(int health, int maxHealth)
        {
                HealthBarSprite.fillAmount = (float)health / (float)maxHealth;
        }

}
