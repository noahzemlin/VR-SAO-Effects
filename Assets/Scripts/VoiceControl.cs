using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceControl : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    // Start is called before the first frame update
    void Start()
    {
        actions.Add("sword skill", SwordSkill);
        actions.Add("use item", UseItem);
        actions.Add("communication skill", CommunicationSkill);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += ActivateSkill;
        keywordRecognizer.Start();
    }

    private void ActivateSkill(PhraseRecognizedEventArgs speech)
    {
        Debug.Log("Found voice command: " + speech.text);
        actions[speech.text].Invoke();
    }

    private void SwordSkill()
    {

    }

    private void UseItem()
    {

    }

    private void CommunicationSkill()
    {

    }
}
