let printerFn value = printfn "Printed value is: %A" value

let printerWorkflow =
    let decade = 10
    printerFn decade
    let age = 28
    printerFn age
    let ageInDecade = decade + age
    printerFn ageInDecade

    ageInDecade

type PrintBuilder() =
    let printer value = printfn "Printed value in computation expression is: %A" value

    member this.Bind (value, fn) =
        printer value
        fn value

    member this.Return value =
        value


let printer = PrintBuilder()

let betterPrinterWorkflow =
    printer {
        let! decade = 10
        let! age = 28
        let irrelevantNumber = 123
        let! ageInDecade = decade + age

        return ageInDecade
    }
