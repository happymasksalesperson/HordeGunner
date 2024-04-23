using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NipperView : MonoBehaviour
{
    public List<Animator> animatorList = new List<Animator>();
    public AnimationClip idleClip;
    public Avatar idleAvatar;
    public AnimationClip jogClip;
    public Avatar jogAvatar;
    public AnimationClip runClip;
    public Avatar runAvatar;
    public AnimationClip attack01Clip;
    public Avatar attack01Avatar;
    public AnimationClip attack02Clip;
    public Avatar attack02Avatar;
    public AnimationClip flailClip;
    public Avatar flailAvatar;
    
    public NipperSensor sensor;

    public Dictionary<NipperAnimationEnum, AnimationClip> animationDict =
        new Dictionary<NipperAnimationEnum, AnimationClip>();
    
    public Dictionary<AnimationClip, Avatar> avatarsDict = new Dictionary<AnimationClip, Avatar>();
    
    private bool initialised = false;
    void OnEnable()
    {
        Animator[] animators = GetComponentsInChildren<Animator>();
        foreach (Animator animator in animators)
        {
            animatorList.Add(animator);
        }
        sensor = GetComponentInParent<NipperSensor>();
        if (!initialised)
        {
            animationDict.Add(NipperAnimationEnum.Idle, idleClip);
            avatarsDict.Add(idleClip, idleAvatar);
            animationDict.Add(NipperAnimationEnum.Jog, jogClip);
            avatarsDict.Add(jogClip, jogAvatar);
            animationDict.Add(NipperAnimationEnum.Run, runClip);
            avatarsDict.Add(runClip, runAvatar);
            animationDict.Add(NipperAnimationEnum.Attack01, attack01Clip);
            avatarsDict.Add(attack01Clip, attack01Avatar);
            animationDict.Add(NipperAnimationEnum.Attack02, attack02Clip);
            avatarsDict.Add(attack02Clip, attack02Avatar);
            animationDict.Add(NipperAnimationEnum.Flail, flailClip);
            avatarsDict.Add(flailClip, flailAvatar);
            initialised = true;
        }
        sensor.AnnounceAnimation += SetAnimation;
    }
    private void SetAnimation(NipperAnimationEnum input)
    {
        if (animationDict.ContainsKey(input))
        {
            foreach (Animator animator in animatorList)
            {
                AnimationClip clip = animationDict[input];
                animator.avatar = avatarsDict[clip];
                animator.Play(clip.name);
            }
        }
    }
    void OnDisable()
    {
        sensor.AnnounceAnimation -= SetAnimation;
    }
}
