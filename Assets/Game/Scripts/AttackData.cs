using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Combat/AttackData")]
public class AttackData : ScriptableObject
{
    public int Id;
    public string AttackName;
    public AnimationClip AnimationClip;
    public float Damage;
    public float ComboWindowStart;
    public float ComboWindowEnd;
    public AttackData NextAttackData;
    public bool UsesRootMotion;
}
