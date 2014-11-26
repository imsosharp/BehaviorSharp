using System;

namespace BehaviorSharp.Components.Composites
{
    public class PartialSequence : BehaviorComponent
    {
        protected BehaviorComponent[] Behaviors;
        private short _sequence;
        private readonly short _seqLength;

        /// <summary>
        /// Performs the given behavior components sequentially (one evaluation per Tick call)
        /// Performs an AND-Like behavior and will perform each successive component
        /// -Returns Success if all behavior components return Success
        /// -Returns Running if an individual behavior component returns Success or Running
        /// -Returns Failure if a behavior components returns Failure or an error is encountered
        /// </summary>
        /// <param name="behaviors">one to many behavior components</param>
        public PartialSequence(params BehaviorComponent[] behaviors)
        {
            Behaviors = behaviors;
            _seqLength = (short)Behaviors.Length;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorState Tick()
        {
            //while you can go through them, do so
            while (_sequence < _seqLength)
            {
                try
                {
                    switch (Behaviors[_sequence].Tick())
                    {
                        case BehaviorState.Failure:
                            _sequence = 0;
                            State = BehaviorState.Failure;
                            return State;
                        case BehaviorState.Success:
                            _sequence++;
                            State = BehaviorState.Running;
                            return State;
                        case BehaviorState.Running:
                            State = BehaviorState.Running;
                            return State;
                    }
                }
                catch (Exception e)
                {
#if DEBUG
                    Console.Error.WriteLine(e.ToString());
#endif
                    _sequence = 0;
                    State = BehaviorState.Failure;
                    return State;
                }

            }

            _sequence = 0;
            State = BehaviorState.Success;
            return State;

        }

    }
}
