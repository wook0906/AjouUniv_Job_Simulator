using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


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
        
        // 패킷 데이터 내용 순서 상 어쩔 수 없이 하드 코딩 해야하는 부분...
        // 고치려면 서버와 패킷 내용 맞출 필요 있음..
        if(!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(peaceWin);
        idx++;

        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(noDeathWin);
        idx++;

        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(tacticModule);
        idx++;

        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(attackModule);
        idx++;

        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(moveModule);
        idx++;

        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(allModule);
        idx++;

        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(allEnemyAttack);
        idx++;

        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(rapidAttack);
        idx++;

        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(bombing);
        idx++;

        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(saw);
        idx++;

        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(hologram);
        idx++;

        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(dodge);
        idx++;

        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(teleport);
        idx++;

        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(shield);
        idx++;

        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(bomb);
        idx++;

        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(hacking);
        idx++;

        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(timeBomb);
        idx++;

        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(anchor);
        idx++;

        if (!Volt_PlayerData.instance.AchievementProgresses.ContainsKey(id + idx))
            Volt_PlayerData.instance.AchievementProgresses.Add(id + idx, new ACHProgress());
        Volt_PlayerData.instance.AchievementProgresses[id + idx].SetAchievementProgress(emp);
        
        DBManager.instance.OnLoadedUserNormalACHCondition();
    }
}