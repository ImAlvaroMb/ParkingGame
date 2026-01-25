using System.Collections.Generic;
using UnityEngine;
using Utilities;
public class AllLevelsController : AbstractSingleton<AllLevelsController>
{
    [SerializeField] private List<IndividualLevelController> levelsList = new List<IndividualLevelController>();
    private IndividualLevelController _activeLevel;

    public void RegisterIndividualLevel(IndividualLevelController levelController)
    {
        if(!levelsList.Contains(levelController)) levelsList.Add(levelController);

        if(levelController.LevelID == 0)
        {
            _activeLevel = levelController;
        } else
        {
            levelController.SetLevelInnactive();
        }
    }

    public void SwitchLevel(int targetID)
    {
        IndividualLevelController nextLevel = levelsList.Find(l => l.LevelID == targetID);

        if (nextLevel == null || _activeLevel == nextLevel) return;

        if (_activeLevel != null) _activeLevel.SetLevelInnactive();

        _activeLevel = nextLevel;
        _activeLevel.SetLevelActive();
    }

    [ContextMenu("Test0")] 
    public void Test0()
    {
        SwitchLevel(0);
    }
    [ContextMenu("Test1")]
    public void Test1()
    {
        SwitchLevel(1);
    }
    [ContextMenu("Test2")]
    public void Test2()
    {
        SwitchLevel(2);
    }
    [ContextMenu("Test3")]
    public void Test3()
    {
        SwitchLevel(3);
    }
}
