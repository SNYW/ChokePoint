using TMPro;
using UnityEngine;

public class PlayerEconomy : MonoBehaviour
{
    public int currentResource;
    public static int currentResourceStatic;
    public int generationRate;
    public float generationCooldown;
    public TextMeshProUGUI currentText;
    public TextMeshProUGUI rateText;
    protected float currentGenerationCooldown;

    private void Start()
    {
        currentGenerationCooldown = generationCooldown;
    }

    protected void GenerateResource(int amount)
    {
        currentResource += amount;
    }

    protected void UpdateText()
    {
        currentText.text = currentResource.ToString();
        rateText.text = "+"+generationRate.ToString();
    }
}
