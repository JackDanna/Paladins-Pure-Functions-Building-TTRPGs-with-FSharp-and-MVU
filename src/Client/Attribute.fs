module Attribute

// Model
type Attribute = { AttributeName: string; Level: int }

let init name = { AttributeName = name; Level = 0 }

// Update
type Msg =
    | ModifyName of string
    | AddOneToLevel
    | MinusOneToLevel

let update (msg: Msg) (model: Attribute) =

    match msg with
    | ModifyName newName -> { model with AttributeName = newName }
    | AddOneToLevel -> { model with Level = model.Level + 1 }
    | MinusOneToLevel -> { model with Level = model.Level - 1 }

// View
open Feliz
open Feliz.DaisyUI

let view (model: Attribute) (dispatch: Msg -> unit) =
    Daisy.card [
        card.border
        prop.className "m-2 inline-flex bg-base-200"
        prop.children [
            Daisy.cardBody [
                prop.className "flex flex-row items-center gap-4"
                prop.children [
                    Daisy.input [
                        color.bgPrimary
                        prop.value model.AttributeName
                        prop.onTextChange (fun userInput -> dispatch (ModifyName userInput))
                    ]
                    Html.text model.Level
                    Daisy.button.button [ prop.text "+"; prop.onClick (fun _ -> dispatch AddOneToLevel) ]
                    Daisy.button.button [ prop.text "-"; prop.onClick (fun _ -> dispatch MinusOneToLevel) ]
                ]
            ]
        ]
    ]