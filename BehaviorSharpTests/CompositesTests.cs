#region LICENSE

// Copyright 2014 - 2014 BehaviorSharpTests
// CompositesTests.cs is part of BehaviorSharpTests.
// BehaviorSharpTests is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// BehaviorSharpTests is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// You should have received a copy of the GNU General Public License
// along with BehaviorSharpTests. If not, see <http://www.gnu.org/licenses/>.

#endregion

#region

using System;
using BehaviorSharp;
using BehaviorSharp.Components.Actions;
using BehaviorSharp.Components.Composites;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace BehaviorSharpTests
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

                        return BehaviorState.Success;
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