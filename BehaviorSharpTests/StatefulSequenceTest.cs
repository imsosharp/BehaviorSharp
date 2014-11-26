using System;
using BehaviorSharp;
using BehaviorSharp.Components.Actions;
using BehaviorSharp.Components.Composites;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BehaviorSharpTest
{
    [TestClass]
    public class StatefulSequenceTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var first = true;

            var statefulSequence =
                new StatefulSequence(
                    new BehaviorAction(() => first ? BehaviorState.Success : BehaviorState.Failure),
                    new BehaviorAction(() =>
                    {
                        if (first)
                        {
                            first = false;
                            return BehaviorState.Running;;
                        }

                        return BehaviorState.Success;;
                    }),
                    new BehaviorAction(() => BehaviorState.Success)
            );

            Assert.IsTrue(first);
            Assert.AreEqual(statefulSequence.Tick(), BehaviorState.Running, "1st running");
            Assert.IsFalse(first, "1st Tick failed");
            Assert.AreEqual(statefulSequence.Tick(), BehaviorState.Success, "2nd success");
            Assert.AreEqual(statefulSequence.Tick(), BehaviorState.Failure, "3rd failure");
        }
    }
}
