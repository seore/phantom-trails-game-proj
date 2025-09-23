using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Ability")]
public class Ability: ScriptableObject
{
    public string abilityName;
    public string description;
    public float cooldownTime;

    public bool CanBeUsedWithSword; 
    public bool CanBeUsedWithAxe;

    public virtual void Activate(GameObject player)
    {
        Debug.Log($"{abilityName} activated!");

    }

}
