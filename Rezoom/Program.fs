open Rezoom.SQL
open Rezoom.SQL.Synchronous
open Rezoom.SQL.Migrations

type MyModel = SQLModel<"."> 

let migrate() =
    let config =
        { MigrationConfig.Default with
            LogMigrationRan = fun m -> printfn "Ran migration: %s" m.MigrationName
        }
    MyModel.Migrate(config)

type InsertUser = SQL<"""
    insert into Users(Username, Email) values (@username, @email);
    """>

type SelectUsers = SQL<"""
    select * from Users
    """>

[<EntryPoint>]
let main argv =
    migrate()
    use context = new ConnectionContext()

    InsertUser.Command(email="my@email.com", username="myCoolUsername").Execute(context)

    let users =
        SelectUsers.Command().Execute(context)

    users
    |> Seq.iter (fun u ->
        printfn "Username: %s" u.Username
        printfn "Email: %s" u.Email
    )

    System.Console.ReadLine() |> ignore

    0