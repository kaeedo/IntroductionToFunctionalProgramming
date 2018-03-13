let parseDateTime (dateTime: string) = System.DateTime.Parse(dateTime)
let getYear (date: System.DateTime) = date.Year

let currentYear: int =
    let date = parseDateTime "13-03-2018 12:00am"
    getYear date

printfn "The current year is: %i" currentYear
// The current year is: 2018

let composedGetYear: string -> int = parseDateTime >> getYear
let yearFromComposed: int = composedGetYear "13-03-2018 12:00am"

printfn "The current year from composed function is: %i" yearFromComposed
