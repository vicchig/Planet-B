using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelManagerCondensation
{
    void SetCondensedVapour(int amount);

    int GetCondensedVapour();
    int GetCondensedVapourNeeded();
}
