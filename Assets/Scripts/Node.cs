using UnityEngine;

public class Node : MonoBehaviour
{
    public Color startColor;
    public Color hoverColor;
    public Color canBuildColor;
    public Color cantBuildColor;

    public Turret turret;

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
        Bounds bounds = GetComponent<Renderer>().bounds;
        return bounds.center + new Vector3(0f, bounds.extents.y / 2, 0f);
    }

    void OnMouseDown()
    {
        if (turret)
            buildManager.SelectTurret(turret);
        else if (buildManager.aboutToBuild)
            BuildTurret(buildManager.GetTurretToBuild());
        // TODO deselect otherwise, but do actions with node ui
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

        Transform blueprintTransform = blueprint.prefab.transform;
        Vector3 distanceToRoot = blueprintTransform.position - blueprintTransform.Find("ROOT").position;
        turret = Instantiate(blueprint.prefab, GetBuildPosition() + distanceToRoot, Quaternion.identity).GetComponent<Turret>();
        turret.blueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret build!");
        // TODO remove tower blueprint in hand
    }


    bool canBuildOn()
    {
        return !turret && buildManager.HasMoney;
    }

    void OnMouseEnter()
    {
        if (buildManager.aboutToBuild && canBuildOn())
            rend.material.color = canBuildColor;

        else if (buildManager.aboutToBuild && !canBuildOn())
            rend.material.color = cantBuildColor;

        else // just hovering
            rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

}
