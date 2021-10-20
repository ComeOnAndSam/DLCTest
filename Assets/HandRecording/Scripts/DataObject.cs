using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataObject
{
    public long timeStamp;
    public List<Quaternion> bone_rots = new();
    public Vector3 hand_pos;
    public Quaternion hand_rot;
}
