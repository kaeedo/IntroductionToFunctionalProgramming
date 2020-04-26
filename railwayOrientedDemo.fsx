open System

type User =
    { Id: int
      Name: string }

let updateNameInDb id newName =
    if id < 0 then
        None
    else
        Some
            { Id = id
              Name = newName }

let trySendResponse id name =
    try
        Some(sprintf """{"id":%i, "name":"%s"}""" id name)
    with _ -> None

type Result<'TSuccess, 'TFailure> =
    | Success of 'TSuccess
    | Failure of 'TFailure

let validate (id, name) =
    if name = String.Empty then Failure "Input must not be empty" else Success(id, name)

let update input =
    let id, name = input
    let updatedUser = updateNameInDb id name
    match updatedUser with
    | Some u -> Success u
    | None -> Failure "User with given ID not found"

let send (input: User) =
    let id = input.Id
    let name = input.Name

    match trySendResponse id name with
    | Some s -> Success(sprintf "Responding with JSON: %s" s)
    | None -> Failure "Couldn't serialize to JSON for some reason"

let bind switchFunction twoTrackInput =
    match twoTrackInput with
    | Success s -> switchFunction s
    | Failure f -> Failure f

let workflow =
    validate
    >> bind update
    >> bind send

let workflowResult id name =
    match workflow (id, name) with
    | Success s -> printfn "Succeeded with message: %s" s
    | Failure f -> printfn "Failed with message: %s" f

workflowResult 1 ""
workflowResult -2 "kai"
workflowResult 3 "alice"
workflowResult 4 "bob"
