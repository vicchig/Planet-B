using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBarRandomizer : MonoBehaviour
{
    private int loadBarValue;


    // Start is called before the first frame update
    void Start()
    {
        loadBarValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        loadBarValue++;
    }

    public int getLoadBarVal() {
        return loadBarValue;
    }

    public void setLoadBarVal(int val) {
        loadBarValue = val;
    }
}
