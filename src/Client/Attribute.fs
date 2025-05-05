module Attribute

// Model
type Attribute = { Name: string; Value: int }

let init name = { Name = name; Value = 0 }

// Update
type Msg =
    | ModifyName of string
    | AddOneToValue
    | MinusOneToValue

let update (msg: Msg) (model: Attribute) =

    match msg with
    | ModifyName newName -> { model with Name = newName }
    | AddOneToValue -> { model with Value = model.Value + 1 }
    | MinusOneToValue -> { model with Value = model.Value - 1 }

// View
open Feliz
open Feliz.DaisyUI

let view (model: Attribute) (dispatch: Msg -> unit) =
    Html.div [
        prop.className "flex items-center gap-4"
        prop.children [
            Daisy.input [
                prop.value model.Name
                prop.onTextChange (fun newString -> dispatch (ModifyName newString))
            ]

            Html.text model.Value

            Daisy.button.button [ prop.text "+"; prop.onClick (fun _ -> dispatch AddOneToValue) ]

            Daisy.button.button [ prop.text "-"; prop.onClick (fun _ -> dispatch MinusOneToValue) ]
        ]
    ]