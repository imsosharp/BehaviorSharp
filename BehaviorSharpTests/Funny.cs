using BehaviorSharp;
using BehaviorSharp.Components.Actions;
using BehaviorSharp.Components.Composites;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BehaviorSharpTests
{
    [TestClass]
    public class Funny
    {
        [TestMethod]
        public void DoorTest()
        {
            var isAtDoor = false;
            var isBehindDoor = false;
            var isDoorOpen = false;
            var isDoorLocked = true;

            var walkToDoor = new BehaviorAction(() =>
            {
                if (isAtDoor)
                    return BehaviorState.Failure;

                isAtDoor = true;
                return BehaviorState.Success;
            });

            var walkThroughDoor = new BehaviorAction(() =>
            {
                if (!isDoorOpen)
                    return BehaviorState.Failure;

                isBehindDoor = true;
                return BehaviorState.Success;
            });

            var unlockDoor = new BehaviorAction(() =>
            {
                if (!isDoorLocked)
                    return BehaviorState.Failure;

                isDoorLocked = false;
                return BehaviorState.Success;
            });

            var openDoor = new BehaviorAction(() =>
            {
                if (isDoorLocked)
                    return BehaviorState.Failure;

                isDoorOpen = true;
                return BehaviorState.Success;
            });

            var closeDoor = new BehaviorAction(() =>
            {
                if (!isBehindDoor)
                    return BehaviorState.Failure;

                isDoorOpen = false;
                return BehaviorState.Success;
            });

            var walkIntoHouseSequence = new Sequence(walkToDoor, 
                new Selector(openDoor, new Sequence(unlockDoor, openDoor)), walkThroughDoor, closeDoor);

            Assert.AreEqual(walkIntoHouseSequence.Tick(), BehaviorState.Success);
            Assert.AreEqual(isAtDoor, true);
            Assert.AreEqual(isBehindDoor, true);
            Assert.AreEqual(isDoorOpen, false);
            Assert.AreEqual(isDoorLocked, false);
        }
    }
}
