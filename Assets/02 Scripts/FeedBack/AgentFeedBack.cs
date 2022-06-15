using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentFeedBack : MonoBehaviour
{
    [SerializeField] private List<FeedBack> _feedbackToPlay = new List<FeedBack>();

    public void PlayFeedBack()
    {
        FinishFeedBack();
        foreach (FeedBack f in _feedbackToPlay)
        {
            f.CreateFeedBack();
        }
    }

    private void FinishFeedBack()
    {
        foreach (FeedBack f in _feedbackToPlay)
        {
            f.CompletePrevFeedBack();
        }
    }
}
