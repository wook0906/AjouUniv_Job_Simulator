using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public class CustomRoomItemSlotStateInfo
    {
        public CustomRoomItemSlotStateInfo()
        {
            isNotEmpty = false;
            state = CustomRoomSlotState.Empty;
            level = 1;
            nickname = "-";
        }
        public bool isNotEmpty;
        public CustomRoomSlotState state;
        public int level;
        public string nickname;
    }
    public enum CustomRoomSlotState
    {
        Empty,
        WaitPlayer,
        Ready,
        Host,
    }
    public class ACHReward
    {
        public int gold = 0;
        public int diamond = 0;
        public int battery = 0;
        public int skinType = 0;
    }
    public enum ProfileArtItemID
    {
        ProfileArt001,
        ProfileArt002,
        ProfileArt003,
        ProfileArt004,
        ProfileArt005,
     
        MAX,
    }
    public struct ProfileData
    {
        public string nickname;
        public int level;
        public int totalPlayCnt;
        public int defeatCnt;
        public int winCnt;
        public int killCnt;
        public int deathCnt;
        public string StateMSG;
    }
    public enum Phase
    {
        None, StartMatching, ArenaSetup, PlayerSetup, ItemSetup, PlaceRobot, SelectBehaviour, SelectRange, Simulation, SuddenDeath, ResolutionTurn, Synchronization, GameOver, WaitSync,

        TutorialSetup,
    }
    public enum MapType
    {
        TwinCity, Rome, Ruhrgebiet, Tokyo
    }
    public enum Scene
    {
        UnKnown,
        Title,
        Tutorial,
        Lobby,
        Twincity,
        Rome,
        Ruhrgebiet,
        Tokyo,
        Shop,
        ResultScene,
        GameScene,
    }

    public enum Objects
    {
        None,
        Coin,
        RepairKit,
        MSG3D,
        Armargeddon,
        GRENADE,
        TIMEBOMB,
        Ballista,
        ModuleBox,
        POWERBEAM,
        MSG2D,
        Max
    }

    public enum Effects
    {
        None,
        VFX_GetRepairItem,
        VFX_GetVictoryCoinItem,
        VFX_GetModuleItem,
        VFX_BallistaSonicBoom,
        VFX_FallInWater,
        VFX_ExplosionArmageddon,
        VFX_DummyGearModule,
        VFX_FireEMP,
        VFX_HackingModule,
        VFX_PowerbeamHead,
        VFX_PowerbeamBody,
        VFX_PowerbeamTail,
        VFX_Repulsionblast,
        VFX_Anchor,
        VFX_SawbladeMark,
        VFX_SawbladeHit,
        VFX_SHIELD,
        VFX_Shockwave,
        VFX_Teleport,
        VFX_ExplosionTimebomb,
        VFX_ResponesPoint,
        VFX_ExplosionRobot,
        VFX_ElectricTrapHit,
        VFX_EMPHit,
        VFX_EMPStun,
        VFX_HoundHit,
        VFX_MercuryHit,
        VFX_ReaperHit,
        VFX_VoltHit,
        VFX_HoundMuzzle,
        VFX_MercuryMuzzle,
        VFX_ReaperMuzzle,
        VFX_ReaperSlash,
        VFX_VoltMuzzle,
        VFX_VoltCharging,
        VFX_VoltSlash,
        VFX_VoltSting,
        VFX_RobotCollision,
        VFX_RobotRunSmoke,
        VFX_RobotSpark1,
        VFX_RobotSpark2,
        VFX_HoundSpawn,
        VFX_VoltSpawn,
        VFX_ReaperSpawn,
        VFX_MercurySpawn,
        VFX_KillboySpawn,
        VFX_ExplosionGrenade,
        VFX_Satelitebeam,
        VFX_FallTile,
        Max
    }
    public enum EmoticonType
    {
        None = 9000000,

        Volt_Joy = 9000001,
        Volt_Surprise = 9000002,
        Volt_Sadness = 9000003,
        Volt_MakeFunOf = 9000004,
        Volt_QuestionMark = 9000005,
        Volt_Hi = 9000006,
        Volt_Boo = 9000007,
        Volt_ThumbsUp = 9000008,
        Volt_Laughter = 9000009,
        Volt_Happiness = 9000010,
        Volt_Angry = 9000011,

        Mercury_Joy = 9000101,
        Mercury_Surprise = 9000102,
        Mercury_Sadness = 9000103,
        Mercury_MakeFunOf = 9000104,
        Mercury_QuestionMark = 9000105,
        Mercury_Hi = 9000106,
        Mercury_Boo = 9000107,
        Mercury_ThumbsUp = 9000108,
        Mercury_Laughter = 9000109,
        Mercury_Happiness = 9000110,
        Mercury_Angry = 9000111,

        Hound_Joy = 9000201,
        Hound_Surprise = 9000202,
        Hound_Sadness = 9000203,
        Hound_MakeFunOf = 9000204,
        Hound_QuestionMark = 9000205,
        Hound_Hi = 9000206,
        Hound_Boo = 9000207,
        Hound_ThumbsUp = 9000208,
        Hound_Laughter = 9000209,
        Hound_Happiness = 9000210,
        Hound_Angry = 9000211,

        Reaper_Joy = 9000301,
        Reaper_Surprise = 9000302,
        Reaper_Sadness = 9000303,
        Reaper_MakeFunOf = 9000304,
        Reaper_QuestionMark = 9000305,
        Reaper_Hi = 9000306,
        Reaper_Boo = 9000307,
        Reaper_ThumbsUp = 9000308,
        Reaper_Laughter = 9000309,
        Reaper_Happiness = 9000310,
        Reaper_Angry = 9000311,

        Common_Surrender = 9000401
    }


    public struct PurchaseProductResult
    {
        public EShopPurchase productType;
        public bool isSuccess; // 구매 성공,실패 여부

        public PurchaseProductResult(EShopPurchase type, bool success)
        {
            this.productType = type;
            this.isSuccess = success;
        }
    }
    public class PackageProductState
    {
        public bool isPurchased;

        public PackageProductState(bool isPurchased)
        {
            this.isPurchased = isPurchased;;
        }
    }

    [Serializable]
    public struct ShopPriceInfo
    {
        public int id;
        public int price;
        public int assetType;
        public int count;

        public ShopPriceInfo(int id, int price, int assetType, int count)
        {
            this.id = id;
            this.price = price;
            this.assetType = assetType;
            this.count = count;
        }
    }

    public static readonly float LoginTimeOutTime = 5.0f;

    public enum AlignmentType
    {
        Center,
        LeftCenter,
        RightCenter,
        Top,
        LeftTop,
        RightTop,
        Bottom,
        LeftBottom,
        RightBottom
    }
}
