using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIndicator : MonoBehaviour
{
    private Text t;
    private Skill skill;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Text>();
        skill = SkillManager.GetSkill();
    }

    // Update is called once per frame
    void Update()
    {
        t.text = skill.TimeToReady() <= 0 ? "Skill Ready" : "Skill Cooling";
    }
}