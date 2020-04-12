namespace Feign.Core
{

    /// <summary>
    /// internal config. for test/debug propose.
    /// </summary>
    public class InternalConfig
    {
        /// <summary>
        /// Emit test code 
        /// eg,
        /// methodILGenerator.EmitWriteLine ("current method is " + methodInfo.Name);
        /// </summary>
        static public bool EmitTestCode = false;

        /// <summary>
        /// log the request info .
        /// </summary>
        static public bool LogRequest = false;


        /// <summary>
        ///   save the original response ,porpose to debug/test .
        /// </summary>
        static public bool SaveResponse = false;

        /// <summary>
        ///   Don't send Request,porpose to debug/test .
        /// </summary>
        public static bool NotRequest { get; set; }
    }

}