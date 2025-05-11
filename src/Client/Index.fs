module Index

open Character
open Elmish

open System
open Browser
open Elmish
open Elmish.Bridge
open Shared
open Shared.Bridge

// Model

type ConnectionStatus =
    | ConnectedToServer
    | NotConnectedToServer

type Index = {
    CharacterOption: Character option
    ConnectionStatus: ConnectionStatus
    EchoGuid: Guid
}

let init () =
    {
        CharacterOption = None
        ConnectionStatus = NotConnectedToServer
        EchoGuid = Guid.NewGuid()
    },
    Cmd.none

// Update
type Msg =
    | ConnectionLost
    | CharacterMsg of Character.Msg
    | RC of ServerToClientMsg // RC stands for Remote Client

let update msg model =
    match msg with
    | ConnectionLost -> init ()
    | CharacterMsg msg ->
        match model.CharacterOption with
        | Some character -> {
            model with
                CharacterOption = Character.update msg character |> Some
          }
        | None -> model
        , Cmd.none
    | RC(InitialConnection character) ->
        {
            model with
                CharacterOption = character |> Some
                ConnectionStatus = ConnectedToServer
        },
        Cmd.none
    | RC(BroadcastedCharacterMsg(characerMsg, echoGuid)) ->

        match echoGuid <> model.EchoGuid, model.CharacterOption with
        | true, Some character -> {
            model with
                CharacterOption = Character.update characerMsg character |> Some
                EchoGuid = Guid.NewGuid()
          }
        | _ -> model
        , Cmd.none



// View

open Feliz
open Feliz.DaisyUI

let view (model: Index) (dispatch: Msg -> unit) =

    Html.div [
        Daisy.navbar [
            prop.className "mb-2 shadow-lg bg-neutral text-neutral-content rounded-box"
            prop.children [
                Daisy.navbarStart []
                Daisy.navbarCenter [
                    Html.span [ prop.text "Paladins & Pure Functions"; prop.className "text-2xl" ]
                ]
                Daisy.navbarEnd [
                    match model.ConnectionStatus with
                    | NotConnectedToServer -> "Not Connected to Server"
                    | ConnectedToServer -> "Connected to Server"
                    |> Html.text
                ]
            ]
        ]
        match model.CharacterOption with
        | Some character -> Character.view character (CharacterMsg >> dispatch)
        | None -> Html.none
    ]