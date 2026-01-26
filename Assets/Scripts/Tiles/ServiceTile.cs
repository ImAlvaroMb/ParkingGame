using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Enums;

public class ServiceTile : BaseTile
{
    public float ServiceTime = 2f;
    private ITimer _serviceTimer;
    public override void RefreshConnections(Dictionary<Vector2Int, BaseTile> grid)
    {
        
    }

    public void StartService(Car car)
    {
        if(_serviceTimer != null)
        {
            InternalNodes[0].SetIsOccupied(true); // this will be changed to occuping the desired spot
            _serviceTimer = TimerSystem.Instance.CreateTimer(ServiceTime, onTimerDecreaseComplete: () =>
            {
                InternalNodes[0].SetIsOccupied(false);
            }, onTimerDecreaseUpdate: (progress) =>
            {
                //update visuals or something
            });
        }
    }
}
