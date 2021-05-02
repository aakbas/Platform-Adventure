using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsTracker : MonoBehaviour
{

    [SerializeField] string eventName;
    [SerializeField] int eventId;
    int CurrentLevel;

    private void Start()
    {
        CurrentLevel = FindObjectOfType<LevelTeleporter>().GetCurrentLevel();
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<TravelerMovement>())
        {
            ReportEventTrigger(eventId, eventName);
            Debug.Log("test basarili");
        }
    }

    public void ReportEventTrigger(int eventId,string eventName)
    {

        Analytics.CustomEvent(eventName, new Dictionary<string, object>
        {
            {"event_id",eventId },
            {"time_elapsed", Time.timeSinceLevelLoad }
        });                 
            
        }
   
    public void DeathEventTrigger()
    {

        Analytics.CustomEvent("Death Counts", new Dictionary<string, object>
        {
            {"Level",CurrentLevel },
            {"time_elapsed",Time.timeSinceLevelLoad }

        }
            );

    }
     
    public void SkillEventTrigger()
    {
        Analytics.CustomEvent("SkillCounts", new Dictionary<string, object>

        {
                {"Level", CurrentLevel }
        }   


            );
    }

}
