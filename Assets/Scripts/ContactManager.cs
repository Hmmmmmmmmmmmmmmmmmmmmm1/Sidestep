using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Collections.LowLevel.Unsafe;
    public struct ContactManager
    {
        public void PreventRotation(PhysicsScene scene, NativeArray<ModifiableContactPair> contactPairs)
        {
            for (int i = 0; i < contactPairs.Length; ++i)
		    {
			    var pair = contactPairs[i];
                pair.rotation = Quaternion.Euler(
                    new Vector3(
                            (Mathf.Abs(GroundChecker.Item2.normal.x) > GroundChecker.Item2.normal.z && Mathf.Abs(GroundChecker.Item2.normal.x) > GroundChecker.Item2.normal.y) ? tra.localEulerAngles.x : GroundChecker.Item2.normal.z * 90,
                            (Mathf.Abs(GroundChecker.Item2.normal.y) > GroundChecker.Item2.normal.z && Mathf.Abs(GroundChecker.Item2.normal.y) > GroundChecker.Item2.normal.x) ? tra.localEulerAngles.y : GroundChecker.Item2.normal.y,
                            (Mathf.Abs(GroundChecker.Item2.normal.z) > GroundChecker.Item2.normal.y && Mathf.Abs(GroundChecker.Item2.normal.z) > GroundChecker.Item2.normal.x) ? tra.localEulerAngles.z : GroundChecker.Item2.normal.x * -90
                            )
                            
                );
            }
        }

    }


