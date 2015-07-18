using System.Collections.Generic;

namespace BLL.Util
{
    public interface IResponse : IEnumerable<Msg>
    {
        bool IsError { get; }
        bool HasMsgs { get; }

    }
}