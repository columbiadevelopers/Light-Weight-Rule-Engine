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
    public partial class RulesToRun
    {

        public RulesToRun() { }


        private Int32 RuleToRunIdValue;
        private Int32 FormIdValue;
        private Int32 RuleIdValue;
        private Int32 ActionIdValue;
        private Int32 SuccessorIdValue;
        private Boolean ActiveValue;

        public Int32 RuleToRunId
        {
            get { return RuleToRunIdValue; }
            set { RuleToRunIdValue = value; }
        }

        public Int32 FormId
        {
            get { return FormIdValue; }
            set { FormIdValue = value; }
        }

        public Int32 RuleId
        {
            get { return RuleIdValue; }
            set { RuleIdValue = value; }
        }

        public Int32 ActionId
        {
            get { return ActionIdValue; }
            set { ActionIdValue = value; }
        }

        public Int32 SuccessorId
        {
            get { return SuccessorIdValue; }
            set { SuccessorIdValue = value; }
        }

        public Boolean Active
        {
            get { return ActiveValue; }
            set { ActiveValue = value; }
        }

    }
}
