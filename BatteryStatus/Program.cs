using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {
         //All inside "Script" goes inside Programmable Block
        /*-------------------------------------------Script------------------------------------------------*/
        List<IMyBatteryBlock> batteries = new List<IMyBatteryBlock>();
        List<IMyTextPanel> textPanels = new List<IMyTextPanel>();

        string Mode = "";
        double Stored = 0;
        double Input = 0;
        double Output = 0;

        public Program()
        {

            //Set update frequency for script
            Runtime.UpdateFrequency = UpdateFrequency.Update100;
        }



        public void Main(string argument, UpdateType updateSource)
        {

            //Get BatteryBlocks on same grid as PB
            GridTerminalSystem.GetBlocksOfType<IMyBatteryBlock>(batteries, b => b.ToString().ToLower().Contains("basebattery"));
            //Get LCD-panels on same grid as PB                                                                     ^ ^ ^ 
            GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(textPanels, b => b.ToString().ToLower().Contains("basebattery"));
            //These will find the batteries and panels on the current grid containing any string matching these   ^ ^ ^

            Stored = 0;
            //Get battery status
            for (int i = 0; i < batteries.Count; i++)
            {
                Mode = batteries[i].ChargeMode.ToString();
                Stored += Math.Floor(batteries[i].CurrentStoredPower);
                Input = Math.Floor(batteries[i].CurrentInput * 1000);
                Output = Math.Floor(batteries[i].CurrentOutput * 1000);
            }


            //Write to TextPanels
            for (int i = 0; i < textPanels.Count; i++)
            {
                textPanels[i].WriteText("Batteries Found: " + batteries.Count + "\n");
                textPanels[i].WriteText("Mode: " + Mode + "\n", true);

                if (Stored > 1)
                {
                    textPanels[i].WriteText("Stored Power: " + Stored + " MW\n", true);
                } 
                else { textPanels[i].WriteText("Stored Power: " + Stored + " kW\n", true); }

                if (Input > 1000)
                {
                    textPanels[i].WriteText("Input: " + Input + " MW\n", true);
                }
                else { textPanels[i].WriteText("Input: " + Input + " kW\n", true); }

                if (Output > 1000)
                {
                    textPanels[i].WriteText("Output: " + Output + " MW\n", true);
                }else { textPanels[i].WriteText("Output: " + Output + " kW\n", true); }
                
            }
        }
        /*-------------------------------------------Script------------------------------------------------*/
    }
}
