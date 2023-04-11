using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum PlayerId
    {
        Red,
        Blue,
    }

    public struct Unit
    {
        public PlayerId Owner;
    }

    public enum UnitBrainState
    {
        Idle,
        Move,
        Follow,
        Attack,
    }

    public struct UnitBrain
    {
        public UnitBrainState State;
    }

    public struct UnitCombat
    {
        public float AggressionRadius;
        public float Range;
        public float Speed;
        public float Cooldown;
        public double CooldownTime;
        public double AttackTime;
        public bool IsReady(double time) => time >= /*CooldownTime + Cooldown*/0;
        public bool IsFinished(double time) => time >= /*AttackTime + Speed*/0;
    }

    public struct UnitFollow
    {
        public float MinDistance;
    }

    public struct UnitLife
    {
        public float Life;
        public float MaxLife;
    }

    public struct UnitDead { }

    public struct UnitAnimator
    {
        public float MoveSpeed;
        public int MoveSpeedId;
        public int AttackId;
    }

    public struct UnitSmartStop
    {
        public float Radius;
    }
