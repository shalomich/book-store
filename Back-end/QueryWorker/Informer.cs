using System;
using System.Collections.Generic;
using System.Text;

namespace QueryWorker
{
    public class Informer
    {
        private List<KeyValuePair<string, string>> _messages = new List<KeyValuePair<string, string>>();
        private int _errorCount = 0;
        public IReadOnlyCollection<KeyValuePair<string, string>> Messages => _messages;

        public void Subscribe(IInformed informed)
        {
            informed.Accepted += AddMessage;
            informed.Crashed += AddErrorMessage;
        }

        public void Unsubscribe(IInformed informed)
        {
            informed.Accepted -= AddMessage;
            informed.Crashed -= AddErrorMessage;
        }
        private void AddMessage(string theme,string text,IInformed informed)
        {
            _messages.Add(KeyValuePair.Create(theme, text));
        }

        private void AddErrorMessage(string theme,string text, IInformed informed)
        {
            _errorCount ++;
            AddMessage($"error{_errorCount}", $"{theme}: {text}, {informed.ToString()}",informed);
        }
    }
}
