using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace FactoryPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            WidgetFactory wf1 = WidgetFactory.GetConcreteFactory("GreenWidget");
            WidgetFactory wf2 = WidgetFactory.GetConcreteFactory("BlueWidget");
            WidgetFactory wf3 = WidgetFactory.GetConcreteFactory("RedWidget");
            WidgetFactory2 wf4 = WidgetFactory2.GetConcreteFactory("FactoryPattern.GreenWidget2");
            WidgetFactory2 wf5 = WidgetFactory2.GetConcreteFactory("FactoryPattern.BlueWidget2");
            WidgetFactory2 wf6 = WidgetFactory2.GetConcreteFactory("FactoryPattern.RedWidget2");
            Console.ReadLine();
        }
    }
   
    public abstract  class WidgetFactory
    {
        public WidgetFactory() { }
        public static WidgetFactory GetConcreteFactory(string ConcreteFactory)
        {
            WidgetFactory wf = null;
            switch (ConcreteFactory)
            {
                case "GreenWidget":
                    wf = new GreenWidget();
                    break;
                case "BlueWidget":
                   wf = new BlueWidget();
                    break;
                case "RedWidget":
                    wf = new RedWidget();
                    break;

            }
            return wf;
        }
    }

   public abstract class WidgetFactory2
    {
       public WidgetFactory2() { }
       public static WidgetFactory2 GetConcreteFactory(string ConcreteFactory)
        {
            Assembly oAssembly = Assembly.Load("FactoryPattern");
            WidgetFactory2 wf = (WidgetFactory2)oAssembly.CreateInstance(ConcreteFactory);
            return wf;
        }
    }

    class GreenWidget:WidgetFactory
    {
        public GreenWidget()
        {
            Console.WriteLine("I am a green widget");
        }
    }
    
    class GreenWidget2 : WidgetFactory2
    {
        public GreenWidget2()
        {
            Console.WriteLine("I am a green widget too");
        }
    }

    class BlueWidget:WidgetFactory
    { 
        public BlueWidget()
        {
            Console.WriteLine("I am a blue widget");
        }
    }
    
    class BlueWidget2 : WidgetFactory2
    {
        public BlueWidget2()
        {
            Console.WriteLine("I am a blue widget too");
        }
    }

    class RedWidget:WidgetFactory
    {
        public RedWidget()
        {
            Console.WriteLine("I am a red widget");
        }
    }
    
    class RedWidget2 : WidgetFactory2
    {
        public RedWidget2()
        {
            Console.WriteLine("I am a red widget too");
        }
    }
}
