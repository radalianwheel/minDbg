using MinDbg.NativeApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinDbg.CorDebug
{
    public sealed class CorChain : WrapperBase
    {
        internal CorChain(ICorDebugChain chain, CorDebuggerOptions options)
            : base(chain, options)
        {
            m_chain = chain;
        }


        internal ICorDebugChain Raw
        {
            get
            {
                return m_chain;
            }
        }

        public CorFrame ActiveFrame
        {
            get
            {
                ICorDebugFrame iframe;
                m_chain.GetActiveFrame(out iframe);
                return (iframe == null ? null : new CorFrame(iframe, options));
            }
        }

        public CorChain Callee
        {
            get
            {
                ICorDebugChain ichain;
                m_chain.GetCallee(out ichain);
                return (ichain == null ? null : new CorChain(ichain, options));
            }
        }

        public CorChain Caller
        {
            get
            {
                ICorDebugChain ichain;
                m_chain.GetCaller(out ichain);
                return (ichain == null ? null : new CorChain(ichain, options));
            }
        }

        public CorChain Next
        {
            get
            {
                ICorDebugChain ichain;
                m_chain.GetNext(out ichain);
                return (ichain == null ? null : new CorChain(ichain, options));
            }
        }

        public CorChain Previous
        {
            get
            {
                ICorDebugChain ichain;
                m_chain.GetPrevious(out ichain);
                return (ichain == null ? null : new CorChain(ichain, options));
            }
        }

        internal CorDebugChainReason Reason
        {
            get
            {
                CorDebugChainReason reason;
                m_chain.GetReason(out reason);
                return reason;
            }
        }

        public void GetStackRange(out Int64 pStart, out Int64 pEnd)
        {
            UInt64 start = 0;
            UInt64 end = 0;
            m_chain.GetStackRange(out start, out end);
            pStart = (Int64)start;
            pEnd = (Int64)end;
        }

        public CorThread Thread
        {
            get
            {
                ICorDebugThread ithread;
                m_chain.GetThread(out ithread);
                return (ithread == null ? null : new CorThread(ithread, options));
            }
        }

        public bool IsManaged
        {
            get
            {
                int managed;
                m_chain.IsManaged(out managed);
                return (managed != 0 ? true : false);
            }
        }

        private ICorDebugChain m_chain;

        /*
        public IEnumerable<CorFrame> EnumerateFrames(CorFrame thread)
        {
            while (m_chain != null)
            {
                if (m_chain.IsManaged)
                {
                    // Enumerate managed frames
                    // A chain may have 0 managed frames.
                    CorFrame f = m_chain.ActiveFrame;
                    while (f != null)
                    {
                        MDbgFrame frame = new MDbgILFrame(thread, f);
                        f = f.Caller;
                        yield return frame;
                    }
                }
                else
                {
                    // ICorDebug doesn't unwind unmanaged frames. Need to let a native-debug component handle that.
                    foreach (MDbgFrame frame in UnwindNativeFrames(thread, chain))
                    {
                        yield return frame;
                    }
                }

                // Move to next chain
                chain = chain.Caller;
            }

        }
        */
    }
}
