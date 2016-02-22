using System;

namespace Caesium {
    public class CalendarParseException : Exception {

        public CalendarParseException(TokenPosition tokenPosition, string message) : base(message) {
            TokenPosition = tokenPosition;
        }

        public TokenPosition TokenPosition { get; }

    }
}
