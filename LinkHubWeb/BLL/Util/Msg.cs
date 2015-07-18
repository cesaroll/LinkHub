using System;
using System.Configuration;
using System.Net.Mime;
using System.Security.Cryptography;

namespace BLL.Util
{
    public enum MsgType
    {
        Validation,
        Information,
        Exception,
        DbException
    }
    public class Msg
    {
        public string Text { get; private set; }
        public MsgType Type { get; private set; }
        public Exception Ex { get; private set; }
        public DateTime CreateDate { get; private set; }

        public Msg()
        {
        }
        public Msg(string text) : this(text, MsgType.Information)
        {
        }

        public Msg(string text, MsgType type) : this(text, type, null)
        {
           
        }

        public Msg(string text, MsgType type, Exception ex)
        {
            Text = text;
            Type = type;
            Ex = ex;
            CreateDate = DateTime.Now;
        }



    }
}