using System;
using System.Linq;

namespace TestAutomation.Framework.DomainLayer.SpecSync {
    internal static class TagConverter {
        private const string ISSUE_ID = "IssueId:";
        private const string INVENTORY_ID = "InventoryId:";
        private const string RELATED_ID = "RelatedId:";
        private const string ENDPOINT = "Endpoint:";

        internal static void Convert(string[] scenarioTags) {
            try {
                string[] issueSeparators = new string[] { ISSUE_ID };
                string issue = scenarioTags.SingleOrDefault(a => a.StartsWith(ISSUE_ID))?.Split(issueSeparators, StringSplitOptions.None)[1];

                string[] inventorySeparators = new string[] { INVENTORY_ID };
                string inventory = scenarioTags.SingleOrDefault(a => a.StartsWith(INVENTORY_ID))?.Split(inventorySeparators, StringSplitOptions.None)[1];

                string[] relatedSeparators = new string[] { RELATED_ID };
                string related = scenarioTags.SingleOrDefault(a => a.StartsWith(RELATED_ID))?.Split(relatedSeparators, StringSplitOptions.None)[1];

                string[] endpointSeparators = new string[] { ENDPOINT };
                string endpoint = scenarioTags.SingleOrDefault(a => a.StartsWith(ENDPOINT))?.Split(endpointSeparators, StringSplitOptions.None)[1];

                //ThreadContext.Properties["IssueId"] = issue;
                //ThreadContext.Properties["InventoryId"] = inventory;
                //ThreadContext.Properties["RelatedId"] = related;
                //ThreadContext.Properties["Endpoint"] = endpoint;
            }
            catch (Exception) {
                ////logger.Error(ex);
            }
        }
    }
}
