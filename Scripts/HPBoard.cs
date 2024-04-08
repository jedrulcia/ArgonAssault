using TMPro;
using UnityEngine;

public class HPBoard : MonoBehaviour
{
    TMP_Text healthText;

    void Start()
    {
        healthText = GetComponent<TMP_Text>();
        healthText.text = "HP: 10";
    }

    public void UpdateHealth(int health)
    {
        healthText.text = $"HP: {health}";
    }
}
