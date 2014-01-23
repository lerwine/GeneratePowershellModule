using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace GeneratePowershellModule
{
    public class PowerShellModuleWriter
    {
        private Collection<string> _lines = new Collection<string>();
        private string _currentLine = "";
        private int _baseIndentLevel = 0;

        public int BaseIndentLevel
        {
            get { return this._baseIndentLevel; }
            set { this._baseIndentLevel = (value < 0) ? 0 : value; }
        }

        public PowerShellModuleWriter(int baseIndentLevel)
        {
            this.BaseIndentLevel = baseIndentLevel;
        }

        public PowerShellModuleWriter() : this(0) { }

        public void Write(string text)
        {
            if (String.IsNullOrEmpty(text))
                return;

            char[] newLineChars = Environment.NewLine.ToCharArray();
            if (newLineChars.Length == 0)
            {
                this._lines.Add(text);
                return;
            }
            
            char[] chars = text.ToCharArray();
            while (chars.Any(c => newLineChars.Any(n => n == c)))
            {
                this._lines.Add(this._currentLine + new String(chars.TakeWhile(c => !newLineChars.Any(n => n == c)).ToArray()));
                chars = chars.SkipWhile(c => !newLineChars.Any(n => n == c)).ToArray();
                this._currentLine = "";

                int matchIndex = 0;
                for (int i = 0; i < chars.Length && i < newLineChars.Length && chars[i] == newLineChars[i]; i++)
                    matchIndex = i;
                chars = chars.Skip(matchIndex + 1).ToArray();
            }

            this._currentLine = new String(chars);
        }

        public void Write(PowerShellModuleWriter writer)
        {
            if (writer == null)
                return;

            this.Write(writer.ToString());
        }

        public void WriteLine(string text)
        {
            if (text == null)
                text = "";

            this.Write(text + Environment.NewLine);
        }

        public void WriteLine(PowerShellModuleWriter writer)
        {
            if (writer == null)
                this.Write(Environment.NewLine);
            else
                this.Write(writer.ToString() + Environment.NewLine);
        }
        
        public void WriteLine()
        {
            this.WriteLine("");
        }

        public void WriteFormat(bool appendNewLine, string format, params object[] args)
        {
            string s = String.Format(format, args);
            if (appendNewLine)
                this.Write(s + Environment.NewLine);
            else
                this.Write(s);
        }

        public void WriteFormat(string format, params object[] args)
        {
            this.WriteFormat(false, format, args);
        }

        public override string ToString()
        {
            string indent = (this.BaseIndentLevel == 0) ? "" : new String('\t', this.BaseIndentLevel);
            Collection<string> normalizedLines = new Collection<string>();

            bool previousLineWasBlank = true;
            foreach (string line in this._lines)
            {
                string s = (indent + line).TrimEnd();
                if (s.Length == 0)
                    previousLineWasBlank = true;
                else
                {
                    if (previousLineWasBlank)
                    {
                        if (normalizedLines.Count > 0)
                            normalizedLines.Add("");

                        previousLineWasBlank = false;
                    }

                    normalizedLines.Add(s);
                }
            }

            string lastLine = (indent + this._currentLine).TrimEnd();
            if (previousLineWasBlank && lastLine.Length > 0)
                normalizedLines.Add("");
            normalizedLines.Add(lastLine);

            return String.Join("\r\n", normalizedLines.ToArray());
        }

        public static string GetQuotedString(string textToSurroundInQuotes)
        {
            if (textToSurroundInQuotes == null)
                return "$null";

            char[] newLineChars = Environment.NewLine.ToCharArray();
            char[] textChars = textToSurroundInQuotes.ToCharArray();
            if (!textChars.Any(c => c == '\'' || c == '"'))
            {
                if (textChars.Any(c => newLineChars.Any(n => c == n)))
                    return String.Format("@'{0}{1}{0}'@", Environment.NewLine, textToSurroundInQuotes);
                return String.Format("'{0}'", textToSurroundInQuotes);
            }

            Collection<char[]> quotedParts = new Collection<char[]>();

            do
            {
                char[] quotableChars = textChars.TakeWhile(c => c != '\'').ToArray();
                if (quotableChars.Length == 0)
                    quotableChars = textChars.TakeWhile(c => c != '"' && c != '$').ToArray();

                quotedParts.Add(quotableChars);
                textChars = textChars.Skip(quotableChars.Length).ToArray();
            } while (textChars.Length > 0);

            string[] quotedItems = quotedParts.Select(charArray =>
            {
                string quoteChar = (charArray.Any(c => c == '\'')) ? "\"" : "'";
                if (charArray.Any(c => newLineChars.Any(n => c == n)))
                    return String.Format("@{0}{1}{2}{1}{0}@", quoteChar, Environment.NewLine, new String(charArray));

                return String.Format("{0}{1}{0}", quoteChar, new String(charArray));
            }).ToArray();

            if (quotedItems.Length == 1)
                return quotedItems[0];

            return String.Format("({0})", String.Join(" + ", quotedItems));
        }

        public bool IsEmpty { get { return (this._lines.All(l => String.IsNullOrWhiteSpace(l)) && String.IsNullOrWhiteSpace(this._currentLine)); } }
    }
}
