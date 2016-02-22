using System;
using System.Text;

namespace Caesium {
    public class CalendarReader {

        private readonly string _content;
        private TokenPosition _position;

        public CalendarReader(string content) {
            _content = content;
        }

        public char ReadChar(char expected) {
            var ch = ReadChar();
            if (ch != expected) {
                throw new CalendarParseException(_position, expected + " expected");
            }
            return ch;
        }

        public char ReadChar() {
            while (!IsEof) {
                var ch = _content[_position.Index];
                _position.Index++;
                if (ch == '\r' && (_position.Index + 1) < _content.Length) {
                    if (_content[_position.Index] != '\n' || (_content[_position.Index + 1] != 0x20 && _content[_position.Index + 1] != 0x09)) {
                        return ch;
                    }
                    _position.Index += 2;
                } else {
                    return ch;
                }
            }
            return (char)(0xffff);
        }

        public char PeekChar() {
            var backup = _position;
            var ch = ReadChar();
            _position = backup;
            return ch;
        }

        public char PeekChar(int distance) {
            var backup = _position;
            var ch = (char)0xffff;
            while (distance >= 0) {
                ch = ReadChar();
                distance--;
            }
            _position = backup;
            return ch;
        }

        public bool PeekOrRead(char skipChar) {
            var backup = _position;
            var ch = ReadChar();
            if (ch == skipChar) return true;
            _position = backup;
            return false;
        }

        public string ReadName() {
            var sb = new StringBuilder();
            while (!IsEof) {
                var ch = PeekChar();
                if (!char.IsLetterOrDigit(ch) && ch != '-') {
                    //if (sb.Length == 0) throw new Exception("Unexpected end of NAME");
                    return sb.ToString();
                }
                sb.Append(ReadChar());
            }
            return sb.ToString();
        }

        public string ReadQuotedString() {
            var sb = new StringBuilder();
            ReadChar('"');
            while (!IsEof) {
                var ch = ReadChar();
                if (ch == '"') return sb.ToString();
                if (!(ch == '\t' || ch >= 0x20)) throw new CalendarParseException(_position, "Unexpected character in quoted string");
                sb.Append(ch);
            }
            throw new CalendarParseException(_position, "Unexpected end on quoted string");
        }

        public TokenPosition GetPosition() {
            return _position;
        }

        public void SetPosition(TokenPosition pos) {
            _position = pos;
        }

        public bool IsSignedInteger {
            get {
                var firstChar = PeekChar();
                switch (firstChar) {
                    case '+':
                    case '-':
                        var secondChar = PeekChar(1);
                        return secondChar >= '0' && secondChar <= '9';
                    default:
                        return firstChar >= '0' && firstChar <= '9';
                }
            }
        }

        public int ReadSignedInteger() {
            var negative = false;
            var value = 0;

            var firstChar = ReadChar();
            switch (firstChar) {
                case '+':
                    break;
                case '-':
                    negative = true;
                    break;
                default:
                    if (firstChar >= '0' && firstChar <= '9') {
                        value = firstChar - '0';
                    } else {
                        throw new Exception("Integer value expected");
                    }
                    break;
            }

            while (!IsEof) {
                var ch = ReadChar();
                if (ch >= '0' && ch <= '9') {
                    value = value * 10 + (ch - '0');
                } else {
                    break;
                }
            }

            return negative ? -value : value;
        }

        public bool IsEof => _position.Index >= _content.Length;

    }
}
