using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class BasicSpecial : MonoBehaviour
{
    public Action<Vector3,State> OnSkillEnd;
    public abstract bool Skill();
    public abstract bool AfterSkill();
}
