﻿using System;
using System.Collections.Generic;
using System.Data.Common;

namespace TestBase.FakeDb
{
    /// <summary>
    /// Maintains a list of Commands + parameters sent to the fake db in unit testing
    /// </summary>
    public class DbCommandInvocationList : List<DbCommandInvocation>
    {
        public void Add(DbCommand cmd, DbParameterCollection parameters)
        {
            Add(new DbCommandInvocation(cmd, parameters));
        }
    }

    /// <summary>
    /// Encapsulates a DbCommand together with the Parameters with which it was invoked.
    /// </summary>
    public class DbCommandInvocation : Tuple<DbCommand, DbParameterCollection>
    {
        public DbCommandInvocation(DbCommand command,DbParameterCollection parameterCollection) : base(command,parameterCollection){}

        public DbCommand             Command    { get { return Item1; } }
        public DbParameterCollection Parameters { get { return Item2; } }
    }
}