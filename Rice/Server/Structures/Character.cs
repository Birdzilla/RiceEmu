﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rice.Server.Core;

namespace Rice.Server.Structures
{
    public struct LoadCharAck : ISerializable
    {
        public uint ServerId;
        public uint ServerStartTime;
        public CharInfo CharInfo;
        public uint nCarSize;
        public List<CarInfo> CarInfo;

        public void Serialize(PacketWriter writer)
        {
            writer.Write(ServerId);
            writer.Write(ServerStartTime);
            CharInfo.Serialize(writer);
            writer.Write(nCarSize);

            foreach (CarInfo info in CarInfo)
                info.Serialize(writer);
        }
    }

    public struct ExpInfo : ISerializable
    {
        public long CurExp;
        public long NextExp;
        public long BaseExp;

        public void Serialize(PacketWriter writer)
        {
            writer.Write(CurExp);
            writer.Write(NextExp);
            writer.Write(BaseExp);
        }

        public static ExpInfo FromLevelExp(int level, long exp)
        {
            return new ExpInfo
            {
                BaseExp = RiceServer.ExpTable[level - 1],
                NextExp = RiceServer.ExpSumTable[level],
                CurExp = exp
            };
        }
    }

    public struct ItemInfo : ISerializable
    {
        public uint CurCarID;
        public ushort State;
        public ushort Slot;
        public uint StackNum; // it's an int in npluto's implementation for some reason
        public uint LastCarID;
        public uint AssistA;
        public uint AssistB;
        public uint AssistC;
        public uint AssistD;
        public uint AssistE;
        public uint AssistF;
        public uint AssistG;
        public uint AssistH;
        public uint AssistI;
        public uint AssistJ;
        public uint Box;
        public uint Belonging;
        public int Upgrade;
        public int UpgradePoint;
        public uint ExpireTick;
        public float ItemHealth;
        public uint unk1;
        public uint TableIdx;
        public uint InvenIdx;
        public int Random;

        public void Serialize(PacketWriter writer)
        {
            writer.Write(CurCarID);
            writer.Write(State);
            writer.Write(Slot);
            writer.Write(StackNum);
            writer.Write(LastCarID);
            writer.Write(AssistA);
            writer.Write(AssistB);
            writer.Write(AssistC);
            writer.Write(AssistD);
            writer.Write(AssistE);
            writer.Write(AssistF);
            writer.Write(AssistG);
            writer.Write(AssistH);
            writer.Write(AssistI);
            writer.Write(AssistJ);
            writer.Write(Box);
            writer.Write(Belonging);
            writer.Write(Upgrade);
            writer.Write(UpgradePoint);
            writer.Write(ExpireTick);
            writer.Write(ItemHealth);
            writer.Write(0); // unk1, 0 in all known cases
            writer.Write(TableIdx);
            writer.Write(InvenIdx);
            writer.Write(Random);
        }
    }

    public struct ItemModInfo : ISerializable
    {
        public ItemInfo Item;
        public int State;

        public void Serialize(PacketWriter writer)
        {
            Item.Serialize(writer);
            writer.Write(State);
        }
    }

    public struct CharInfo : ISerializable
    {
        public ulong CID;
        public string Name; // 0x15
        public string LastMessageFrom; // 0xB
        public int LastDate;
        public ushort Avatar;
        public ushort Level;
        public ExpInfo ExpInfo;
        public long MitoMoney;
        public long TeamId;
        public long TeamMarkId;
        public string TeamName; // 0xD
        public int TeamRank;
        public byte PType;
        public uint PvpCnt;
        public uint PvpWinCnt;
        public uint PvpPoint;
        public uint TPvpCnt;
        public uint TPvpWinCnt;
        public uint TPvpPoint;
        public uint QuickCnt;
        public float TotalDistance;
        public Vector4 Position;
        public int LastChannel;
        public int City;
        public int PosState;
        public int CurrentCarID;
        public uint QuickSlot1;
        public uint QuickSlot2;
        public DateTime TeamJoinDate;
        public DateTime TeamCloseDate;
        public DateTime TeamLeaveDate;
        public int HancoinInven;
        public int HancoinGarage;
        public int Flags;
        public int Guild;
        public long Mileage;
        public uint GPTeam;

        public void Serialize(PacketWriter writer)
        {
            writer.Write(CID);
            writer.WriteUnicodeStatic(Name, 21);
            writer.WriteUnicodeStatic(LastMessageFrom, 11);
            writer.Write(LastDate);
            writer.Write(Avatar);
            writer.Write(Level);
            ExpInfo.Serialize(writer);
            writer.Write(MitoMoney);
            writer.Write(TeamId);
            writer.Write(TeamMarkId);
            writer.WriteUnicodeStatic(TeamName, 13);
            writer.Write(TeamRank);
            writer.Write(PType);
            writer.Write(PvpCnt);
            writer.Write(PvpWinCnt);
            writer.Write(PvpPoint);
            writer.Write(TPvpCnt);
            writer.Write(TPvpWinCnt);
            writer.Write(TPvpPoint);
            writer.Write(QuickCnt);
            writer.Write(0); // unknown
            writer.Write(0); // unknown
            writer.Write(TotalDistance);
            writer.Write(Position);
            writer.Write(LastChannel);
            writer.Write(City);
            writer.Write(PosState);
            writer.Write(CurrentCarID);
            writer.Write(QuickSlot1);
            writer.Write(QuickSlot2);
            writer.Write(TeamJoinDate);
            writer.Write(TeamCloseDate);
            writer.Write(TeamLeaveDate);
            writer.Write(new byte[12]); // filler
            writer.Write(HancoinInven);
            writer.Write(HancoinGarage);
            writer.Write(Flags);
            writer.Write(Guild);
            writer.Write(new byte[38]); // filler
            writer.Write(GPTeam); // DCGP team
        }
    }

    public struct VisualItem : ISerializable
    {
        public short Neon;
        public short Plate;
        public short Decal;
        public short DecalColor;
        public short AeroBumper;
        public short AeroIntercooler;
        public short AeroSet;
        public short MufflerFlame;
        public short Wheel;
        public short Spoiler;
        public short[] Reserve; // 6
        public string PlateString; // 9

        public void Serialize(PacketWriter writer)
        {
            writer.Write(Neon);
            writer.Write(Plate);
            writer.Write(Decal);
            writer.Write(DecalColor);
            writer.Write(AeroBumper);
            writer.Write(AeroIntercooler);
            writer.Write(AeroSet);
            writer.Write(MufflerFlame);
            writer.Write(Wheel);
            writer.Write(Spoiler);
            for (int i = 0; i < Reserve.Length; i++)
                writer.Write(Reserve[i]);
            writer.WriteUnicodeStatic(PlateString, 9);
        }
    }

    public struct PlayerInfo : ISerializable
    {
        public string Name; // 0xD
        public ushort Serial;
        public ushort Age;
        public long Cid;
        public ushort Level;
        public uint Exp;
        public long TeamId;
        public long TeamMarkId;
        public string TeamName; // 0xE
        public ushort TeamNLevel;
        public VisualItem VisualItem;
        public float UseTime;

        public void Serialize(PacketWriter writer)
        {
            writer.WriteUnicodeStatic(Name, 0xD, true);
            writer.Write(Serial);
            writer.Write(Age);
            writer.Write(new byte[186]); // filler
            /*
            writer.Write(Cid);
            writer.Write(Level);
            writer.Write(Exp);
            writer.Write(TeamId);
            writer.Write(TeamMarkId);
            writer.Write(TeamName);
            writer.Write(TeamNLevel);
            VisualItem.Serialize(writer);
            writer.Write(UseTime);
             */
        }
    }
}