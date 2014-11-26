using System;
using BehaviorSharp.Components;
using BehaviorSharp.Components.Composites;

namespace BehaviorSharp
{
    public enum BehaviorReturnCode
    {
        Failure,
        Success,
        Running
    }

    public delegate BehaviorReturnCode BehaviorReturn();

    /// <summary>
    /// 
    /// </summary>
    public class Behavior
    {
        public BehaviorReturnCode ReturnCode { get; set; }

        private readonly BehaviorComponent _root;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        public Behavior(IndexSelector root)
        {
            _root = root;
        }

        public Behavior(BehaviorComponent root)
        {
            _root = root;
        }

        /// <summary>
        /// perform the behavior
        /// </summary>
        public BehaviorReturnCode Behave()
        {
            try
            {
                switch (_root.Behave())
                {
                    case BehaviorReturnCode.Failure:
                        ReturnCode = BehaviorReturnCode.Failure;
                        return ReturnCode;
                    case BehaviorReturnCode.Success:
                        ReturnCode = BehaviorReturnCode.Success;
                        return ReturnCode;
                    case BehaviorReturnCode.Running:
                        ReturnCode = BehaviorReturnCode.Running;
                        return ReturnCode;
                    default:
                        ReturnCode = BehaviorReturnCode.Running;
                        return ReturnCode;
                }
            }
            catch (Exception e)
            {
#if DEBUG
                Console.Error.WriteLine(e.ToString());
#endif
                ReturnCode = BehaviorReturnCode.Failure;
                return ReturnCode;
            }
        }
    }
}
