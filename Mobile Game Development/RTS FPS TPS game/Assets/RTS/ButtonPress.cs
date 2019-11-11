using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour {
    public Button followButton;
    public Button patrolButton;
    public void OnFollowPress()
    {
        if(!AgentFollow.followPress)
        {
            AgentFollow.followPress = true;
            followButton.GetComponent<Image>().color = Color.red;
        }
        else
        {
            AgentFollow.followPress = false;
            followButton.GetComponent<Image>().color = Color.white;
        }
        
    }

    public void OnPatrolPress()
    {
        if(!AgentFollow.patrolPress)
        {
            AgentFollow.patrolPress = true;
            patrolButton.GetComponent<Image>().color = Color.red;
        }
        else
        {
            AgentFollow.patrolPress = false;
            patrolButton.GetComponent<Image>().color = Color.white;
        }
       
    }
}
