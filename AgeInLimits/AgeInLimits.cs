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
    public partial class AgeInLimits : Handler
    {

        public AgeInLimits() { }


        private Int32 AgeInLimitsIdValue;
        private Int32 FormIdValue;
        private Int32 AgeMinValue;
        private Int32 AgeMaxValue;
        private Int32 RelationshipIdValue;

        public Int32 AgeInLimitsId
        {
            get { return AgeInLimitsIdValue; }
            set { AgeInLimitsIdValue = value; }
        }

        public Int32 FormId
        {
            get { return FormIdValue; }
            set { FormIdValue = value; }
        }

        public Int32 AgeMin
        {
            get { return AgeMinValue; }
            set { AgeMinValue = value; }
        }

        public Int32 AgeMax
        {
            get { return AgeMaxValue; }
            set { AgeMaxValue = value; }
        }

        public Int32 RelationshipId
        {
            get { return RelationshipIdValue; }
            set { RelationshipIdValue = value; }
        }

    }
}