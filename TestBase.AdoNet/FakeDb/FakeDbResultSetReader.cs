﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace TestBase.AdoNet
{
    public class FakeDbResultSetReader : DbDataReader
    {
        static readonly int _STARTPOS = -1;

        // The DataReader should always be open when returned to the user.
        bool _fOpen = true;
        int _nPos = _STARTPOS;

        // Keep track of the results and position
        // within the resultset (starts prior to first record).
        public FakeDbResultSet Resultset;

        public override int Depth => 0;

        public override bool HasRows => HasRowsSetter;

        public bool HasRowsSetter { get; set; }

        public override bool IsClosed => !_fOpen;

        public override int       RecordsAffected => -1;
        public          DataTable FakeSchemaTable { get; set; } = new DataTable();

        //public override DataTable GetSchemaTable() => base.GetSchemaTable();

        public override int FieldCount => Resultset.metaData.Length;

        public override object this[int i] => Resultset.Data[_nPos, i];

        public override object this[string name] => this[GetOrdinal(name)];

        public override void Close() { _fOpen = false; }

        /// <returns><see cref="FakeSchemaTable" /> which defaults to an empty DataTable()</returns>
        public override DataTable GetSchemaTable() { return FakeSchemaTable; }

        public override bool NextResult() { return false; }

        public override bool Read()
        {
            // Return true if it is possible to advance and if you are still positioned
            // on a valid row. Because the data array in the resultset
            // is two-dimensional, you must divide by the number of columns.
            if (++_nPos >= Resultset.Data.Length / Resultset.metaData.Length)
                return false;
            else
                return true;
        }

        public override string GetName(int i) { return Resultset.metaData[i].Name; }

        public override string GetDataTypeName(int i)
        {
            /*
             * Usually this would return the name of the type
             * as used on the back end, for example 'smallint' or 'varchar'.
             * The sample returns the simple name of the .NET Framework type.
             */
            return Resultset.metaData[i].Type.Name;
        }

        public override IEnumerator GetEnumerator()
        {
            var rows    = Resultset.Data.Length / Resultset.metaData.Length;
            var results = new List<object[]>();
            for (var i = 0; i < rows; i++)
            {
                var row = new object[Resultset.metaData.Length];
                results.Add(row);
            }

            return results.GetEnumerator();
        }

        public override Type GetFieldType(int i)
        {
            // Return the actual Type class for the data type.
            return Resultset.metaData[i].Type;
        }

        public override object GetValue(int i) { return Resultset.Data[_nPos, i]; }

        public override int GetValues(object[] values)
        {
            int i                                                                          = 0, j = 0;
            for (; i < values.Length && j < Resultset.metaData.Length; i++, j++) values[i] = Resultset.Data[_nPos, j];

            return i;
        }

        public override int GetOrdinal(string name)
        {
            // Look for the ordinal of the column with the same name and return it.
            for (var i = 0; i < Resultset.metaData.Length; i++)
                if (0 == _cultureAwareCompare(name, Resultset.metaData[i].Name))
                    return i;

            // Throw an exception if the ordinal cannot be found.
            throw new IndexOutOfRangeException("Could not find specified column in results");
        }

        public override bool GetBoolean(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (bool) Resultset.Data[_nPos, i];
        }

        public override byte GetByte(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (byte) Resultset.Data[_nPos, i];
        }

        public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            // The sample does not support this method.
            throw new NotSupportedException("GetBytes not supported.");
        }

        public override char GetChar(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (char) Resultset.Data[_nPos, i];
        }

        public override long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            // The sample does not support this method.
            throw new NotSupportedException("GetChars not supported.");
        }

        public override Guid GetGuid(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (Guid) Resultset.Data[_nPos, i];
        }

        public override short GetInt16(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (short) Resultset.Data[_nPos, i];
        }

        public override int GetInt32(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (int) Resultset.Data[_nPos, i];
        }

        public override long GetInt64(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (long) Resultset.Data[_nPos, i];
        }

        public override float GetFloat(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (float) Resultset.Data[_nPos, i];
        }

        public override double GetDouble(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (double) Resultset.Data[_nPos, i];
        }

        public override string GetString(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (string) Resultset.Data[_nPos, i];
        }

        public override decimal GetDecimal(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
             */
            return (decimal) Resultset.Data[_nPos, i];
        }

        public override DateTime GetDateTime(int i)
        {
            /*
             * Force the cast to return the type. InvalidCastException
             * should be thrown if the data is not already of the correct type.
            */
            return (DateTime) Resultset.Data[_nPos, i];
        }

        public override bool IsDBNull(int i) { return Resultset.Data[_nPos, i] == DBNull.Value; }

        /*
         * Implementation specific methods.
         */
        int _cultureAwareCompare(string strA, string strB)
        {
            return CultureInfo.CurrentCulture.CompareInfo.Compare(strA,
                                                                  strB,
                                                                  CompareOptions.IgnoreKanaType
                                                                | CompareOptions.IgnoreWidth
                                                                | CompareOptions.IgnoreCase);
        }
    }
}
