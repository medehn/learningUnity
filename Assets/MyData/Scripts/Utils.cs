using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils  {

    //to see where the collider is even if not focused, draw the outlines into the scene view - doesnt affect actual game! parameter monobehaviour, das boxcollider als geschwister hat
    public static void DrawBoxCollider(MonoBehaviour mb, Color color) {
        if (UnityEditor.Selection.activeGameObject != mb.gameObject)
        {
            BoxCollider bc = mb.GetComponent<BoxCollider>();

            if (bc == null)
                return;
              
            Gizmos.color = color;
            Matrix4x4 oldMatrix = Gizmos.matrix;
            Gizmos.matrix = mb.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(bc.center, bc.size);
            Gizmos.matrix = oldMatrix;
        }
    }
}
