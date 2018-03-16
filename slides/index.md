- title : Functional Programming with F#
- description : Introduction to functional programming using F#
- author : Kai Ito
- theme : night
- transition : default

***

<br /><br /><br /><br />

## Functional Programming with F#

<br /><br /><br />

* Kai Ito

***

### What is F#

* Functional first, multi-paradigm language
* Runs on .Net (and Mono, Xamarin, .Net Core)
* First released in 2005 by Microsoft Research
* Now belongs to The F# Software Foundation
* Open Source

---

### Syntax in a nutshell

* ML syntax
* Type inference
* Whitespace significant
* Expression-based
* Immutable by default

<br />

    [lang=fsharp]
    let thisIsAnInt = 1
    let thisIsAString = "This is a string"
    let optionalTypeAnnotation: bool = true

    let listOfInts = [ 1; 2; 3 ]
    let listOfStrings =
        [ "foo"
          "bar"
          "baz" ]

' let declarations are called values instead of variables

---

### Functions

* First class functions
* Last expression is the return "statement"

<br />

    [lang=fsharp]
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

---

### Piping

<br />

    [lang=fsharp]
    let printer f = printfn "Number is: %f" f

    [0.0..100.0]
    |> List.filter (fun i -> i % 2.0 = 0.0)
    |> List.map (fun i -> i ** 2.0)
    |> List.iter printer

---

### Records

* Simple aggregates of data
* Can be struct or reference types (since 4.1)
* Has structural equality

<br />

    [lang=fsharp]
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

---

### Discriminated Unions

* More powerful enum
* Data point that can have multiple different types

<br />

    [lang=fsharp]
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

***

### What is Functional Programming

* Function Composition over Inheritance
* Expressions over Statements
* Immutability
* Use of higher-order functions
* Use of pure functions

---

### Benefits of the Functional paradigm

* Higher order functions
  * High level abstractions
  * Code reusability
* One directional data flow
  * Easier to reason about
  * Easier to test
  * Easier to debug
* Immutability
  * Less bugs
  * No invalid state
  * Thread safety
* "If it compiles, it works"
* "Pit of Success"

---

<img src="images/MountainOfSuccess.jpg" />

' The ball is your project and its architecture
' The top of the mountain is a successful
' Takes a lot of hard work and discipline
' However, the moment you or someone else on the team makes a mistake, all that hard work is undone
' For example, making a database call directly from a Domain Model, instead of going through predefined services and other abstractions

***

### Benefits of the F# type system

* No null
* Make illegal states unrepresentable
* Use types to represent the domain
* Types can also be used to encode business logic

---

### The Option type

    [lang=fsharp]
    type Option<'a> =
    | Some of 'a
    | None

    let validInt = Some 1
    let invalidInt = None

    let numbers = [ 1; 2; 3; 4; ]
    let foundNumber = numbers |> List.tryFind (fun x -> x = 4)
    let missingNumber = numbers |> List.tryFind (fun x -> x = 50)

    printfn "The number is: %i" foundNumber
    // Compile Error: Type mismatch: Expecting "int" but got "int option"

    let printInt input =
        match input with
        | Some i -> printfn "The number is: %i" i
        | None -> printfn "Didn't find number"

    printInt foundNumber // The number is: 4
    printInt missingNumber // Didn't find number

' This is how a lack of value is represented
' Many standard library and 3rd party libraries use the Option type as a return value
' Why is this useful?
' The compiler forces you to handle both cases
' Forces you to think about what happens when you get an unexpected result

---

### Making illegal state unrepresentable
* Image business log where a User either needs an email address or phone number or both
* Required to have at least one of them


    [lang=fsharp]
    type ContactUser = { Username: string; Email: string option; PhoneNumber: string option }

    let createUser username emailAddress phoneNumber =
        // Logic to ensure either email address or phone is supplied
        // Error prone

        { Username = "kaiito"; Email = "kai.ito@zuehlke.com"; PhoneNumber = "089 555 1234" }

---

### F# Type System to the rescue
    [lang=fsharp]
    type ContactInformation =
    | Email of string
    | PhoneNumber of string
    | EmailAndPhone of string * string

    type SafeContactUser = { Username: string; Contact: ContactInformation }

    let email = Email "kai.ito@zuehlke.com"
    let phoneNumber = PhoneNumber "089 555 1234"
    let emailAndPhone = EmailAndPhone ("kai.ito@zuehlke.com", "089 555 1234")

    let user1 = { Username = "kaiito"; Contact = email }
    let user2 = { Username = "kaiito"; Contact = phoneNumber }
    let user3 = { Username = "kaiito"; Contact = emailAndPhone }

    let user4 = { Username = "kaiito"; Contact = null } // Compiler Error
    let user5 = { Username = "kaiito"; Contact = "someString" } // Compiler Error

' When deconstructing the Contact information, automatically know what kind of contact information it is

' Assumes happy path
' Validation has been done elsewhere
' Goes along with pit of success

***

### Function Composition
* Compose multiple functions into one function
* Code reusability without verbosity

---

<img src="images/Composition1.jpg" />

---

<img src="images/Composition2.jpg" />

---

    [lang=fsharp]
    let parseDateTime (dateTime: string) = System.DateTime.Parse(dateTime)
    let getYear (date: System.DateTime) = date.Year

    let date = parseDateTime "13-03-2018 12:00am"
    let currentYear: int = getYear date

    printfn "The current year is: %i" currentYear
    // The current year is: 2018

    let composedGetYear: string -> int = parseDateTime >> getYear
    let yearFromComposed: int = composedGetYear "13-03-2018 12:00am"

    printfn "The current year from composed function is: %i" yearFromComposed
    // The current year from composed function is: 2018

***

### Function Currying

***

### Railway Oriented Programming
* Error handling through function composition
* Clean control flow
* Treat errors as first class citizens

' Not just exceptions. Any errors, for example validation messages as well

---

<img src="images/RoP1.jpg" />

' Let's take a look at our Red Circle to Yellow Star function from earlier
' What happens when some kind of error occurs?

---

<img src="images/RoP2.jpg" />

' We can model our function like a railway switch
' We have our input, and we have a happy path, and an error path
' The happy path can cause an error, but the error path can't go back to the happy path

---

<img src="images/RoP3.jpg" />

' And just like real rail tracks, we can easily combine them
' But since we're combining functions, we can easily Compose them like we saw earlier
' However, now you might ask, what happens to the end of the error track. Where does that go?

---

<img src="images/RoP4.jpg" />

' We can expand the input of our functions to take in an error as well
' As I mentioned a moment ago, once we are on an error path, we won't be going back to the happy path
' Reason being, we will no longer have valid input for the next part in our workflow, which would cause additional errors to occur

---

<img src="images/RoP5.jpg" />
' We can now expand this model to compose as many functions as we want, creating an entire workflow

---

### Demo

' Railway oriented programming can do much more, for example running multiple tracks in parallel
' or using single track functions, or having dead end tracks.
' The standard library already includes several helper methods, but there also exists several open source libraries for working with RoP, one such library is called Chessie

***

### Monads
* A monad is just a monoid in the category of endofunctors
