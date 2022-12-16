using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

public class TimeController : MonoBehaviour
{
    public static float gravity = 100;
    public struct RecordedData
    {
        public Vector2 pos;
        public Vector2 vel;
        public float animationTime;
    }

    /// <summary>
    /// 保存一定时间内所有物体的数组
    /// </summary>
    RecordedData[,] recordedDatas;
    int recordMax = 100000;
    int recordCount;
    int recordIndex;
    bool wasSteppingBack = false;

    TimeControlled[] timeObjects;

    // Start is called before the first frame update
    void Start()
    {
        timeObjects = GameObject.FindObjectsOfType<TimeControlled>();
        recordedDatas = new RecordedData[timeObjects.Length, recordMax];
    }

    // Update is called once per frame
    void Update()
    {
        // 暂停
        bool pause = Input.GetKey(KeyCode.UpArrow);
        // 回退
        bool stepBack = Input.GetKey(KeyCode.LeftArrow);
        // 前进
        bool stepForward = Input.GetKey(KeyCode.RightArrow);

        if (stepBack)
        {
            wasSteppingBack = true;
            if (recordIndex > 0)
            {
                recordIndex--;

                for (int objectIndex = 0; objectIndex < timeObjects.Length; objectIndex++)
                {
                    TimeControlled timeObject = timeObjects[objectIndex];
                    RecordedData data = recordedDatas[objectIndex, recordIndex];
                    timeObject.transform.position = data.pos;
                    timeObject.velocity = data.vel;
                    timeObject.animationTime = data.animationTime;

                    timeObject.UpdateAnimaiton();
                }
            }
        }
        else if (pause && stepForward)
        {
            wasSteppingBack = true;
            if (recordIndex < recordCount - 1)
            {
                recordIndex++;

                for (int objectIndex = 0; objectIndex < timeObjects.Length; objectIndex++)
                {
                    TimeControlled timeObject = timeObjects[objectIndex];
                    RecordedData data = recordedDatas[objectIndex, recordIndex];
                    timeObject.transform.position = data.pos;
                    timeObject.velocity = data.vel;
                    timeObject.animationTime = data.animationTime;

                    timeObject.UpdateAnimaiton();
                }
            }
        }
        else if (!pause && !stepBack)
        {
            if (wasSteppingBack)
            {
                recordIndex = recordCount;
                wasSteppingBack = false;
            }

            for (int objectIndex = 0; objectIndex < timeObjects.Length; objectIndex++)
            {
                TimeControlled timeObject = timeObjects[objectIndex];
                RecordedData data = new RecordedData();
                data.pos = timeObject.transform.position;
                data.vel = timeObject.velocity;
                data.animationTime = timeObject.animationTime;
                recordedDatas[objectIndex, recordCount] = data;
            }
            recordCount++;
            recordIndex = recordCount;

            foreach (TimeControlled timeObject in timeObjects)
            {
                timeObject.TimeUpdate();
                timeObject.UpdateAnimaiton();
            }
        }
    }
}
