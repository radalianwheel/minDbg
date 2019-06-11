using MinDbg.NativeApi;

namespace MinDbg.CorDebug
{
    public class CorException
    {
        private ICorDebugValue ppExceptionObj;

        internal CorException(ICorDebugValue ppExceptionObj)
        {
            this.ppExceptionObj = ppExceptionObj;
        }

        public string Name {
            get
            {
                return "???";
            }
        }
    }
}