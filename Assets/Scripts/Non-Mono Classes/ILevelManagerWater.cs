using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelManagerWater
{
    void SetWaterInPool(int amount);
    void SetEvaporatedWater(int amount);

    int GetWaterInPool();
    int GetEvaporatedWater();
    int GetWaterNeededInPool();
    int GetEvaporationNeeded();

}
