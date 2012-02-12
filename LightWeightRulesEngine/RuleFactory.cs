using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml.Linq;

namespace LightWeightRulesEngine
{
    public abstract class RuleFactory
    {
        public static int HandleRequest(Rule Rule, int exitPath, XElement Application)
        {
            Assembly oAssembly = Assembly.Load(Rule.AssemblyName, null);
            Handler oFactory = ((Handler)oAssembly.CreateInstance(Rule.ClassName, true));
            exitPath = oFactory.HandleRequest(ref exitPath, ref Application);
            return exitPath;
        }

        public static int GetNextRule(int RuleId, int exitPath, XElement Application,List<RulesToRun> lstRulesToRun)
        {
            var next = from RulesToRun r in lstRulesToRun
                       where r.ActionId == exitPath &&
                       r.FormId == Convert.ToInt32(((String)Application.Element("FormId"))) &&
                       r.RuleId == RuleId
                       select r.SuccessorId;
            int nextRuleId = 0;
            foreach (var i in next) nextRuleId = Convert.ToInt32(i);
            return nextRuleId;
        }
    }
}
