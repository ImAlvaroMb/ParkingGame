using UnityEngine;

public class IndividualLevelController : MonoBehaviour
{
    public int LevelID;
    public Transform InitialCameraPosition;
    public CameraMovementController CameraRef;
    private void Start()
    {
        AllLevelsController.Instance.RegisterIndividualLevel(this);
    }
    public void SetLevelActive() // will enable all visual elements that are optimized, load them and return something when finished
    {
        CameraRef.gameObject.SetActive(true);
        CameraRef.StartTransitionBetweenLevels();
    }

    public void SetLevelInnactive() // will enable all the optimizations needed
    {
        CameraRef.gameObject.SetActive(false);
    }

}
