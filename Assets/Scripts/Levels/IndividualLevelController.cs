using UnityEngine;

public class IndividualLevelController : MonoBehaviour
{
    public int LevelID;
    public Transform InitialCameraPosition;
    private void Start()
    {
        AllLevelsController.Instance.RegisterIndividualLevel(this);
    }
    public void SetLevelActive() // will enable all visual elements that are optimized, load them and return something when finished
    {

    }

    public void SetLevelInnactive() // will enable all the optimizations needed
    {

    }

}
