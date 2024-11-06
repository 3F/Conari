/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;

namespace net.r_eg.Conari.Log
{
    [Serializable]
    public class Message: EventArgs
    {
        public DateTime stamp;

        public string content;

        public Exception exception;

        public object data;

        public Level type;

        public enum Level
        {
            Trace,
            Debug,
            Info,
            Warn,
            Error,
            Fatal
        }

        public Message(string msg, Level type = Level.Debug)
        {
            content     = msg;
            this.type   = type;
            stamp       = DateTime.Now;
        }

        public Message(string msg, Exception ex, Level type = Level.Error)
            : this(msg, type)
        {
            exception = ex;
        }

        public Message(string msg, object data, Level type = Level.Debug)
            : this(msg, type)
        {
            this.data = data;
        }
    }
}
