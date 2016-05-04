using GameData;
using GameData.Info;
using GameEngine.Entities;
using GameEngine.Global.Providers;
using GameEngine.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Global {
    public interface IGlobal {
        /// <summary>
        /// The World Object Provides the area to add Map Information, Entities, And Items
        /// Currently in the game
        /// </summary>
        World World { get; }
        
        /// <summary>
        /// The Current Player
        /// </summary>
        IEntity Player { get; }

        TimeProvider Time { get; }

        void SendAction(ActionParameters parameter);

        void SetPlayer(IEntity player);

        void PullNetwork();

        void Tick(float amount = 1.0f);
    }

    public class Global : IGlobal {

        /// <summary>
        /// Provides the current game time, used to coordinate actions, and movements
        /// </summary>
        public TimeProvider Time { get; private set; }

        /// <summary>
        /// The World Object Provides the area to add Map Information, Entities, And Items
        /// Currently in the game
        /// </summary>
        public World World { get; private set; }

        public SortedList Actions { get; set; }
        
        /// <summary>
        /// The Current Player
        /// </summary>
        public IEntity Player { get; private set; }

        public INetworkAdapter Network { get; private set; }

        public Global(INetworkAdapter network) {
            // create a blank world object
            this.World = new World();

            this.Network = network;

            // create the Time Provider
            this.Time = new TimeProvider();

            this.Actions = new SortedList();
        }

        public void SendAction(ActionParameters parameter) {
            this.Network.Send(parameter);
        }
                
        /// <summary>
        /// Sets the current player entity, and adds it to the world
        /// </summary>
        /// <param name="player"></param>
        public void SetPlayer(IEntity player) {
            // remove the current player from the world
            if (this.Player != null) {
                World.UnregisterEntity(this.Player.ID);
            }
            // set the global player object
            this.Player = player;

            // add the new player to the world
            World.RegisterEntity(player);
        }

        public void PullNetwork() {
            foreach (var parameter in Network.Recieve()) {
                var entity = this.World.Entities[parameter.entityID];
                entity.PerformAction(this, parameter);
            }
        }

        /// <summary>
        /// Progresses Game Time and Refreshes the World and it's contents
        /// </summary>
        /// <param name="amount"></param>
        public void Tick(float amount = 1.0f) {
            this.Time.Current += amount;
            this.PullNetwork();
            foreach (var entity in this.World.Entities) {
                entity.Value.Refresh(this);
            }
        }

    }

    public static class GameGlobal {
        /// <summary>
        /// ID Provider provides IDs to all Game objects
        /// </summary>
        public static IDProvider IDs { get; private set; }

        /// <summary>
        /// This Lookup Provides Information about how Skills Effect an Entities Stats
        /// When used
        /// </summary>
        private static SkillStatInfo[][] SkillStateInfo;

        public static IGlobal GlobalInfo;

        private static Random rnd;

        public static float RandomFloat(float min, float max) {
            return ((float)rnd.NextDouble()) * (max - min) + min;
        }

        public static int RandomInt(int min, int max) {
            return rnd.Next(min, max+1);
        }


        private static int a = 1212;
        private static int c = 2000;
        private static int lastX = 1;
        public static int NextRandomInt(int max) {
            lastX = (a * lastX + c) % max;
            return lastX;
        }
        public static void SeedRandomInt(int _a, int _c, int start) {
            a = _a;
            c = _c;
            lastX = start;
        }
        /// <summary>
        /// Global Constructor
        /// </summary>
        static GameGlobal() {
            rnd = new Random();

            // initialize the skill state info
            SkillStateInfo = new SkillStatInfo[Globals.SkillTypeCount][];
            for (int i = 0; i < Globals.StatTypeCount - 1; i++)
                SkillStateInfo[i] = new SkillStatInfo[Globals.StatTypeCount];

            // create the ID Provider
            IDs = new IDProvider();

            INetworkAdapter networkAdapter = new LoopbackNetworkAdapter();

            GlobalInfo = new Global(networkAdapter);

            Rand = new Random();
        }

        public static Random Rand { get; private set; }

        public static void DefineSkillEffect(SkillType skill, StatType type, SkillStatInfo definition) {
            SkillStateInfo[(int)skill][(int)type] = definition;

        }
        public static float CalculateSkillIncrease(SkillType skill, float level) {
            // doesn't do much for now, just  return 1/floor(level)^2
            return 1.0f / (float)Math.Pow(2.0, Math.Floor(level));
        }



        public static float CalculateSkillEffect(SkillType skill, StatType type, float level, float current) {
        
           
            var info = SkillStateInfo[(int)skill][(int)type];
            if (info.Function == SkillStatInfoFunction.LinearPercent) {
                return current * (info.BaseValue + ((float)Math.Floor(level) * info.ModifierValue));
            } else { //None
                return current;
            }

        }

        public static float CalculateWeaponDamage(Item item, EntityStats stats) {
            return 0;
        }

        public static float CalculateSkillPointsMadeFromLevelingUpSkill(SkillType skillType, float level) {
            //for now return a basic amount
            return .01f;
        }







        public static string GetName(Entity entity) {
            return "computed name of entity";
        }
        public static string GetName(Item item) {
            return "computed name of item";
        }
    }
}
