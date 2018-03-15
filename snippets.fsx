let thisIsAnInt = 1
let thisIsAString = "This is a string"
let optionalTypeAnnotation: bool = true

let listOfInts = [ 1; 2; 3 ]
let listOfStrings =
    [ "foo"
      "bar"
      "baz" ]



let add a b =
    a + b

let sign num =
    if num > 0 then "positive"
    elif num < 0 then "negative"
    else "zero"

let rec fib n =
    match n with
    | 1 -> 1
    | 2 -> 1
    | n -> fib(n-1) + fib(n-2)




let printer f = printfn "Number is: %f" f

[0.0..100.0]
|> List.filter (fun i -> i % 2.0 = 0.0)
|> List.map (fun i -> i ** 2.0)
|> List.iter printer



type User =
    { FirstName: string
      LastName: string
      Age: int }

let kai = { FirstName = "Kai"; LastName = "Ito"; Age = 27 }
let cloneOfKai = { FirstName = "Kai"; LastName = "Ito"; Age = 27 }

printfn "%b" (kai = cloneOfKai) // true

let olderKai = { kai with Age = kai.Age + 1 }

printfn "%i" <| olderKai.Age // 28
printfn "%i" <| kai.Age // 27




type Shape =
| Rectangle of width : float * length : float
| Circle of radius : float
| Triangle of float * float * float

let rectangle = Rectangle (2.0, 5.0)
let circle = Circle 2.5
let triangle = Triangle (6.1, 2.0, 3.7)

let whichShape shape =
    match shape with
    | Rectangle (width, length) ->
        printfn "Rectangle with sides %f %f" width length
    | Circle radius ->
        printfn "Circle with radius %f" radius
    | Triangle (side1, side2, side3) ->
        printfn "Triangle with sides %f %f %f" side1 side2 side3







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
