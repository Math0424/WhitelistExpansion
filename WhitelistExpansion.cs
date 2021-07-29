using Sandbox.Engine.Physics;
using Sandbox.Game.AI.Pathfinding;
using Sandbox.Game.AI.Pathfinding.Obsolete;
using Sandbox.Game.Entities.Character;
using Sandbox.Game.Entities.Cube;
using Sandbox.Game.GameSystems;
using Sandbox.Game.Multiplayer;
using Sandbox.Game.Screens.Helpers;
using Sandbox.Game.Weapons;
using Sandbox.Game.World;
using Sandbox.Graphics;
using Sandbox.Graphics.GUI;
using SpaceEngineers.Game.Entities.Blocks;
using System;
using VRage.Audio;
using VRage.Game.Models;
using VRage.Game.ObjectBuilders.Components;
using VRage.Game.Utils;
using VRage.Plugins;
using VRage.Scripting;
using VRage.Utils;
using VRageRender;
using VRageRender.Animations;

namespace WhitelistExpansion
{
    public class WhitelistExpansion : IPlugin
    {
        bool patched = false;
        public void Update() 
        {
            if (!patched)
            {
                patched = true;

                MyScriptCompiler.Static.AddConditionalCompilationSymbols("WHITELIST_EXPANSION");

                using (var white = MyScriptCompiler.Static.Whitelist.OpenBatch())
                {
                    MyLog.Default.WriteLine("[WhitelistExpansion] Start additions");
                    white.AllowNamespaceOfTypes(MyWhitelistTarget.ModApi, new Type[]
                    {
                        typeof(MyCharacterBone), //All animations
                        typeof(MyPhysics), //All physics
                        typeof(MyModel), //All models
                        typeof(MyGuiControlButton), //All UI code
                        typeof(MyMissile), //All the weapons
                        typeof(MySlimBlock), //All cubeblocks and its systems
                        typeof(MySurvivalKit), //All the blocks

                        typeof(MyGuiSounds), //All the sounds
                        typeof(MyGridConveyorSystem), //All game systems

                        typeof(MyNavgroupLinks), //All the new Pathfinding
                        typeof(MyGridNavigationMesh), //All the old Pathfinding
                        
                        typeof(MyToolbarItem), //All toolbar
                    });
                    white.AllowTypes(MyWhitelistTarget.ModApi, new Type[]
                    {   //Misc - Ask for something to be added
                        typeof(MyCamera),
                        typeof(MyCameraShake),
                        typeof(MyCharacter),
                        typeof(MyPlayer),
                        typeof(MyRenderProxy),
                        typeof(MyGuiManager),
                        typeof(Sync),
                        typeof(MySafeZoneAction),
                    });
                }
                foreach (var s in MyScriptCompiler.Static.Whitelist.GetWhitelist())
                {
                    if (s.Value == MyWhitelistTarget.Both || s.Value == MyWhitelistTarget.ModApi)
                    {
                        MyLog.Default.WriteLine("[WhitelistExpansion] " + s.Key);
                    }
                }
                MyLog.Default.WriteLine("[WhitelistExpansion] End additions");
            }
        }
        public void Init(object gameInstance) {}
        public void Dispose() {}
    }
}
