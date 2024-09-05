using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using TestAutomation.Framework.DomainLayer.Managers;
using TestAutomation.Framework.DomainLayer.Models.Enums;
using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using TestAutomation.Framework.DomainLayer.POMBase;
using TestAutomation.Framework.InfrastructureLayer.Settings;

namespace TestAutomation.Framework.DomainLayer.Models.Entities {
    class ProxyServer {
        private readonly Test test;

        private string clientIP;

        private string sessionId;

        public DesktopAppType DesktopAppType { get; set; }

        public ProxyServer(Test test) {
            this.test = test;
        }

        public string SessionId
        {
            get
            {
                sessionId =
                    sessionId ??
                    DriverManager.GetSession(test).Id;
                return sessionId;
            }
        }

        public string ClientIP
        {
            get
            {
                clientIP =
                    clientIP ??
                    GetClient().IP;
                return clientIP;
            }
        }

        private Client GetClient() {
            try {
                if (Config.GetInstance().ExecutionEnvironment.Equals(ExecutionEnvironment.LOCALHOST)) {
                    return new Client("localhost");
                }

                JObject joResponse = JObject.Parse(GetClientResponse());
                JValue ojObject = (JValue)joResponse["proxyId"];
                ////logger.Info("Client Response: " + ojObject);

                string withoutProtocol = ojObject.ToString().Replace("http://", "").Replace("https://", "");
                const string pattern = @"(?<ip>(?:\d{1,3}\.){3}\d{1,3})";
                Match ipMatch = Regex.Match(withoutProtocol, pattern);

                if (ipMatch.Success) {
                    return new Client(ipMatch.Value);
                }
                else {
                    throw new FormatException("Invalid URL Format: IP address not found");
                }
            }
            catch (Exception) {
                string mlResult = null;//ServiceLocator.Create<IStringService>().GetString("TestAuto_GetClient");
                ////logger.Error(ex);
                throw new Exception(mlResult);
            }
        }

        private string GetClientResponse() {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetHttpRequestUri());
            try {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream()) {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex) {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream()) {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    string errorText = reader.ReadToEnd();
                }
                throw;
            }

        }

        private string GetHttpRequestUri() {
            string serverBaseURI = "http://localhost";

            if (Config.GetInstance().ExecutionEnvironment.Equals(ExecutionEnvironment.TESTINIUM)) {
                serverBaseURI = Config.GetInstance().ServerUrl;
                ////logger.Info("ServerBaseUrl=" + serverBaseURI);
                string mlResult = null;// ServiceLocator.Create<IStringService>().GetString("TestAuto_TestiniumBaseUrl");
                string message = string.Format(mlResult, serverBaseURI);
                ////logger.Info(message);
            }

            string Uri = serverBaseURI + ":4444/grid/api/testsession?session=" + SessionId;
            return Uri;
        }
    }
}
