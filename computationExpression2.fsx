open System

let getQueryParam (param: string) =
    if String.IsNullOrWhiteSpace(param) then
        None
    else
        Some param

let validateQueryParam (param: string) =
    let parts = param.Split('=')
    if parts.Length = 2 then
        Some parts.[1]
    else
        None

let queryParam = "userId=123"

let result =
    let queryParamFromUrl = getQueryParam queryParam

    match queryParamFromUrl with
    | None -> None
    | Some queryParam ->
        let validQueryParamValue = validateQueryParam queryParam
        match validQueryParamValue with
        | None -> None
        | Some value ->
            let validValue = getQueryParam value
            match validValue with
            | None -> None
            | Some valid -> Some valid

match result with
| None -> printfn "Query param is malformed"
| Some r -> printfn "Query parameter value is: %s" r






type MaybeBuilder() =
    member this.Bind (value, fn) =
        match value with
        | None -> None
        | Some r -> fn r

    member this.Return value =
        Some value

let maybe = MaybeBuilder()

let maybeResult urlParameter =
    let maybeValue =
        maybe {
            let! queryParam = getQueryParam urlParameter
            let! value = validateQueryParam queryParam
            let! validValue = getQueryParam value

            return validValue
        }

    match maybeValue with
    | None -> printfn "Maybe: Query param is malformed"
    | Some r -> printfn "Maybe: Query parameter value is: %s" r

maybeResult "userId=123"
maybeResult "userId="
maybeResult "userI-123"
maybeResult ""
