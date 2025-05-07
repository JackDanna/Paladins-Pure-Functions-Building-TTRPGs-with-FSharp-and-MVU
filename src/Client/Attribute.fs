module Attribute

// Model
type Attribute = { AttributeName: string; Value: int }

let init name = { AttributeName = name; Value = 0 }

// Update
type Msg =
    | ModifyName of string
    | AddOneToValue
    | MinusOneToValue

let update (msg: Msg) (model: Attribute) =

    match msg with
    | ModifyName newName -> { model with AttributeName = newName }
    | AddOneToValue -> { model with Value = model.Value + 1 }
    | MinusOneToValue -> { model with Value = model.Value - 1 }

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
                        prop.onTextChange (fun newName -> dispatch (ModifyName newName))
                    ]
                    Html.text model.Value
                    Daisy.button.button [ prop.text "+"; prop.onClick (fun _ -> dispatch AddOneToValue) ]
                    Daisy.button.button [ prop.text "-"; prop.onClick (fun _ -> dispatch MinusOneToValue) ]
                ]
            ]
        ]
    ]