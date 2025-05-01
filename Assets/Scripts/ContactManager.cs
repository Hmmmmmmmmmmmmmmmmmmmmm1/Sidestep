using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Collections;
    public struct ContactManager
    {
        public int tra;
        
        public void PreventRotation(PhysicsScene scene, NativeArray<ModifiableContactPair> contactPairs)
        {
            for (int i = 0; i < contactPairs.Length; ++i)
		    {
			    var pair = contactPairs[i];
                /*pair.rotation = Quaternion.Euler(
                    new Vector3(
                            (Mathf.Abs(tra.up.x) > tra.up.z && Mathf.Abs(tra.up.x) > tra.up.y) ? tra.localEulerAngles.x : tra.up.z * 90,
                            (Mathf.Abs(tra.up.y) > tra.up.z && Mathf.Abs(tra.up.y) > tra.up.x) ? tra.localEulerAngles.y : tra.up.y,
                            (Mathf.Abs(tra.up.z) > tra.up.y && Mathf.Abs(tra.up.z) > tra.up.x) ? tra.localEulerAngles.z : tra.up.x * -90
                            )
                            
                );*/
            }
        }

    }


