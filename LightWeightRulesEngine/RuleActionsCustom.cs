
using System;
using System.Data;
using System.Collections.Generic;

namespace LightWeightRulesEngine
{

    public partial class RuleActions
    {

        public RuleActions(Int32 ActionId, String Action, Boolean Active)
        {

            ActionIdValue = ActionId;
            ActionValue = Action;
            ActiveValue = Active;
        }

        public static List<RuleActions> SelectAll_RuleActions(string ConnectionString)
        {
            List<RuleActions> lstRuleActions = new List<RuleActions>();
            DataTable dt = new DataTable();
            String SQL = "pr_App_RuleActions_SELECTALL";
            DataAccess.ExecuteSQL(SQL, ConnectionString, dt);

            foreach (DataRow dr in dt.Rows) lstRuleActions.Add(
                  new RuleActions(
                      Convert.ToInt32(dr["ActionId"]),
                      dr["Action"].ToString(),
                      Convert.ToBoolean(dr["Active"])));

            return lstRuleActions;
        }
    }
}
