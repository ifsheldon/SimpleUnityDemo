using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTimer : MonoBehaviour
{
    private Skill skill = SkillManager.GetSkill();
    // Start is called before the first frame update
//    void Start()
//    {
//        
//    }

    // Update is called once per frame
    void Update()
    {
        skill.AddTime(Time.deltaTime);
    }
}
