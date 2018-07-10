using System;

namespace SWAG
{
    [Flags]
    public enum EventLevel
        : Int32
    {
        None = 0,

        Critical = 1 << 0,

        Error = 1 << 1,

        Warning = 1 << 2,

        Information = 1 << 3,

        Debug = 1 << 4,

        Trace = 1 << 5,

        Any = EventLevel.Critical | EventLevel.Error | EventLevel.Warning |
            EventLevel.Information | EventLevel.Debug | EventLevel.Trace,
    }
}
