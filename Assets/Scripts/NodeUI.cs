using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject UI;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;

    private Turret turret { get { return BuildManager.instance.selectedTurret; } }

    public void Show()
    {
        transform.position = turret.transform.position;

        if (!turret.isUpgraded)
        {
            upgradeCost.text = "$" + turret.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + turret.GetSellAmount();

        UI.SetActive(true);
    }

    public void Hide()
    {
        UI.SetActive(false);
    }

    public void Upgrade()
    {
        if (PlayerStats.Money < turret.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            // TODO inform user of it
            return;
        }

        PlayerStats.Money -= turret.upgradeCost;

        turret.Upgrade();

        BuildManager.instance.DeselectTurret();
    }

    public void Sell()
    {
        PlayerStats.Money += turret.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(BuildManager.instance.sellEffect, turret.transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret.gameObject);

        BuildManager.instance.DeselectTurret();
    }

}
