using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Core;
using DumbBotsNET.Api;
using Entities;

namespace Scripting
{
    /// <summary>
    /// Class that runs user scripts
    /// </summary>
    internal static class ScriptRunner
    {
        internal static Dictionary<Team, ICommand> TeamScripts { get; private set; }
        internal static IDirectorCommand DirectorScript { get; private set; }
        private static IPlayerApi _scriptMethods;
        private static IDirectorApi _directorMethods;

        static ScriptRunner()
        {
            TeamScripts = new Dictionary<Team, ICommand>();
            _scriptMethods = new ScriptMethods();
            _directorMethods = new DirectorMethods();
        }

        internal static void ReflectScripts()
        {
            ReflectScript(Team.Team1);
            ReflectScript(Team.Team2);
            ReflectDirectorScript();
        }

        internal static void ReflectScript(Team team)
        {
            try
            {
                byte[] rawAssembly = LoadFile(String.Format("AI\\AIscript{0}.dll", (int)team));
                Assembly assembly = AppDomain.CurrentDomain.Load(rawAssembly);

                TeamScripts[team] = (ICommand)assembly.CreateInstance("AIScript");

                assembly = null;
                rawAssembly = null;
            }
            catch (Exception)
            {
                TeamScripts[team] = null;
            }
        }

        internal static void ReflectDirectorScript()
        {
            try
            {
                byte[] rawAssembly = LoadFile("AI\\Director.dll");
                Assembly assembly = AppDomain.CurrentDomain.Load(rawAssembly);

                DirectorScript = (IDirectorCommand)assembly.CreateInstance("AIScript");

                assembly = null;
                rawAssembly = null;
            }
            catch (Exception)
            {
                DirectorScript = null;
            }
        }

        internal static void Think(CombatEntity entity, CombatEntity enemy)
        {
            try
            {
                (_scriptMethods as ScriptMethods).Entity = entity;
                (_scriptMethods as ScriptMethods).Enemy = enemy;
                TeamScripts[entity.Team].Think(_scriptMethods);
            }
            catch (Exception ex)
            {
                //TODO
            }
        }

        internal static void DirectorThink(CombatEntity player1, CombatEntity player2)
        {
            try
            {
                DirectorScript.Think(_directorMethods);
            }
            catch (Exception)
            {
                //TODO
            }
        }

        internal static void DirectorAfterMapLoad(CombatEntity player1, CombatEntity player2)
        {
            try
            {
                (_directorMethods as DirectorMethods).Text = null;
                DirectorScript.AfterMapLoad(_directorMethods);
            }
            catch (Exception)
            {
                //TODO
            }
        }

        private static byte[] LoadFile(string filename)
        {
            byte[] buffer;
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                buffer = new byte[(int)fs.Length];
                fs.Read(buffer, 0, buffer.Length);
            }
            return buffer;
        }
    }
}