using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcesUI : MonoBehaviour
{
    public TextMeshProUGUI sheepText;
    public TextMeshProUGUI landText;
    public TextMeshProUGUI moneyText;
    
    void Update()
    {
        sheepText.text = "Sheep: " + MainSystem.sheep;
        landText.text = "Land: " + MainSystem.land;
        moneyText.text = "Money: $" + MainSystem.money;
    }
}
