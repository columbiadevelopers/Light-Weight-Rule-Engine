///
/// This class is generated from an automated application and is created based on
/// Database fields and their property types.
/// 
/// Modifying this file may result in problems interacting with your database table.
/// We recommend you do not modify this file but regenerate it from the database as needed.
/// 
/// This is a partial class so that you can create a second partial class that will contain all your
/// overloaded and custom code.  This second partial class will not be modified by the class generator.
/// 

using System;
using System.Diagnostics;

namespace LightWeightRulesEngine
{
    public partial class Rule
    {

        public Rule() { }


        private Int32 RuleIDValue;
        private String RuleDescriptionValue;
        private String ClassNameValue;
        private String AssemblyNameValue;
        private Boolean ActiveValue;

        public Int32 RuleID
        {
            get { return RuleIDValue; }
            set { RuleIDValue = value; }
        }

        public String RuleDescription
        {
            get { return RuleDescriptionValue; }
            set { RuleDescriptionValue = value; }
        }

        public String ClassName
        {
            get { return ClassNameValue; }
            set { ClassNameValue = value; }
        }

        public String AssemblyName
        {
            get { return AssemblyNameValue; }
            set { AssemblyNameValue = value; }
        }

        public Boolean Active
        {
            get { return ActiveValue; }
            set { ActiveValue = value; }
        }

    }
}
