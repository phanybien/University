using System;
using System.Collections.Generic;
using System.Text;

namespace XNA_TPS.GameLogic
{
    public static class UnitTypes
    {
        // Player
        // ---------------------------------------------------------------------------
        public enum PlayerType
        {
            Marine,beast
        }
        public static string[] PlayerModelFileName = { "PlayerMarine" };
        public static float[] PlayerSpeed = { 1.0f };
        public static int[] MAXExp = {1000 };

        // Player Weapons
        // ---------------------------------------------------------------------------
        public enum PlayerWeaponType
        {
            MachineGun
        }
        public static string[] PlayerWeaponModelFileName = { "WeaponMachineGun" };
        public static int[] BulletsCount = { 300 };

        // Enemies
        // ---------------------------------------------------------------------------
        public enum EnemyType
        {
            LV1,LV3,LV5,LV8,LV10,Boss
        }
        public static string[] EnemyModelFileName = { "EnemyBeast","EnemyBeast","EnemyBeast","EnemyBeast","EnemyBeast","EnemyBeast" };
        public static int[] EnemyLife = { 300,600,1000,1800,3000,10000 };
        public static float[] EnemySpeed = { 1.0f,1.1f,1.2f,1.3f,1.4f,2f };
        public static int[] EnemyPerceptionDistance = { 120,140,160,180,200,250 };
        public static int[] EnemyAttackDistance = { 10,12,14,16,18,20 };
        public static int[] EnemyAttackDamage = { 8,20,50,90,160,300 };
        public static int[] EnemyExp = {300,500,1000,1500,2000,10000};
        //Achers
        //---------------------------------------------------------------------------
        public enum AcherType
        {
            LV2,LV4,LV6,LV8
        }
        public static string[] AcherModelFileName = { "PlayerMarine", "PlayerMarine", "PlayerMarine", "PlayerMarine" };
        public static int[] AcherLife = { 120, 300, 500, 1000 };
        public static float[] AcherSpeed = { 0.7f, 0.8f, 0.9f, 1.3f };
        public static int[] AcherPerceptionDistance = { 100, 120, 140, 160 };
        public static int[] AcherAttackDistance = { 80, 100, 120, 150 };
        public static int[] AcherAttackDamage = { 20, 15, 40, 60 };
    }
}
