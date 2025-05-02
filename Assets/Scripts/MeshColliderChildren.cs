using UnityEngine;
using UnityEditor;

public class AddMeshColliders : MonoBehaviour
{
    [MenuItem("Tools/Add MeshColliders to Selected GameObject and Children")]
    static void AddMeshCollidersToSelection()
    {
        GameObject selected = Selection.activeGameObject;
        if (selected == null)
        {
            Debug.LogWarning("No GameObject selected.");
            return;
        }

        int added = 0;
        foreach (Transform t in selected.GetComponentsInChildren<Transform>())
        {
            MeshFilter mf = t.GetComponent<MeshFilter>();
            if (mf != null && t.GetComponent<MeshCollider>() == null)
            {
                MeshCollider mc = t.gameObject.AddComponent<MeshCollider>();
                mc.sharedMesh = mf.sharedMesh;
                mc.convex = true; // optional: set to true if used with Rigidbody
                added++;
            }
        }

        Debug.Log($"Added {added} MeshColliders to {selected.name} and its children.");
    }
}
