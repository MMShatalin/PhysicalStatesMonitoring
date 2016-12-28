﻿using System;

namespace Itp
{
    public class DataReaderErrorEventArgs : EventArgs
    {
        public int ErrorCode { get; set; }
        public string ErrorText { get; set; }

        public DataReaderErrorEventArgs(int errorCode, string errorText)
        {
            ErrorCode = errorCode;
            ErrorText = errorText;
        }
    }
}