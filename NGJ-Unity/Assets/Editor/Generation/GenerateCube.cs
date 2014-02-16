using UnityEngine;
using UnityEditor;
using System.Collections;

public class GenerateCube : EditorWindow 
{
    int iDimensions;
    float fScale;

    [MenuItem("Window/Generate Cube")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(GenerateCube));
    }

    public void OnGUI()
    {
        GUILayout.Label("Generate Cube", EditorStyles.boldLabel);
        iDimensions = EditorGUILayout.IntField("Dimension", iDimensions);
        fScale = EditorGUILayout.FloatField("Scale", fScale);
        if (GUILayout.Button("My editor button"))
        {
            BuildCube();
        }
    }

    private void BuildCube()
    {
        GameObject newCube = new GameObject();
        newCube.transform.position = Vector3.zero;

        int minDim = Mathf.FloorToInt(-iDimensions / 2);
        int maxDim = Mathf.CeilToInt(iDimensions / 2);

        Debug.Log("minDim: " + minDim + " maxDim: " + maxDim);

        for (int iDimX = minDim; (iDimensions % 2 != 0 ? iDimX <= maxDim : iDimX < maxDim ); ++iDimX)
        {
            for (int iDimY = minDim; (iDimensions % 2 != 0 ? iDimY <= maxDim : iDimY < maxDim); ++iDimY)
            {
                for (int iDimZ = minDim; (iDimensions % 2 != 0 ? iDimZ <= maxDim : iDimZ < maxDim); ++iDimZ)
                {
                    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    go.transform.parent = newCube.transform;
                    go.transform.localScale = new Vector3(fScale, fScale, fScale);
                    go.transform.position = new Vector3(iDimX * 0.5f, iDimY * 0.5f, iDimZ * 0.5f);
                }
            }
        }
    }

}
