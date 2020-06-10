open System
open FSharp.Data

type Reddit = JsonProvider<"https://www.reddit.com/r/fsharp/.json">

[<EntryPoint>]
let main argv =
    let subReddit = "fsharp"
    let posts =
        Reddit.Load(sprintf "https://www.reddit.com/r/%s/.json" subReddit).Data.Children
        |> Seq.map (fun p -> p.Data)

    posts
    |> Seq.groupBy (fun p -> p.Author.String)
    |> Seq.filter (fun (author, _) -> author.IsSome)
    |> Seq.map (fun (author, posts) -> author.Value, (posts |> Seq.length))
    |> Seq.filter (fun (_, posts) -> posts > 1)
    |> Seq.iter (printfn "%A")

    0
