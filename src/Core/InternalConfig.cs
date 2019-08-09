namespace Feign.Core {

    /// <summary>
    /// internal config. for test/debug propose.
    /// </summary>
    public class InternalConfig {
        /// <summary>
        /// Emit test code 
        /// eg,
        /// methodILGenerator.EmitWriteLine ("current method is " + methodInfo.Name);
        /// </summary>
        static public bool EmitTestCode = false;

        /// <summary>
        /// log the request parameter .
        /// </summary>
        static public bool LogRequestParameter = false;


        static public bool SaveResponse = false;

    }

}