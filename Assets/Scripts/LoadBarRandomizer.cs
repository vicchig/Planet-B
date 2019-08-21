using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBarRandomizer : MonoBehaviour
{
    private int loadBarValue;

    private float x;
    private float timeStep;

    private float maxXStep;
    private float minXStep;
    private float maxTimeStep;
    private float minTimeStep;
    private float initialLoadStep;
    private float loadedAtVal;
    private bool continuousLoad;
    private float continuousLoadStep;
    private bool getContinuousLoadStep;
    private float y;


    private LoadScreenManager manager;

    void Start()
    {
        loadBarValue = 0;
        timeStep = 0;
        x = 0;
        manager = transform.parent.transform.parent.GetComponent<LoadScreenManager>();

        maxXStep = 0.1f;
        minXStep = 0.02f;

        maxTimeStep = 0.1f;
        minTimeStep = 0.02f;

        initialLoadStep = Random.Range(10, 20);//the value up to which the load happens using loadVal++
        loadedAtVal = Random.Range(95, 99);//value after which the level is considered loaded
    }

    void Update()
    {
        if (!manager.isLoaded())
        {
            if (loadBarValue >= initialLoadStep)
            {
                if (!continuousLoad && ((loadBarValue <= 45 && loadBarValue >= 33) || (loadBarValue <= 74 && loadBarValue >= 88))) {
                    continuousLoad = (Mathf.RoundToInt(Random.Range(0, 100)) <= 20) ? true : false;
                    if (continuousLoad) {
                        getContinuousLoadStep = true;
                    }
                }

                if (!continuousLoad) {
                    if (timeStep <= 0)
                    {
                        x += Random.Range(minXStep, maxXStep);
                        timeStep = Random.Range(minTimeStep, maxTimeStep);

                        y = valueFunction(x);
                        loadBarValue = Mathf.RoundToInt(y * 100);
                    }
                    else
                    {
                        timeStep -= Time.unscaledDeltaTime;
                    }

                    if (loadBarValue >= loadedAtVal)
                    {
                        loadBarValue = 100;
                    }
                    else if (loadBarValue >= initialLoadStep && loadBarValue < 75)
                    {
                        maxXStep = 0.3f;
                        minXStep = 0.1f;

                        maxTimeStep = 2f;
                        minTimeStep = 1f;
                    }
                    else
                    {
                        maxXStep = 1f;
                        minXStep = 0.5f;

                        maxTimeStep = 0.3f;
                        minTimeStep = 0.1f;
                    }
                }
                else {
                    if (getContinuousLoadStep) {
                        continuousLoadStep = Mathf.RoundToInt(Random.Range(7, 10));
                        getContinuousLoadStep = false;
                    }

                    if (continuousLoadStep > 0)
                    {
                        loadBarValue++;
                        y += 0.01f;
                        x = inverseValueFunction(y);
                        continuousLoadStep--;
                    }
                    else {
                        getContinuousLoadStep = true;
                        continuousLoad = false;
                    }
                }
            }
            else {

                loadBarValue++;
                y += 0.01f;
                x = inverseValueFunction(y);
            }
        }
    }

    public int getLoadBarVal() {
        return loadBarValue;
    }

    public void setLoadBarVal(int val) {
        loadBarValue = val;
    }


    private float valueFunction(float x) {
        return 2 / Mathf.PI * Mathf.Atan((Mathf.PI / 2) * x);
    }

    private float inverseValueFunction(float y) {
        return 2 / Mathf.PI * Mathf.Tan(Mathf.PI / 2 * y);
    }
    
}
