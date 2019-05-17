using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceControl : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
	private KeywordRecognizer swordSkillRecognizer;
	private KeywordRecognizer itemSkillRecognizer;
	private KeywordRecognizer commSkillRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
	private Dictionary<string, Action> swordSkills = new Dictionary<string, Action>();
	private Dictionary<string, Action> itemSkills = new Dictionary<string, Action>();
	private Dictionary<string, Action> commSkills = new Dictionary<string, Action>();
	
	private bool waitingForSkill = false;
	private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        actions.Add("sword skill", SwordSkill);
        actions.Add("use item", UseItem);
        actions.Add("communication skill", CommunicationSkill);
		
		swordSkills.Add("solid strike", SS_Solid_Strike);
		swordSkills.Add("point defect", SS_Point_Defect);
		swordSkills.Add("mothers rosario", SS_Mothers_Rosario);
		
		itemSkills.Add("health potion", IS_Health_Potion);
		itemSkills.Add("teleport scroll", IS_Teleport_Scroll);
		
		commSkills.Add("direct link", CC_Direct_Link);
		commSkills.Add("open channel", CC_Open_Channel);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += ActivateSkill;
		
		swordSkillRecognizer = new KeywordRecognizer(swordSkills.Keys.ToArray());
        swordSkillRecognizer.OnPhraseRecognized += ActivateSwordSkill;
		
		itemSkillRecognizer = new KeywordRecognizer(itemSkills.Keys.ToArray());
        itemSkillRecognizer.OnPhraseRecognized += ActivateItemSkill;
		
		commSkillRecognizer = new KeywordRecognizer(commSkills.Keys.ToArray());
        commSkillRecognizer.OnPhraseRecognized += ActivateCommSkill;
		
        keywordRecognizer.Start();
		Debug.Log("Voice Recognition Ready!");
    }

    private void ActivateSkill(PhraseRecognizedEventArgs speech)
    {
		if(!waitingForSkill)
		{
			Debug.Log("Found voice command: " + speech.text);
			waitingForSkill = true;
			actions[speech.text].Invoke();
		}
    }
	
	private void ActivateSwordSkill(PhraseRecognizedEventArgs speech)
	{
		Debug.Log("Found Sword Skill: " + speech.text);
		waitingForSkill = false;
		swordSkills[speech.text].Invoke();
	}
	
	private void ActivateItemSkill(PhraseRecognizedEventArgs speech)
	{
		Debug.Log("Found Item Skill: " + speech.text);
		waitingForSkill = false;
		itemSkills[speech.text].Invoke();
	}
	
	private void ActivateCommSkill(PhraseRecognizedEventArgs speech)
	{
		Debug.Log("Found Communication Skill: " + speech.text);
		waitingForSkill = false;
		commSkills[speech.text].Invoke();
	}

    private void SwordSkill()
    {
		swordSkillRecognizer.Start();
    }

    private void UseItem()
    {
		itemSkillRecognizer.Start();
    }

    private void CommunicationSkill()
    {
		commSkillRecognizer.Start();
    }
	
	private void SS_Solid_Strike() 
	{
		if(swordSkillRecognizer.IsRunning)
			swordSkillRecognizer.Stop();
	}
	
	private void SS_Point_Defect() 
	{
		if(swordSkillRecognizer.IsRunning)
			swordSkillRecognizer.Stop();
	}
	
	private void SS_Mothers_Rosario() 
	{
		Debug.Log("This is the golden goose!!!");
		if(swordSkillRecognizer.IsRunning)
			swordSkillRecognizer.Stop();
	}
	
	private void IS_Health_Potion()
	{
		if(itemSkillRecognizer.IsRunning)
			itemSkillRecognizer.Stop();
	}
	
	private void IS_Teleport_Scroll()
	{
		if(itemSkillRecognizer.IsRunning)
			itemSkillRecognizer.Stop();
	}
	
	private void CC_Direct_Link()
	{
		if(commSkillRecognizer.IsRunning)
			commSkillRecognizer.Stop();
	}
	
	private void CC_Open_Channel()
	{
		if(commSkillRecognizer.IsRunning)
			commSkillRecognizer.Stop();
	}
	
	void Update()
	{
		if(waitingForSkill && timer >= 5)
		{
			if(swordSkillRecognizer.IsRunning)
				swordSkillRecognizer.Stop();
			if(itemSkillRecognizer.IsRunning)
				itemSkillRecognizer.Stop();
			if(commSkillRecognizer.IsRunning)
				commSkillRecognizer.Stop();
			waitingForSkill = false;
			timer = 0;
			Debug.Log("Time ran out while looking for skill");
		}
		else if(waitingForSkill) 
		{
			timer += Time.deltaTime;
		}
		if(!waitingForSkill) {
			timer = 0;
		}
	}
}
