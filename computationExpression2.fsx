let (/?) top bottom =
    if bottom = 0.0 then
        None
    else
        Some (top / bottom)

let zero = 0.0
let one = 100.0
let two = 200.0
let three = 300.0
let four = 400.0


let result =
    match one /? two with
    | None -> None
    | Some r ->
        let foo = r /? three
        match foo with
        | None -> None
        | Some s ->
            let bar = s /? zero
            match bar with
            | None -> None
            | Some t ->
                let baz = t /? four
                match baz with
                | None -> None
                | Some u -> Some u

match result with
| None -> printfn "One of the divisors was 0"
| Some r -> printfn "Result is: %f" r

type MaybeBuilder() =
    member this.Bind(value, fn) =
        match value with
        | None -> None
        | Some r -> fn r

    member this.Return(value) =
        Some value

let maybe = new MaybeBuilder()

let maybeResult =
    maybe {
        let! foo = one /? two
        let! bar = foo /? three
        let! baz = bar /? zero
        let! qux = baz /? four

        return qux
    }

match maybeResult with
| None -> printfn "Maybe: One of the divisors was 0"
| Some r -> printfn "Maybe: Result is: %f" r
