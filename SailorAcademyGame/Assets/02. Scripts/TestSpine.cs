using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class TestSpine : MonoBehaviour
{
    [SerializeField] SkeletonGraphic skelAnim;//SkeletonAnimation

    //public
    public Spine.Skeleton skel;
    public Spine.AnimationState animState;

    private void Start()
    {
        skelAnim.Skeleton.SetToSetupPose();
        skelAnim.Skeleton.SetBonesToSetupPose();
        skelAnim.Skeleton.SetSlotsToSetupPose();
    }

    public void PlayFriendAnim(int num) { 
        
        
    }

    public void PlayMentalAnim(int num)
    {


    }




}
