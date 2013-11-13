// -----------------------------------------------------------------------
// <copyright file="CommandResultBase.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Thai.Tran" date="2013.11.05">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.SearchCopy.Command
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Class CommandResultBase.
    /// </summary>
    public class CommandResultBase : ICommandResult
    {
        private dynamic _code;
        private dynamic _message;

        public CommandResultBase()
            : this(string.Empty, string.Empty)
        { 
        }

        public CommandResultBase(dynamic code, dynamic message)
        {
            this._code = code;
            this._message = message;
        }

        public dynamic Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public dynamic Message
        {
            get { return _message; }
            set { _message = value; }
        }        

        public virtual bool IsSuccessful
        {
            get { throw new NotImplementedException(); }
        }
    }
}
