using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Cassia;
using TestAutomation.Framework.DomainLayer.Container;
using TestAutomation.Framework.DomainLayer.Models.Entities;
using TestAutomation.Framework.DomainLayer.Models.Enums;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using TestAutomation.Framework.DomainLayer.POMBase;
using TestAutomation.Framework.DomainLayer.Services;
using TestAutomation.Framework.InfrastructureLayer.Settings;
using SystemProcess = System.Diagnostics.Process;
using SystemProcessStartInfo = System.Diagnostics.ProcessStartInfo;

namespace TestAutomation.Framework.DomainLayer.Managers {
    public static class ProcessManager {
        public static void KillProcess(Process process, Test test) {
            string clientURI = GetClientUri(test);
            Kill(process, clientURI);
        }

        public static void KillProcess(string processName) {
            string clientURI = null;//ThreadContext.Properties["ClientIP"].ToString();
            Kill(new Process(processName), clientURI);
        }

        private static void Kill(Process process, string clientURI) {
            if (process == null) {
                return;
            }
            string commandLine = " /s  localhost /IM " + process.Name + ".exe /F /T";

            if (!Config.GetInstance().ExecutionEnvironment.Equals(ExecutionEnvironment.LOCALHOST)) {
                commandLine = commandLine.Replace("localhost", clientURI);
            }

            ExecuteProcess(new ProcessCommand("taskkill", commandLine));
        }

        private static void ExecuteProcess(ProcessCommand processCommand) {
            try {
                SystemProcessStartInfo processInfo =
                    new SystemProcessStartInfo(
                        processCommand.Type,
                        processCommand.Statement) {
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };

                SystemProcess nativeProcess = SystemProcess.Start(processInfo);

                nativeProcess.Close();
            }
            catch (Exception) {
            }
        }

        internal static void KillAll(Test test) {
            IList<Process> processList = Config.GetInstance().ProcessListToKill;

            string clientURI = GetClientUri(test);

            foreach (Process process in processList) {
                string commandLine = " /s  localhost /IM " + process.Name + ".exe /F /T";

                if (!Config.GetInstance().ExecutionEnvironment.Equals(ExecutionEnvironment.LOCALHOST)) {

                    if (process.Name.Equals("Winium.Desktop.Driver")) {
                        continue;
                    }

                    commandLine = commandLine.Replace("localhost", clientURI);
                }

                ExecuteProcess(new ProcessCommand("taskkill", commandLine));
            }
        }

        /// <summary>
        /// Get remote machine info
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public static string GetClientUri(Test test) {
            string ClientUri = null;
            try {
                ClientUri = ((ProxyServer)ContainerService.
                 Get<ProxyServerContainer>(test).
                 Get(test)).ClientIP;
            }
            catch (Exception) {
                ////logger.Error(ex);
            }

            if (!string.IsNullOrEmpty(ClientUri)) {
                return ClientUri;
            }

            return GetLocalMachineIp();
        }

        private static string GetLocalMachineIp() {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList) {
                if (ip.AddressFamily == AddressFamily.InterNetwork) {
                    return ip.ToString();
                }
            }
            return "";
        }

        /// <summary>
        /// Returns remote machine name
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public static string GetClientMachineName(Test test) {
            string commandOutput;
            MatchCollection matches = null;

            try {
                commandOutput = StartNSLookupProcess(GetClientUri(test));
                matches = Regex.Matches(commandOutput, string.Format(@"(?<=Name:)(.+?)(?=.{0})", Config.GetInstance().ServerDomainName));
            }
            catch (Exception) {
                ////logger.Error(ex);
            }

            if (matches == null)
                return string.Empty;
            return matches.Count == 0
                ? Dns.GetHostEntry(Environment.MachineName).HostName
                : matches[0]
                            .ToString()
                            .TrimStart()
                            .TrimEnd()
                            .ToUpper();
        }

        /// <summary>
        /// Returns active user name
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public static string GetActiveUserFromMachine(Test test) {
            if (!Config.GetInstance().ExecutionEnvironment.Equals(ExecutionEnvironment.LOCALHOST)) {
                string remoteClientName = GetClientMachineName(test);
                string loggedUser = GetLoggedUsername(remoteClientName);
                return loggedUser;
            }
            else
                return Environment.UserName;
        }

        /// <summary>
        /// Returns active user name
        /// </summary>
        /// <param name="ipAdress"></param>
        /// <returns></returns>
        public static string GetActiveUserFromMachine(string ipAdress) {
            string loggedUser = GetLoggedUsername(ipAdress);
            return loggedUser;
        }

        private static string GetLoggedUsername(string remoteClientName) {
            string activeLoggedUser = string.Empty;
            try {
                ITerminalServicesManager manager = new TerminalServicesManager();
                using (ITerminalServer server = manager.GetRemoteServer(remoteClientName)) {
                    server.Open();
                    foreach (ITerminalServicesSession session in server.GetSessions()) {
                        ConnectionState state;
                        if (!String.IsNullOrEmpty(session.UserName)) {
                            state = session.ConnectionState;
                            if (state.Equals(ConnectionState.Active))
                                activeLoggedUser = session.UserName;
                        }
                    }
                }
            }
            catch (Exception) {
                ////logger.Error(ex);
            }
            return activeLoggedUser;
        }

        private static string StartNSLookupProcess(string clientIP) {
            SystemProcessStartInfo proscessStartInfo = new SystemProcessStartInfo("cmd") {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                RedirectStandardInput = true
            };

            var process = SystemProcess.Start(proscessStartInfo);

            process.StandardInput.WriteLine("nslookup {0}", clientIP);
            process.StandardInput.WriteLine("exit");
            string commandOutput = process.StandardOutput.ReadToEnd();
            return commandOutput;
        }
    }
}
