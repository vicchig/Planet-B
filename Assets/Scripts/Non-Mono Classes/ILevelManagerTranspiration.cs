using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelManagerTranspiration
{
    
    void SetTranspirationAmnt(int amount);

    int GetTranspirationAmountNeeded();
    int GetTranspirationAmount();

}
