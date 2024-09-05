using System;

namespace TestAutomation.Framework.DomainLayer.Models.ValueObjects
{
    internal class ProcessCommand
    {
        private string type;

        private string statement;

        public string Type
        {
            get
            {
                if (type == null)
                {
                    throw new ArgumentException("ProcessCommand Type değeri null olamaz.");
                }

                return type;
            }

            private set
            {
                type = value;
            }
        }

        public string Statement
        {
            get
            {
                if (statement == null)
                {
                    throw new ArgumentException("ProcessCommand Statement değeri null olamaz.");
                }

                return statement;
            }

            private set
            {
                statement = value;
            }
        }

        public ProcessCommand(string commandType, string commandStatement)
        {
            Type = commandType;

            Statement = commandStatement;
        }
    }
}
