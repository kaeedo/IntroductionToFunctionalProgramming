let printerFn value = printfn "Printed value is: %A" value

let printerWorkflow =
    let foo = 1
    printerFn foo
    let bar = 2
    printerFn bar
    let baz = foo + bar
    printerFn baz

    baz

type PrintBuilder() =
    let printer value = printfn "Printed value is: %A" value

    member this.Bind (value, fn) =
        printer value
        fn value

    member this.Return value =
        value


let printer = new PrintBuilder()

let betterPrinterWorkflow =
    printer {
        let! foo = 10
        let! bar = 20
        let noLog = 123
        let! baz = foo + bar

        return baz
    }
