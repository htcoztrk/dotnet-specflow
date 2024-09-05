using Dapper.Contrib.Extensions;

namespace TestAutomation.Tests.Entities
{
    [Table("Interface.Main")]
    public class  DenemeTestData
    {
        [Key]
        public int ID { get; set; }

        public string SCREEN_FULL_NAME { get; set; }

        public string SCREEN_NAME { get; set; }

        public string DOMAIN { get; set; }

        public int ACCOUNT_BRANCH_CODE { get; set; }

        public int ACCOUNT_NUMBER { get; set; }

        public int ACCOUNT_SUFFIX { get; set; }

        public string TEST_CASE_NAME { get; set; }

        public int DATA_TYPE { get; set; }

        public string DATA_VALUE { get; set; }

        public int DATA_AVAILABILITY { get; set; }
    }
}
