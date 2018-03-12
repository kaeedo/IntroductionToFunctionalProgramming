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

    let otherSign num =
        match num with
        | n when n > 0 -> "positive"
        | n when n < 0 -> "negative"
        | _ -> "zero"

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

    printfn "%O" <| olderKai.Age // 28
    printfn "%O" <| kai.Age // 27
