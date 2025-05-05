module Attribute

type Attribute = { Name: string; Value: int }

let init () = 
    { 
        Name = "Strength"
        Value = 0
    }

type Msg =
    | AddOneToValue
    | MinusOneToValue

let update 
    (msg: Msg)
    (model: Attribute) 
    =

    match msg with
    | AddOneToValue -> 
        { 
            model with 
                Value = model.Value + 1 
        }
    | MinusOneToValue ->
        {
            model with 
                Value = model.Value - 1
        }

open Feliz
open Feliz.DaisyUI

let view (model: Attribute) (dispatch: Msg -> unit) =
    Html.div [
        prop.className "flex items-center gap-4"
        prop.children [
            Daisy.label model.Name

            Html.text model.Value

            Daisy.button.button [
                prop.text "+"
                prop.onClick (fun e -> dispatch AddOneToValue)
            ]
            
            Daisy.button.button [
                prop.text "-"
                prop.onClick (fun (e: Browser.Types.MouseEvent) -> dispatch MinusOneToValue)
            ]
        ]
    ]