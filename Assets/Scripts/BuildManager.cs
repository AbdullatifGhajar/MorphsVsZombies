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

    public AudioClip buyClip;
    public AudioClip noMoneyClip;

    [HideInInspector]
    public Turret turretToBuild;
    [HideInInspector]
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

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            Reset();
    }

    public void Reset()
    {
        UnbuyTurret();
        DeselectTurret();
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
        if (PlayerStats.Money < turret.cost)
        {
            Debug.Log("Not enough money to build that!");
            GameObject.Find("Money").GetComponent<Shaking>().Shake();
            GetComponent<AudioSource>().PlayOneShot(noMoneyClip);
            return;
        }
        DeselectTurret();

        PlayerStats.Money -= turret.cost;

        turretToBuild = Instantiate(turret.prefab, transform.position, Quaternion.identity).GetComponent<Turret>();
        turretToBuild.blueprint = turret;
        turretToBuild.enabled = false;
    }

    public void BuildTurretOn(Node node)
    {
        GameObject effect = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        turretToBuild.enabled = true;

        Vector3 distanceToRoot = turretToBuild.transform.position - turretToBuild.transform.Find("ROOT").position;
        turretToBuild.transform.position = node.GetBuildPosition() + distanceToRoot;

        node.turret = turretToBuild;
        turretToBuild = null;

        GetComponent<AudioSource>().PlayOneShot(buyClip);

        Debug.Log("Turret build!");
    }

    public void UnbuyTurret()
    {
        if (turretToBuild)
        {
            PlayerStats.Money += turretToBuild.cost;
            Destroy(turretToBuild.gameObject);
            turretToBuild = null;
        }
    }

}
