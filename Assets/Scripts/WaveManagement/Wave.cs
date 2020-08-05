using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName ="Wave Management/Wave")]
public class Wave : ScriptableObject
{
    public float timeToNext;
    public GameObject[] buildings;
}
