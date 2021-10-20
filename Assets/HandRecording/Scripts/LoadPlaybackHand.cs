using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;

using Debug = UnityEngine.Debug;

public class LoadPlaybackHand : MonoBehaviour
{
    public TextAsset recordingToLoad;

    public bool playOnStart;
    public bool loopPlayback;

    [Range(0f, 100f)]
    public float playbackSpeed = 1;

    RecordedData dataFile;
    List<DataObject> data;

    CustomSkeleton playbackHand;
    const int num_bones = 19;

    Vector3 startPos;
    Quaternion startRot;

    bool playingBack;

    int invoke = 0;
    int maxInvokes = 0;

    List<long> timestamps;
    Stopwatch timer;

    // Start is called before the first frame update
    void Start()
    {
        playbackHand = gameObject.GetComponentInParent<CustomSkeleton>();
        startPos = playbackHand.transform.position;
        startRot = playbackHand.transform.rotation;

        LoadRecordingFromAsset();

        if (playOnStart)
            TogglePlayback();
    }

    public void TogglePlayback()
    {
        if (!playingBack)
        {
            if (data == null)
            {
                Debug.LogWarning("No data is stored, run a recording to get data.");
                return;
            }

            if (playbackHand == null)
            {
                Debug.LogError("No playback hand is set for this component.");
            }

            if (data.Count <= 0)
            {
                Debug.LogWarning("Nothing has been recorded");
                return;
            }

            maxInvokes = data.Count;
            
            invoke = 0;

            timestamps = new();
            foreach(DataObject d in dataFile.dataObjects)
            {
                timestamps.Add(d.timeStamp);
            }

            timer = new Stopwatch();
            timer.Start();

            playingBack = true;
        }
        else
        {
            timer.Stop();
            playingBack = false;
            if (loopPlayback)
            {
                TogglePlayback();
            }
            else
            {
                playbackHand.transform.SetPositionAndRotation(startPos, startRot);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playingBack)
        {
            float currentTime = timer.ElapsedTicks * playbackSpeed;
            bool timeUp = true;
            for(int i = invoke; i < timestamps.Count; i++)
            {
                if(timestamps[i] >= currentTime)
                {
                    invoke = i;
                    Playback();
                    timeUp = false;
                    break;
                }
            }

            //playback is done
            if (timeUp)
                TogglePlayback();
        }
        
        /*
        if (Input.GetButtonDown("Playback"))
            TogglePlayback();
        else if (Input.GetButton("Loop Off"))
            loopPlayback = false;
        else if (Input.GetButton("Loop On"))
            loopPlayback = true;*/
    }

    void Playback()
    {
        if (invoke >= maxInvokes)
        {
            //playback is done
            Debug.Log("Done playback");
            TogglePlayback();
            return;
        }

        playbackHand.transform.localPosition = data[invoke].hand_pos;
        playbackHand.transform.localRotation = data[invoke].hand_rot;
               
        //apply bone rotations loop
        for (var i = 2; i < num_bones; i++)
        {
            playbackHand.bones[i].localRotation = data[invoke].bone_rots[i];
        }
    }

    void LoadRecordingFromAsset()
    {
        dataFile = JsonUtility.FromJson<RecordedData>(recordingToLoad.text);
        if (dataFile == null)
        {
            Debug.LogError("No recording data found");
            return;
        }

        data = dataFile.dataObjects;
    }
}
