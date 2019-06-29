using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelManagerCondensation
{
    void SetCondensedVapour(int amount);
    void ChangeCloudColour();

    int GetCondensedVapour();
    int GetCondensedVapourNeeded();
}
