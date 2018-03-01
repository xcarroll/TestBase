﻿using System;
using System.Collections.Generic;
using System.IO;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;

namespace TestBase
{
    /// <summary>
    /// An <see cref="ILogEventSink"/> sink for Serilog which logs to a given <see cref="IList{T}"/> of string.
    /// Each emitted <see cref="LogEvent"/> will be <see cref="ICollection{T}.Add"/>ed to the IList after being formatted 
    /// by the given <see cref="ITextFormatter"/>
    /// </summary>
    /// <remarks>
    /// You may wish to be careful to only use this for short-lived loggers if you use a <see cref="List{T}"/> 
    /// or other in-memory structure as your <see cref="IList{T}"/>.
    /// </remarks>
    class StringListSink : ILogEventSink
    {
        readonly IList<string> stringList;
        readonly ITextFormatter _textFormatter;
        readonly object _syncRoot = new object();

        /// <summary>
        /// An <see cref="ILogEventSink"/> sink for Serilog which logs to a given <see cref="IList{T}"/> of string.
        /// </summary>
        /// <param name="stringList">You may wish to be careful to only use this for short-lived loggers, if you 
        /// use a <see cref="List{T}"/> or other in-memory structure as your <see cref="IList{T}"/>.</param>
        public StringListSink(IList<string> stringList, ITextFormatter textFormatter)
        {
            if (textFormatter == null) throw new ArgumentNullException("textFormatter");
            this.stringList = stringList;
            _textFormatter = textFormatter;
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            lock (_syncRoot)
            {
                using (var payload = new StringWriter())
                {
                    _textFormatter.Format(logEvent, payload);
                    stringList.Add(payload.ToString());
                }
            }
        }

        public static ILogEventSink For(IList<string> stringList)
        {
            var formatter = new MessageTemplateTextFormatter(DefaultOutputTemplate, null);
            return new StringListSink(stringList, formatter);
        }

        internal const string DefaultOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}";
    }
}