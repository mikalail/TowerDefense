using UnityEngine;

public class shop : MonoBehaviour
{

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void PurchaseSingleCannon()
    {
        Debug.Log("Single Cannon Tower Selected");
        buildManager.SetTowerToBuild(buildManager.singleCannonPrefab);
    }

    public void PurchaseDoubleCannon()
    {
        Debug.Log("Double Cannon Tower Selected");
        buildManager.SetTowerToBuild(buildManager.doubleCannonPrefab);
    }

    public void PurchaseSniperCannon()
    {
        Debug.Log("Sniper Cannon Tower Selected");
        buildManager.SetTowerToBuild(buildManager.sniperCannonPrefab);
    }

}