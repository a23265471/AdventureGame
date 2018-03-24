using UnityEngine;

// This class represents a single outcome from clicking
// on an interactable.  It has an array of Conditions
// and if they are all met an ReactionCollection that
// will happen.
[System.Serializable]
public class ConditionCollection
{
    public string description;
    public Condition[] requiredConditions = new Condition[0];
    public ReactionCollection reactionCollection;
    public bool CheckAndReact()
    {
        for(int i = 0; i < requiredConditions.Length; i++)
        {
            if (AllConditions.CheckCondition(requiredConditions[i]) == false)
                return false;
        }
        if (reactionCollection)
        {
            reactionCollection.React();
        }
        return true;
    }
}