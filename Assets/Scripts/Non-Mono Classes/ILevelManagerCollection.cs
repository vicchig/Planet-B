using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelManagerCollection 
{
    void setWaterInPool1(int amount);
    void setWaterInPool2(int amount);

    int getWaterInPool1();
    int getWaterInPool2();
    int getWaterNeededInPool1();
    int getWaterNeededInPool2();
}
