using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More BuildManager in Scenr");
            return;
        }
        instance = this;
    }

    //public GameObject standardTurretPrefab;

    //public GameObject missileLauncherPrefab;

    private TurretBlueprint turretToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;


    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to build that..");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;

        GameObject turret =(GameObject)  Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Turret builded! Money left: " + PlayerStats.Money.ToString());
    }

    public void SelectedNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
            
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }
}
