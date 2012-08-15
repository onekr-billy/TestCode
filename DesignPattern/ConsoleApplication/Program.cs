using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteDesignPattern(DesignPatternType.单态模式);

            Console.Read();
        }

        /// <summary>
        /// 执行设计模式
        /// </summary>
        /// <param name="designPatternType"></param>
        private static void ExecuteDesignPattern(DesignPatternType designPatternType)
        {
            Console.WriteLine("-> {0} 开始",designPatternType.ToString());
            switch (designPatternType)
            {
                case DesignPatternType.单态模式:
                    Console.WriteLine("写入属性值 “我是张斌!” ");
                    SingletonPattern.GetInstance.name = "我是张斌!";
                    Console.WriteLine("读出属性值 {0}", SingletonPattern.GetInstance.name);
                    break;

            }
            Console.WriteLine("-> {0} 结束", designPatternType.ToString());
        }



    }

    /// <summary>
    /// 设计模式类型
    /// </summary>
    public enum DesignPatternType
    {
        #region 创建型模式
        抽象工厂,
        单态模式,
        工厂方法,
        建造者模式,
        原型模式,
        #endregion

        #region 行为性模式
        备忘录模式,
        策略模式,
        迭代器模式,
        访问者模式,
        观察者模式,
        解释器模式,
        命令模式,
        模板模式,
        责任链模式,
        中介者模式,
        状态模式,
        #endregion

        #region 结构型模式
        代理模式,
        亨元模式,
        桥接模式,
        适配器模式,
        外观模式,
        装饰模式,
        组合模式
        #endregion
    }
}
