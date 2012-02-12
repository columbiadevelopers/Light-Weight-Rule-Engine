using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml.Linq;
using System.Reflection;
using LightWeightRulesEngine;
using System.IO;

namespace LightWeightRulesEngine
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
       /// <summary>
       /// The Main method performs the management of the rules engine. Although this project is a 
       /// console application it could easily be converted to a Windows service or a WCF service.
       /// 
       /// Other triggers such as a filewatcher could be used in this process.
       /// </summary>
       /// <param name="args">Not applicable</param>
        static void Main(string[] args)
        {
            
           //PopulateLists prepares all the rule lists and component lists for the application.
            PopulateLists();
            
            //The application will loop through the list of files in lstXDocPath
            foreach (string XDocPath in lstXDocPath)
            {
                //Using LINQ to XML The program loads the Xml document. 
                XDocument xdoc = XDocument.Load(XDocPath);

                //From the xdoc the program will grab the Element "Applications"
                XElement xe = xdoc.Element("Applications");

                //Since the file has a single application this code block will be appropriate.
                
                //The program grabs the formid as the source for selecting the rules
                string FormId = ((string)xe.Element("Application").Element("FormId"));
                
                //Based on the FormId the list of rules will be selected from the list of rules.
                var rulesByFormId = from RulesByFormId rulesbyformid in lstRulesByFormId
                                    where ((rulesbyformid.FormId == Convert.ToInt32(FormId))
                                    && (rulesbyformid.Active))
                                    orderby rulesbyformid.RuleId
                                    select rulesbyformid;

                //Convert the enumerable object to a list
                List<RulesByFormId> lstrulesbyformid = rulesByFormId.ToList<RulesByFormId>();


                Rule rule = new Rule();
                //Here we write out the application Xml to the Console.
                Console.WriteLine("");
                Console.WriteLine(xe.Element("Application").ToString());

                //We set the next rule to the first rule in the set.
                int NextRule = lstrulesbyformid[0].RuleId;

                //Loop through the rules
                do
                {
                    //Get the rule based on the NextRule Variable.
                    var _rule = from Rule rules in lstRules
                                where rules.RuleID == NextRule
                                select rules;

                    if (_rule.Equals( null)) 
                    { 
                        NextRule = 10000;
                        goto ExitRule;
                    }
                    try
                    {
                        rule = ((Rule)_rule.ToList<Rule>()[0]);

                        //Run the Rule through the RuleFactory
                        int exitPath = RuleFactory.HandleRequest(rule, NextRule, xe.Element("Application"));

                        int AppId = Convert.ToInt32(xe.Element("Application").Attribute("AppId").Value);

                        //The AppId and the exitPath determine the test output to the Console.
                        switch(exitPath)
                        {
                            case 1:
                                Console.WriteLine("Rule " + rule.AssemblyName + " for AppId:" + AppId + " is Complete");
                                break;
                            case 2:
                                Console.WriteLine("Rule " + rule.AssemblyName + " for AppId:" + AppId + " Failed");
                                break;
                            case 3:
                                Console.WriteLine("Rule " + rule.AssemblyName + " for AppId:" + AppId + " had a catastrophic failure");
                                break;
                        }
                        //Based on the exitPath get the NextRule
                        NextRule = RuleFactory.GetNextRule(rule.RuleID, exitPath, xe.Element("Application"), lstRulesToRun);
                    }
                    catch (ArgumentOutOfRangeException) { NextRule = 0; }
                ExitRule:
                    if (NextRule == 10000 || NextRule==0) break;
                }while(NextRule != 0 && NextRule != 10000);

                if (NextRule == 10000)
                {
                    Console.WriteLine("We are done with the tests on file " + XDocPath);
                }
                if (NextRule == 0)
                {
                    Console.WriteLine(XDocPath + " resulted in a catastrophic error.  ");
                }
                
            }
            
            //After all the tests are run display all the errors.
            Console.WriteLine("");
            Console.WriteLine("");
            DataTable dt = ErrorLogging.SelectAll_ErrorLogging();
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine("AppId= " + dr["AppId"].ToString() + " FormId= " + 
                        dr["FormId"].ToString() + " ErrorDescription = "+ dr["ErrorDescription"].ToString());
                    
                }
            
            Console.ReadLine();
            
            ErrorLogging.ClearLog();
        }
           

        #region PrivateMethods

        static void GetXmlDocPath()
        {
            string pathRaw = Assembly.GetExecutingAssembly().Location;
            String Path = pathRaw.Substring(0, pathRaw.IndexOf("bin") - 1)+"\\"+ ConfigurationManager.AppSettings["Path"];
            
            DirectoryInfo di = new DirectoryInfo(Path);
            FileInfo[] fi = di.GetFiles();
            for (int i = 0; i < fi.Length; i++) _lstXDocPath.Add(Path + fi[i].Name);
        }
        
        static void PopulateLists()
        {
          GetXmlDocPath();
          lstProductForms=ProductForms.SelectAll_ProductForms(ConnectionString);
          lstRules = Rule.SelectAllActive_Rules(ConnectionString);
          lstRuleActions = RuleActions.SelectAll_RuleActions(ConnectionString);
          lstRulesToRun = RulesToRun.SelectAll_RulesToRun(ConnectionString);
          lstRulesByFormId = RulesByFormId.SelectAll_RulesByFormId(ConnectionString);
        }
        #endregion

        #region Properties
       
        private static string _ConnectionString = string.Empty;  
        private static List<string> _lstXDocPath = new List<string>();
        private static List<Rule> _lstRules = new List<Rule>();
        private static List<RuleActions> _lstRuleActions = new List<RuleActions>();
        private static List<RulesToRun> _lstRulesToRun = new List<RulesToRun>();
        private static List<RulesByFormId> _lstRulesByFormId = new List<RulesByFormId>();
        private static List<ProductForms> _lstProductForms = new List<ProductForms>();
        
        public static string ConnectionString
        {
            get
            {
                string pathRaw = Assembly.GetExecutingAssembly().Location;
                String ConnectionStringPath = pathRaw.Substring(0, pathRaw.IndexOf("bin") - 1);
                if (_ConnectionString == "") _ConnectionString =
                        ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.Replace("XXXXXX", ConnectionStringPath);
                return _ConnectionString;
            }
        }
        
        public static List<string> lstXDocPath
        {
            get { return _lstXDocPath; }
            set { _lstXDocPath = value; }
        }
        
        public static List<Rule> lstRules
        {
            get { return _lstRules; }
            set { _lstRules = value; }
        }
        
        public static List<RuleActions> lstRuleActions
        {
            get { return _lstRuleActions; }
            set { _lstRuleActions = value; }
        }
        
        public static List<RulesByFormId> lstRulesByFormId
        {
            get { return _lstRulesByFormId; }
            set { _lstRulesByFormId = value; }
        }
        
        public static List<RulesToRun> lstRulesToRun
        {
            get { return _lstRulesToRun; }
            set { _lstRulesToRun = value; }
        }
        
        public static List<ProductForms> lstProductForms
        {
            get { return _lstProductForms; }
            set { _lstProductForms = value; }
        }
        
        #endregion
    }
}
