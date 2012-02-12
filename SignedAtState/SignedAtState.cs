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
    public partial class SignedAtState : Handler
    {

        public SignedAtState() { }


        private Int32 SignedAtStateIdValue;
        private Int32 FormIdValue;
        private String StateValue;

        public Int32 SignedAtStateId
        {
            get { return SignedAtStateIdValue; }
            set { SignedAtStateIdValue = value; }
        }

        public Int32 FormId
        {
            get { return FormIdValue; }
            set { FormIdValue = value; }
        }

        public String State
        {
            get { return StateValue; }
            set { StateValue = value; }
        }

    }
}