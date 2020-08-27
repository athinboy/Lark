namespace Lark.Core.ValueBind
{
    public class BindBase
    {
        /// <summary>
        /// greater value show higher priority.
        /// 只有两个级别：attribute 是一级别，系统自动推定的是一级别。
        /// </summary>
        public int Priority = 5;

        public bool Validated = true;

        public void Prompt()
        {
            Priority += 1;
        }



    }
}