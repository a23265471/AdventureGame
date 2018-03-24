using System;
using UnityEngine;

// This script works as a singleton asset.  That means that
// it is globally accessible through a static instance
// reference.  
[CreateAssetMenu]
public class AllConditions:ResettableScriptableObject
{
    public Condition[] conditions;
    private static AllConditions instance;
    public static AllConditions Instance
    {
        get
        {
            if (!instance)
                instance = FindObjectOfType<AllConditions>();
            if (!instance)
                instance = Resources.Load<AllConditions>("AllConditions");
            if (!instance)
                Debug.LogError("AllCondition has not created yet!!");
            return instance; 
        }

    }
    public override void Reset()
    {
        if (conditions == null)
            return;
        for(int i = 0; i < conditions.Length; i++)
        {
            conditions[i].satisfied = false;

        }
    }

    public static bool CheckCondition(Condition requiredCondition)
    {
        Condition[] allCondition = Instance.conditions;
        Condition globalCondition = null;
        if(allCondition != null && allCondition[0] != null)
        {
            for(int i = 0; i < allCondition.Length; i++)
            {
                if (allCondition[i].description == requiredCondition.description)
                    globalCondition = allCondition[i];
            }
           
        }

        if (globalCondition == null)
            return false;
        return globalCondition.satisfied == requiredCondition.satisfied;
    }

    public static Condition GetCondition(Condition.ConditionName conditionName)
    {
        //for (int i = 0; i < Instance.conditions.Length; i++)
        //{
        //    if (Instance.conditions[i].description == conditionName)
        //        return Instance.conditions[i];
        //}
        //也可以寫成面那一行
       return Array.Find(Instance.conditions, x => x.description == conditionName);
    }

}
