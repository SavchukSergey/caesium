# About

Caesium is a tool for parsing ICS files

# Usage

Parse calendar from existing string:

`
var calendar = (VCalendar)CalendarObject.Parse(content)
`

Load calendar from uri:

`
var calendar = VCalendar.Load(uri)
`
