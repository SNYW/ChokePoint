using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

[CustomEditor(typeof(BuildGridManager))]
public class GridManagerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BuildGridManager buildGridManager = (BuildGridManager)target;
        if (GUILayout.Button("Generate Grid"))
        {
            buildGridManager.GenerateBuildGrid();
        }
        if (GUILayout.Button("Clear Grid"))
        {
            buildGridManager.ClearGrid();
        }
    }

}
