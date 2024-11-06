/*!
 * Copyright (c) 2016  Denis Kuzmin <x-3F@outlook.com> github/3F
 * Copyright (c) Conari contributors https://github.com/3F/Conari/graphs/contributors
 * Licensed under the MIT License (MIT).
 * See accompanying LICENSE.txt file or visit https://github.com/3F/Conari
*/

using System;

namespace net.r_eg.Conari.Log
{
    public interface ISender
    {
        /// <summary>
        /// When message has been received.
        /// </summary>
        event EventHandler<Message> Received;

        /// <summary>
        /// To send new message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        void send(object sender, Message msg);

        /// <summary>
        /// To send new message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        void send(object sender, string msg);

        /// <summary>
        /// To send new message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        void send(object sender, string msg, Message.Level type);

        /// <summary>
        /// To send new message with default sender as typeof(T).
        /// It useful for static methods etc.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        void send<T>(Message msg) where T: class;

        /// <summary>
        /// To send new message with default sender as typeof(T).
        /// It useful for static methods etc.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        void send<T>(string msg) where T : class;

        /// <summary>
        /// To send new message with default sender as typeof(T).
        /// It useful for static methods etc.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        void send<T>(string msg, Message.Level type) where T : class;
    }
}
