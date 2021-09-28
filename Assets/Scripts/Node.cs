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
            buildManager.BuildTurretOn(this);
        // TODO deselect otherwise, but do actions with node ui
    }

    void OnMouseEnter()
    {
        if (buildManager.aboutToBuild && !turret){
            rend.material.color = canBuildColor;
            buildManager.turretToBuild.transform.position = GetBuildPosition() + new Vector3(0f, 3f, 0f);
        }

        else if (buildManager.aboutToBuild && turret)
            rend.material.color = cantBuildColor;

        else // just hovering
            rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

}
