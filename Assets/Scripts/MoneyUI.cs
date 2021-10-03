using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    private Text moneyText;

    void Start()
    {
        moneyText = GetComponent<Text>();
    }

    void Update()
    {
        moneyText.text = "$" + PlayerStats.Money.ToString();
    }
}
