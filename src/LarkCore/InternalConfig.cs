namespace Lark.Core
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
        public static bool EmitTestCode { get; set; } = false;

        /// <summary>
        /// log the request info .
        /// </summary>
        public static bool LogRequest { get; set; } = false;

        /// <summary>
        /// log the response info .
        /// </summary>
        public static bool LogResponse { get; set; } = false;


        /// <summary>
        ///   save the original response ,porpose to debug/test .
        /// </summary>
        public static bool SaveResponse { get; set; } = false;

        /// <summary>
        ///   Don't send Request,porpose to debug/test .
        /// </summary>
        public static bool NotRequest { get; set; } = false;

        public static bool SaveRequest { get; set; } = false;
    }

}