using MinDbg.NativeApi;
using MinDbg.SourceBinding;
using System.Collections.Generic;
using System.Globalization;
using System;

namespace MinDbg.CorDebug
{
    /// <summary>
    /// A class that represents the ICorDebugThread interface.
    /// </summary>
    public sealed class CorThread : WrapperBase
    {
        private readonly ICorDebugThread cothread;

        /// <summary>
        /// Initializes new instance of the thread object.
        /// </summary>
        /// <param name="cothread">COM thread representation.</param>
        /// <param name="options">The options.</param>
        internal CorThread(ICorDebugThread cothread, CorDebuggerOptions options) 
            : base(cothread, options)
        {
            this.cothread = cothread;
        }

        /// <summary>
        /// Get callback, run on worker thread.
        /// </summary>
        /// <returns>An array of the callstack</returns>
        public List<CorFrame> GetFrameList()
        {
            ICorDebugChain ch = null;
            cothread.GetActiveChain(out ch);
            CorChain chain = new CorChain(ch, options);

            List<CorFrame> frameList = new List<CorFrame>();
            CorFrame corFrame = chain.ActiveFrame;
            for (int i = 0; i < 20; i++)
			{
                frameList.Add(corFrame);
			    ICorDebugFrame caller;
                corFrame.GetFrame().GetCaller(out caller);
                if (null == caller) break;

                corFrame = new CorFrame(caller, options);
			}
            
            return frameList;
        }


        /// <summary>
        /// Gets the active stack frame.
        /// </summary>
        /// <returns>Active stack frame.</returns>
        public CorFrame GetActiveFrame()
        {
            ICorDebugFrame coframe;
            cothread.GetActiveFrame(out coframe);
            return new CorFrame(coframe, options);
        }

        /// <summary>
        /// Gets the current source position.
        /// </summary>
        /// <returns>The current source position.</returns>
        public CorSourcePosition GetCurrentSourcePosition()
        {
            return GetActiveFrame().GetSourcePosition();
        }

        public CorException CurrentException
        {
            get
            {
                ICorDebugValue ppExceptionObj;
                cothread.GetCurrentException(out ppExceptionObj);

                return new CorException(ppExceptionObj);
            }
        }
    }
}
