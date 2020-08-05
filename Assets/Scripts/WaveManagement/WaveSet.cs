using UnityEngine;

[CreateAssetMenu(fileName = "WaveSet", menuName = "Wave Management/WaveSet")]
public class WaveSet : ScriptableObject
{
    public Wave[] waves;
}