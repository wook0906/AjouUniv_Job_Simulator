using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Facebook.Unity;

public class UserNormalAchConditionStatePacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        Debug.Log("UserNormalAchConditionStatePacket Unpack");
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        int gamePlay = ByteConverter.ToInt(buffer, ref startIndex);
        int kill = ByteConverter.ToInt(buffer, ref startIndex);
        int victoryCoin = ByteConverter.ToInt(buffer, ref startIndex);
        int dead = ByteConverter.ToInt(buffer, ref startIndex);
        int attackTry = ByteConverter.ToInt(buffer, ref startIndex);
        int attackSuccess = ByteConverter.ToInt(buffer, ref startIndex);
        int victoryCount = ByteConverter.ToInt(buffer, ref startIndex);
        int buySkin = ByteConverter.ToInt(buffer, ref startIndex);
        int allEnemyKillOneGame = ByteConverter.ToInt(buffer, ref startIndex);
        int peaceWin = ByteConverter.ToInt(buffer, ref startIndex);
        int noDeathWin = ByteConverter.ToInt(buffer, ref startIndex);
        int allEnemyKillWin = ByteConverter.ToInt(buffer, ref startIndex);
        int tacticModule = ByteConverter.ToInt(buffer, ref startIndex);
        int attackModule = ByteConverter.ToInt(buffer, ref startIndex);
        int moveModule = ByteConverter.ToInt(buffer, ref startIndex);
        int allModule = ByteConverter.ToInt(buffer, ref startIndex);
        int allEnemyAttack = ByteConverter.ToInt(buffer, ref startIndex);
        int allEnemyKill = ByteConverter.ToInt(buffer, ref startIndex);
        int rapidAttack = ByteConverter.ToInt(buffer, ref startIndex);
        int bombing = ByteConverter.ToInt(buffer, ref startIndex);
        int saw = ByteConverter.ToInt(buffer, ref startIndex);
        int hologram = ByteConverter.ToInt(buffer, ref startIndex);
        int dodge = ByteConverter.ToInt(buffer, ref startIndex);
        int teleport = ByteConverter.ToInt(buffer, ref startIndex);
        int shield = ByteConverter.ToInt(buffer, ref startIndex);
        int bomb = ByteConverter.ToInt(buffer, ref startIndex);
        int hacking = ByteConverter.ToInt(buffer, ref startIndex);
        int timeBomb = ByteConverter.ToInt(buffer, ref startIndex);
        int anchor = ByteConverter.ToInt(buffer, ref startIndex);
        int emp = ByteConverter.ToInt(buffer, ref startIndex);
        int gold = ByteConverter.ToInt(buffer, ref startIndex);

        Debug.Log("Game Play count : " + gamePlay);
        Debug.Log("kill count : " + kill);
        Debug.Log("victory Coin : " + victoryCoin);
        Debug.Log("Dead count : " + dead);
        Debug.Log("Attack Try count : " + attackTry);
        Debug.Log("Attack Success count : " + attackSuccess);
        Debug.Log("Victory count : " + victoryCount);
        Debug.Log($"buy skin : {buySkin}");
        Debug.Log($"All Enemy Kill One Game : {allEnemyKillOneGame}");
        Debug.Log($"peace win : {peaceWin}");
        Debug.Log($"no death win : {noDeathWin}");
        Debug.Log($"all enemy kill win : {allEnemyKillWin}");
        Debug.Log($"tactic module : {tacticModule}");
        Debug.Log($"attack Module : {attackModule}");
        Debug.Log($"move Module : {moveModule}");
        Debug.Log($"all Module : {allModule}");
        Debug.Log($"all Enemy Attack : {allEnemyAttack}");
        Debug.Log($"all Enemy kill : {allEnemyKill}");
        Debug.Log($"rapid Attack : {rapidAttack}");
        Debug.Log($"bombing : {bombing}");
        Debug.Log($"saw : {saw}");
        Debug.Log($"hologram : {hologram}");
        Debug.Log($"dodge : {dodge}");
        Debug.Log($"teleport : {teleport}");
        Debug.Log($"shield : {shield}");
        Debug.Log($"bomb : {bomb}");
        Debug.Log($"hacking : {hacking}");
        Debug.Log($"timeBomb : {timeBomb}");
        Debug.Log($"anchor : {anchor}");
        Debug.Log($"emp : {emp}");
        Debug.Log($"gold : {gold}");

        Volt_PlayerData.instance.PlayCount = gamePlay;
        Volt_PlayerData.instance.KillCount = kill;
        Volt_PlayerData.instance.CoinCount = victoryCoin;
        Volt_PlayerData.instance.DeathCount = dead;
        Volt_PlayerData.instance.AttackTryCount = attackTry;
        Volt_PlayerData.instance.AttackSuccessCount = attackSuccess;
        Volt_PlayerData.instance.VictoryCount = victoryCount;
        
        int id = 2000000;
        for (int j = 1; j <= 3; j++)
        {
            if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + j))
                Volt_PlayerData.instance.AchievementProgresses.Add(id + j, new ACHProgress());

            Volt_PlayerData.instance.AchievementProgresses[id + j].SetAchievementProgress(gamePlay);
        }
        for (int j = 4; j <= 5; j++)
        {
            if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + j))
                Volt_PlayerData.instance.AchievementProgresses.Add(id + j, new ACHProgress());

            Volt_PlayerData.instance.AchievementProgresses[id + j].SetAchievementProgress(kill);
        }
        for (int j = 6; j <= 8; j++)
        {
            if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + j))
                Volt_PlayerData.instance.AchievementProgresses.Add(id + j, new ACHProgress());

            Volt_PlayerData.instance.AchievementProgresses[id + j].SetAchievementProgress(gold);
        }
        Volt_PlayerData.instance.DeathCount = dead;
        Volt_PlayerData.instance.AttackTryCount = attackTry;
        Volt_PlayerData.instance.AttackSuccessCount = attackSuccess;
        Volt_PlayerData.instance.VictoryCount = victoryCount;
        for(int j = 9; j <= 11; j++)
        {
            if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + j))
                Volt_PlayerData.instance.AchievementProgresses.Add(id + j, new ACHProgress());

            Volt_PlayerData.instance.AchievementProgresses[id + j].SetAchievementProgress(victoryCount);
        }
        for (int j = 12; j <= 13; j++)
        {
            if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + j))
                Volt_PlayerData.instance.AchievementProgresses.Add(id + j, new ACHProgress());
            Volt_PlayerData.instance.AchievementProgresses[id + j].SetAchievementProgress(buySkin);
        }

        int idx = 14;
        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
        {
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        }        
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(allEnemyKillOneGame);
        idx++;
        Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(peaceWin);
        idx++;
        Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(noDeathWin);
        idx++;
        Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(tacticModule);
        idx++;
        Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(attackModule);
        idx++;
        Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(moveModule);
        idx++;
        Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(allModule);
        idx++;
        Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(allEnemyAttack);
        idx++;
        Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(rapidAttack);
        idx++;
        Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(bombing);
        idx++;
        Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(saw);
        idx++;
        Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(hologram);
        idx++;
        Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(dodge);
        idx++;
        Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(teleport);
        idx++;
        Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(shield);
        idx++;
        Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(bomb);
        idx++;
        Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(hacking);
        idx++;
        Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(timeBomb);
        idx++;
        Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(anchor);
        idx++;
        Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(emp);
        idx++;
        //DBManager.instance.userExtraData = new UserExtraData(gamePlay, kill, victoryCoin, dead,
        //attackTry, attackSuccess, victoryCount);

        //DBManager.instance.OnLoadedUserACHConditionState();
        /*
         UserDaily(Normal)Condition 2개 관련 코드들 전부 제거
         UserDaily(Normal)ConditionState를 추가하였음
         해당 클래스에서 UserDaily(Normal)Condition에서 진행하던 코드를 실행시키면 됨.
         */


        //int startIndex = PacketInfo.FromServerPacketSettingIndex;
        //int id = 2000001;
        //int length = 0;

        //for (int i = 0; i < (int)ENormalConditionType.End; i++)
        //{

        //    int value = ByteConverter.ToInt(buffer, ref startIndex);
        //    if (i == 0) // 게임 플레이 횟수가 들어온다.
        //    {
        //        Volt_PlayerData.instance.PlayCount = value;

        //        for (int j = 0; j < 2; ++j)
        //        {
        //            if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + length))
        //                Volt_PlayerData.instance.AchievementProgresses.Add(id + length, new ACHProgress());

        //            Volt_PlayerData.instance.AchievementProgresses[id + length].SetAchievementProgress(value);
        //            length++;
        //        }
        //    }
        //    else if (i == 1) // 적을 죽인 횟수
        //    {
        //        Volt_PlayerData.instance.KillCount = value;

        //        for (int j = 0; j < 2; ++j)
        //        {
        //            if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + length))
        //                Volt_PlayerData.instance.AchievementProgresses.Add(id + length, new ACHProgress());

        //            Volt_PlayerData.instance.AchievementProgresses[id + length].SetAchievementProgress(value);
        //            length++;
        //        }

        //    }
        //    else if (i == 2) // 승점 코인 획득 갯수
        //    {
        //        Volt_PlayerData.instance.CoinCount = value;
        //        for (int j = 0; j < 3; ++j)
        //        {

        //            if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + length))
        //                Volt_PlayerData.instance.AchievementProgresses.Add(id + length, new ACHProgress());

        //            Volt_PlayerData.instance.AchievementProgresses[id + length].SetAchievementProgress(value);
        //            length++;
        //        }
        //    }
        //    else if (i == 3) // 죽은 횟수
        //    {
        //        Volt_PlayerData.instance.DeathCount = value;
        //    }
        //    else if (i == 4) // 공격 횟수
        //    {
        //        Volt_PlayerData.instance.AttackTryCount = value;
        //    }
        //    else if (i == 5) // 공격 성공횟수{
        //    {
        //        Volt_PlayerData.instance.AttackSuccessCount = value;
        //    }
        //    else if (i == 6) // 승리 횟수
        //    {
        //        Volt_PlayerData.instance.VictoryCount = value;

        //        for(int j = 0; j < 3; j++)
        //        {
        //            if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + length))
        //                Volt_PlayerData.instance.AchievementProgresses.Add(id + length, new ACHProgress());

        //            Volt_PlayerData.instance.AchievementProgresses[id + length].SetAchievementProgress(value);
        //            length++;
        //        }
        //    }
        //    else // 나머지....
        //    {
        //        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + length))
        //            Volt_PlayerData.instance.AchievementProgresses.Add(id + length, new ACHProgress());

        //        Volt_PlayerData.instance.AchievementProgresses[id + length].SetAchievementProgress(value);
        //        length++;
        //    }
        //}
        DBManager.instance.OnLoadedUserNormalACHCondition();
    }
}