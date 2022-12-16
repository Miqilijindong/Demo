using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTriggerClass :MonoBehaviour
{
    public EventTrigger SpeedUPEventTrigger;

    private void Start()
    {
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerDown;
        entry1.callback = new EventTrigger.TriggerEvent();
        entry1.callback.AddListener(OnClickDown);
        SpeedUPEventTrigger.triggers.Add(entry1);
    }

    public void OnClickDown(BaseEventData baseEventData)
    {
        Debug.Log("OnClickDown");
    }
}
