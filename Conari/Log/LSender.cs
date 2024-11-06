/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;

namespace net.r_eg.Conari.Log
{
    /// <summary>
    /// A simple retranslator.
    /// Use the NLog etc.
    /// </summary>
    public class LSender: ISender
    {
        /// <summary>
        /// When message has been received.
        /// </summary>
        public event EventHandler<Message> Received = delegate(object sender, Message e) { };

        /// <summary>
        /// Static alias to Received.
        /// </summary>
        public static event EventHandler<Message> SReceived
        {
            add {
                _.Received += value;
            }
            remove {
                _.Received -= value;
            }
        }

        /// <summary>
        /// Static alias to `send(object sender, Message msg)`
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        public static void Send(object sender, Message msg)
        {
            _.send(sender, msg);
        }

        /// <summary>
        /// Static alias to `send(object sender, string msg)`
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        public static void Send(object sender, string msg)
        {
            _.send(sender, msg);
        }

        /// <summary>
        /// Static alias to `send(object sender, string msg, Message.Level type)`
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        public static void Send(object sender, string msg, Message.Level type)
        {
            _.send(sender, msg, type);
        }

        /// <summary>
        /// To send new message with default sender as typeof(T).
        /// It useful for static methods etc.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        public static void Send<T>(Message msg) where T : class
        {
            _.send<T>(msg);
        }

        /// <summary>
        /// To send new message with default sender as typeof(T).
        /// It useful for static methods etc.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        public static void Send<T>(string msg) where T : class
        {
            _.send<T>(msg);
        }

        /// <summary>
        /// To send new message with default sender as typeof(T).
        /// It useful for static methods etc.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        public static void Send<T>(string msg, Message.Level type) where T : class
        {
            _.send<T>(msg, type);
        }

        /// <summary>
        /// To send new message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        public void send(object sender, Message msg)
        {
            if(sender == null) {
                sender = this;
            }

            Received(sender, msg);
        }

        /// <summary>
        /// To send new message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        public void send(object sender, string msg)
        {
            send(sender, msg, Message.Level.Debug);
        }

        /// <summary>
        /// To send new message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        public void send(object sender, string msg, Message.Level type)
        {
            send(sender, new Message(msg, type));
        }

        /// <summary>
        /// To send new message with default sender as typeof(T).
        /// It useful for static methods etc.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        public void send<T>(Message msg) where T : class
        {
            send(typeof(T), msg);
        }

        /// <summary>
        /// To send new message with default sender as typeof(T).
        /// It useful for static methods etc.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        public void send<T>(string msg) where T : class
        {
            send(typeof(T), msg);
        }

        /// <summary>
        /// To send new message with default sender as typeof(T).
        /// It useful for static methods etc.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        public void send<T>(string msg, Message.Level type) where T : class
        {
            send(typeof(T), msg, type);
        }

        /// <summary>
        /// Thread-safe getting the instance of the Sender class
        /// </summary>
        public static ISender _
        {
            get { return _lazy.Value; }
        }
        private static readonly Lazy<ISender> _lazy = new Lazy<ISender>(() => new LSender());

        protected LSender() { }
    }
}
