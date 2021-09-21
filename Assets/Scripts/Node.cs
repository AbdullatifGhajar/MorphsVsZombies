using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color startColor;
    public Color hoverColor;
    public Color canBuildColor;
    public Color cantBuildColor;

    public GameObject turret;
    public TurretBlueprint turretBlueprint;
    public bool isUpgraded = false;

    private Renderer rend;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + new Vector3(0f, transform.lossyScale.y / 2, 0f);
    }

    void OnMouseDown()
    {
        if (turret != null)
        {

            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.aboutToBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            // TODO inform user of it
            return;
        }

        PlayerStats.Money -= blueprint.cost;
        turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret build!");
        // TODO remove tower blueprint in hand
    }

    // TODO deligate upgrading to tower, not node
    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            // TODO inform user of it
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        Destroy(turret);
        turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;

        Debug.Log("Turret upgraded!");
    }

    public void SellTurret()
    {
        // TODO SellAmount must come from turret, not the blueprint
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);
        turretBlueprint = null;
    }

    bool canBuildOn()
    {
        return buildManager.aboutToBuild && !turret && buildManager.HasMoney;
    }

    bool cantBuildOn()
    {
        return buildManager.aboutToBuild && (turret || !buildManager.HasMoney);
    }

    void OnMouseEnter()
    {
        if (canBuildOn())
            rend.material.color = canBuildColor;

        else if (cantBuildOn())
            rend.material.color = cantBuildColor;

        else // just hovering
            rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

}
