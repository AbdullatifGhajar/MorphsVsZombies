using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    public GameObject buildEffect;
    public GameObject sellEffect;

    private TurretBlueprint turretToBuild;
    public Turret selectedTurret;

    public NodeUI nodeUI;

    public bool aboutToBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void SelectTurret(Turret turret)
    {
        DeselectTurret();

        selectedTurret = turret;
        turretToBuild = null;

        nodeUI.Show();

        turret.getSelected();
    }

    public void DeselectTurret()
    {
        if (!selectedTurret)
            return;

        nodeUI.Hide();
        selectedTurret.getDeselected();
        selectedTurret = null;
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectTurret();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

}
