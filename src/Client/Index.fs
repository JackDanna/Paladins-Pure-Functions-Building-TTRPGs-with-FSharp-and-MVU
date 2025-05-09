module Index

open Character
open Elmish

// Model
type Index = {
    Character: Character
    DataFromServer: string
}

let init () =
    {
        Character = Character.init ()
        DataFromServer = "No data from server yet"
    },
    Cmd.none


// Update
type Msg =
    | CharacterMsg of Character.Msg
    | FetchDataFromServer
    | GotDataFromServer of string

let update msg model =
    match msg with
    | CharacterMsg msg ->
        {
            model with
                Character = Character.update msg model.Character
        },
        Cmd.none
    | FetchDataFromServer ->
        model,
        Cmd.OfAsync.perform
            (fun () -> async {

                do! Async.Sleep 5000
                return "This is the stuff form the server"

            })
            ()
            GotDataFromServer

    | GotDataFromServer data -> { model with DataFromServer = data }, Cmd.none

// View

open Feliz
open Feliz.DaisyUI

let view (model: Index) (dispatch: Msg -> unit) =

    Html.div [
        Daisy.navbar [
            prop.className "mb-2 shadow-lg bg-neutral text-neutral-content rounded-box"
            prop.children [
                Daisy.navbarCenter [ Html.span [ prop.text model.DataFromServer; prop.className "text-2xl" ] ]
                Daisy.navbarEnd [
                    Daisy.button.button [ prop.text "Click me"; prop.onClick (fun e -> dispatch FetchDataFromServer) ]
                ]
            ]
        ]
        Character.view model.Character (CharacterMsg >> dispatch)
    ]