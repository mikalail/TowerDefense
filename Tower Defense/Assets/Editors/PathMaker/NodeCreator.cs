/*  How to make this work:
 *  =======================
 *  
 *  what you need:
 *  (1) a Folder called "Resources" added to the "Assets" folder with your desired prefab in it - name the prefab "Node"
 *  (2) Add the "Editor" folder to your Assets folder - this contains the scripts 
 *  (3) an empty GameObject in the Hierarchy
 *  
 *  What you do:
 *  (1) Attach the script "NodeGroup.cs" to the empty GameObject in the Hierarchy.
 *  (2) Select the EmptyGameObject
 *  (3) In the Editor Window, hold alt and click to add clones of the prefab to the world.
 *  (4) Deselect the EmptyGameObject to stop
 * 
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Path))]
public class ObjectGroupInspector : Editor
{

    void OnSceneGUI()
    {
        // If we are in edit mode, but not in Play mode, and the user clicks (right click, middle click or alt+left click)
        if (Application.isEditor && !Application.isPlaying)
        {

            if (Event.current.type == EventType.MouseUp)
            {

                // Shoot a ray from the mouse position into the world
                Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hitInfo;

                // Shoot this ray. check in a distance of 10000.
                if (Physics.Raycast(worldRay, out hitInfo, 10000))
                {
                    // Load the current prefab
                    GameObject prefab = Resources.Load("Node") as GameObject;
                    GameObject anchor_point = prefab;

                    // Instance this prefab
                    GameObject prefab_instance = Instantiate(prefab);

                    // Place the prefab at correct position (position of the hit).
                    prefab_instance.transform.position = hitInfo.point;
                    prefab_instance.transform.SetParent(Selection.gameObjects[0].transform);

                    


                    // Mark the instance as dirty because we like dirty **** <== This is SUPPOSED to create an "undo" state.  However, this does not 
                    //                                                           work with the current version of Unity (Different command(s))
                    //EditorUtility.SetDirty(prefab_instance);
                }
            }
            // Mark the event as used
            Event.current.Use();
        } // End if  (if it is not in Edit mode or it Is in play mode).
    } // End OnSceneGUI
}
