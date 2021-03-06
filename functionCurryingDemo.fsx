open System.Text.Json

type User = { Id: int; Name: string }
//let environment = "dev"
let environment = "production"

module WebServer =
    let get (userFromDb: int -> User) (formatter: User -> string) =
        let idFromQueryParameters = 1 // From HTTP Query parameters
        let user = userFromDb idFromQueryParameters

        user |> formatter

let jsonSerializer shouldMinify data =
    let serializerOptions = JsonSerializerOptions(WriteIndented = not shouldMinify)
    JsonSerializer.Serialize(data, serializerOptions)

// Partially apply jsonSerializer depending on environment
let formatter =
    match environment with
    | "production" -> jsonSerializer true
    | _ -> jsonSerializer false

let getUserById id =
    // Call database and query for user by id
    { Id = id; Name = "Kai Ito"}

printfn
    "Response from \"web server\" is: %s" (WebServer.get getUserById formatter)
