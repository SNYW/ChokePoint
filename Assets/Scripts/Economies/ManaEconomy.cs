using TMPro;
using UnityEngine;

public class ManaEconomy : PlayerEconomy
{
    public int storageMax;
    public TextMeshProUGUI storageText;
    private void Update()
    {
        ManageCooldown();
        UpdateText();
        UpdateStorageText();
    }
    private void ManageCooldown()
    {
        currentGenerationCooldown -= Time.deltaTime;
        if(currentGenerationCooldown <= 0)
        {
            currentGenerationCooldown = generationCooldown;
            if(currentResource + generationRate > storageMax)
            {
                currentResource = storageMax;
            }
            else
            {
                GenerateResource(generationRate);
            }
        }
    }
    private void UpdateStorageText()
    {
        storageText.text = storageMax.ToString();
    }
}
