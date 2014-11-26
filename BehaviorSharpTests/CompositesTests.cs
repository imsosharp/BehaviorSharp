using System;
using BehaviorSharp;
using BehaviorSharp.Components.Actions;
using BehaviorSharp.Components.Composites;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BehaviorSharpTest
{
    [TestClass]
    public class CompositesTests
    {
        [TestMethod]
        public void StatefulSelector()
        {
            var first = true;
            var second = true;

            var statefulSelector =
                new StatefulSelector(
                    new BehaviorAction(() => BehaviorState.Failure),
                    new BehaviorAction(() =>
                    {
                        if (first)
                        {
                            first = false;
                            return BehaviorState.Running;
                        }

                        return BehaviorState.Failure;
                    }),
                    new BehaviorAction(() =>
                    {
                        if (first)
                        {
                            return BehaviorState.Success;
                        }

                        if (second)
                        {
                            second = false;
                            return BehaviorState.Success;
                        }

                        return BehaviorState.Failure;
                    })
            );

            var statefulSelectorError =
                new StatefulSelector(
                    new BehaviorAction(() => { throw new Exception("Failure"); })
            );

            // test init
            Assert.IsTrue(first, "init first");
            Assert.IsTrue(second, "init second");
            Assert.AreEqual(statefulSelector.Tick(), BehaviorState.Running, "1st running");
            Assert.IsFalse(first, "1st Tick failed");
            Assert.AreEqual(statefulSelector.Tick(), BehaviorState.Success, "2nd success");
            Assert.IsFalse(second, "2nd Tick failed");
            Assert.AreEqual(statefulSelector.Tick(), BehaviorState.Failure, "3rd failure");

            first = true;
            second = true;

            // test reset
            Assert.IsTrue(first, "reset first");
            Assert.IsTrue(second, "reset second");
            Assert.AreEqual(statefulSelector.Tick(), BehaviorState.Running, "1st running");
            Assert.IsFalse(first, "1st Tick failed");
            Assert.AreEqual(statefulSelector.Tick(), BehaviorState.Success, "2nd success");
            Assert.IsFalse(second, "2nd Tick failed");
            Assert.AreEqual(statefulSelector.Tick(), BehaviorState.Failure, "3rd failure");

            // test exception
            Assert.AreEqual(statefulSelectorError.Tick(), BehaviorState.Failure, "error failure");
            Assert.AreEqual(statefulSelectorError.LastBehavior, 0);
        }

        [TestMethod]
        public void StatefulSequence()
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
                            return BehaviorState.Running;
                        }

                        return BehaviorState.Success;;
                    }),
                    new BehaviorAction(() => BehaviorState.Success)
            );

            var statefulSequenceError =
                new StatefulSequence(
                    new BehaviorAction(() => { throw new Exception("Failure"); })
            );

            // test init
            Assert.IsTrue(first, "init first");
            Assert.AreEqual(statefulSequence.Tick(), BehaviorState.Running, "1st running");
            Assert.IsFalse(first, "1st Tick failed");
            Assert.AreEqual(statefulSequence.Tick(), BehaviorState.Success, "2nd success");
            Assert.AreEqual(statefulSequence.Tick(), BehaviorState.Failure, "3rd failure");

            first = true;

            // test reset
            Assert.IsTrue(first, "reset first");
            Assert.AreEqual(statefulSequence.Tick(), BehaviorState.Running, "1st running");
            Assert.IsFalse(first, "1st Tick failed");
            Assert.AreEqual(statefulSequence.Tick(), BehaviorState.Success, "2nd success");
            Assert.AreEqual(statefulSequence.Tick(), BehaviorState.Failure, "3rd failure");

            // test exception
            Assert.AreEqual(statefulSequenceError.Tick(), BehaviorState.Failure, "error failure");
            Assert.AreEqual(statefulSequenceError.LastBehavior, 0);
        }
    }
}
