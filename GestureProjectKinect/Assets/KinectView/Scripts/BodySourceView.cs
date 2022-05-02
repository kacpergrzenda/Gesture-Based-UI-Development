using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class BodySourceView : MonoBehaviour 
{
    //public Material BoneMaterial;
    public GameObject BodySourceManager;

    public GameObject mJointObject;
    
    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;

    //private Dictionary<ulong, GameObject> mBodies = new Dictionary<ulong, GameObject>();
    private List<Kinect.JointType> _joints = new List<Kinect.JointType>
    {
        Kinect.JointType.HandLeft,
        Kinect.JointType.HandRight
    };
    
    // private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    // {
    //     { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
    //     { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
    //     { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
    //     { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },
        
    //     { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
    //     { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
    //     { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
    //     { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },
        
    //     { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
    //     { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
    //     { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
    //     { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
    //     { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
    //     { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },
        
    //     { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
    //     { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
    //     { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
    //     { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
    //     { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
    //     { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },
        
    //     { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
    //     { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
    //     { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
    //     { Kinect.JointType.Neck, Kinect.JointType.Head },
    // };
    
    void Update () 
    {
        if (BodySourceManager == null)
        {
            return;
        }
        
        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }
        
        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }
        
        List<ulong> trackedIds = new List<ulong>();
        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
              }
                
            if(body.IsTracked)
            {
                trackedIds.Add (body.TrackingId);
            }
        }
        
        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);
        
        // First delete untracked bodies
        foreach(ulong trackingId in knownIds)
        {
            if(!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
            }
            
            if(body.IsTracked)
            {
                if(!_Bodies.ContainsKey(body.TrackingId))
                {
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }
                
                RefreshBodyObject(body, _Bodies[body.TrackingId]);
            }
        }
    }
    
    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);

        foreach(Kinect.JointType joint in _joints)
        {
            //Create Object 
            GameObject newJoint = Instantiate(mJointObject);
            newJoint.name = joint.ToString();
            
            // Parent to body
            newJoint.transform.parent = body.transform;
        }
        
        
        return body;
    }
    
    private void RefreshBodyObject(Kinect.Body body, GameObject bodyObject)
    {
        // for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        // {
        //     Kinect.Joint sourceJoint = body.Joints[jt];
        //     Kinect.Joint? targetJoint = null;
            
        //     if(_BoneMap.ContainsKey(jt))
        //     {
        //         targetJoint = body.Joints[_BoneMap[jt]];
        //     }
            
        //     Transform jointObj = bodyObject.transform.Find(jt.ToString());
        //     jointObj.localPosition = GetVector3FromJoint(sourceJoint);
            
        //     LineRenderer lr = jointObj.GetComponent<LineRenderer>();
        //     if(targetJoint.HasValue)
        //     {
        //         lr.SetPosition(0, jointObj.localPosition);
        //         lr.SetPosition(1, GetVector3FromJoint(targetJoint.Value));
        //         lr.SetColors(GetColorForState (sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));
        //     }
        //     else
        //     {
        //         lr.enabled = false;
        //     }
        // }

        foreach ( Kinect.JointType _joint in _joints)
        {
            // Get new target position
            Kinect.Joint sourceJoint = body.Joints[_joint];
            Vector3 targetPosition = GetVector3FromJoint(sourceJoint);
            targetPosition.z = 0;

            // Get joint, set new position
            Transform jointObject = bodyObject.transform.Find(_joint.ToString());
            jointObject.position = targetPosition;
        }
    }
    
    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
        case Kinect.TrackingState.Tracked:
            return Color.green;

        case Kinect.TrackingState.Inferred:
            return Color.red;

        default:
            return Color.black;
        }
    }
    
    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }
}
