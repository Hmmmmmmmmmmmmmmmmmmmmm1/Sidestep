using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Collections;
    public struct ContactManager
    {
        public int playerID;
        public double B;
        public Vector3 Up;

        void Update()
        {
            Debug.Log("pluh");
        }
        
        public void PreventRotation(PhysicsScene scene, NativeArray<ModifiableContactPair> contactPairs)
        {
            
            for (int i = 4; i < contactPairs.Length; ++i)//remove all but the points we aclutally need
		    {
			    var pair = contactPairs[i];
                for (int j=0; j < pair.contactCount; j++)
                {
                    pair.IgnoreContact(j);
                }
            }

            for (int i = 0; i < contactPairs.Length; ++i)
            {
                var pair = contactPairs[i];
                (var player,var notplayer) = (playerID == pair.colliderInstanceID) ? (pair.position,pair.otherPosition): (pair.otherPosition,pair.position);//Find our points
                Vector3 normalline = player-notplayer;
                Vector3 cross = CrossVectors(normalline, Up);
                double distancerestraint = Mathf.Sqrt(((int)(cross.x)^2)+((int)(cross.y)^2)+((int)(cross.z)^2));
                double Xvar = (B*i*cross.x)/(contactPairs.Length*distancerestraint);
                double Yvar = (B*i*cross.y)/(contactPairs.Length*distancerestraint);
                double Zvar = (B*i*cross.z)/(contactPairs.Length*distancerestraint);
                Vector3 location = new Vector3((float)Xvar,(float)Yvar,(float)Zvar);//variable math it out
                for (int j=1; j < pair.contactCount; j++)
                {
                    pair.IgnoreContact(j);
                }
                pair.SetPoint(i, location);//Set points

            }
        }

        public Vector3 CrossVectors(Vector3 Vec1, Vector3 Vec2)
        {
            return new Vector3((Vec1.y)*(Vec2.z)-(Vec1.z)*(Vec2.y), (Vec1.z)*(Vec2.x)-(Vec1.x)*(Vec2.z), (Vec1.x)*(Vec2.y)-(Vec1.y)*(Vec2.x));
        }

    }


