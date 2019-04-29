namespace Feign.Core {

    /// <summary>
    /// internal config .for test/debug propose.
    /// </summary>
    internal class InternalConfig {
        /// <summary>
        /// Emit test code when wrap the interface class.
        /// eg,
        /// methodILGenerator.EmitWriteLine ("current method is " + methodInfo.Name);
        /// </summary>
        static internal bool EmitTestCode = true;
    }

}