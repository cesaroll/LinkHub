using System.Collections;
using System.Collections.Generic;

namespace BLL.Util
{
    public class Response : IResponse
    {
        public bool IsError { get; set; }

        private List<Msg> _msgs; 
        private List<Msg> Msgs
        {
            get { return _msgs ?? (_msgs = new List<Msg>()); }
        }

        public Response(bool isError)
        {
            IsError = isError;
        }

        public Response(bool isError, Msg msg)
        {
            IsError = isError;
            Add(msg);
        }

        public Response(bool isError, List<Msg> msgs)
        {
            IsError = isError;
            _msgs = msgs;
        }

        public bool HasMsgs
        {
            get
            {
                return !(_msgs == null || _msgs.Count == 0);
            }
        }

        public void Add(Msg msg)
        {
            if (msg != null)
                Msgs.Add(msg);
        }

        public void Clean()
        {
            _msgs = null;
            IsError = false;
        }

        public void Include(IResponse resp)
        {
            if (resp == null)
                return;

            foreach (var msg in resp)
            {
                Add(msg);
            }
        }

        public IEnumerator<Msg> GetEnumerator()
        {
            return Msgs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}