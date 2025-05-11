module Server

open Saturn
open Giraffe

open Shared
open Shared.Bridge
open Elmish
open Elmish.Bridge


let mutable characterDB = Character.init ()

// Bridge Stuff

type ServerMsg =
    | ClosedConnection
    | RS of ClientToServerMsg // RS stands for Remote Server
    | ResultOfSideEffect of Result<unit, string>

let hub = ServerHub<User, ServerMsg, ServerToClientMsg>()

let init clientDispatch () =
    characterDB |> ServerToClientMsg.InitialConnection |> clientDispatch
    Guest, Cmd.none

let update clientDispatch msg (model: User) =
    match msg with
    | ClosedConnection -> model, Cmd.none
    | RS(UpdateCharacter(msg, echoGuid)) ->

        hub.SendClientIf (fun user -> true) (BroadcastedCharacterMsg(msg, echoGuid))

        model,
        Cmd.OfAsyncImmediate.perform
            (fun () -> async {

                characterDB <- Character.update msg characterDB

                return Ok()

            })
            ()
            ResultOfSideEffect

    | ResultOfSideEffect result -> model, Cmd.none


let server =
    Bridge.mkServer Shared.Bridge.endpoint init update
    |> Bridge.withConsoleTrace
    |> Bridge.withServerHub hub
    |> Bridge.whenDown ClosedConnection
    |> Bridge.run Giraffe.server

let webApp =
    choose [
        server
    //route "/" >=> htmlFile "/index.html"
    ]

// open Microsoft.AspNetCore.Builder
// open Microsoft.AspNetCore.StaticFiles
// open Microsoft.Extensions.FileProviders
// open System.IO
// open Microsoft.AspNetCore.Http

let app = application {
    url "http://0.0.0.0:5000"
    use_router webApp
    app_config Giraffe.useWebSockets
    memory_cache

    // app_config (fun (app: IApplicationBuilder) ->
    //     app.UseStaticFiles(
    //         StaticFileOptions(
    //             FileProvider =
    //                 new PhysicalFileProvider(
    //                     Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../../FogentRoleplay"))
    //                 ),
    //             RequestPath = new PathString "/FogentRoleplay"
    //         )
    //     )
    //     |> ignore

    //     app) // Return the original app builder

    use_gzip
}

[<EntryPoint>]
let main _ =
    run app
    0