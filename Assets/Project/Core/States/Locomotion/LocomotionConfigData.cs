using UnityEngine;

[CreateAssetMenu(fileName = "LocomotionConfigData", menuName = "Scriptable Objects/Locomotion Config")]
public class LocomotionConfigData : ScriptableObject
{
    public float WalkSpeed;
    public float SprintSpeed;
    public float RotationSpeed;
}
