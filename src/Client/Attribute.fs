module Attribute

// Model
type Model = { Name: string; Value: int }

let init () = { Name = "Strength"; Value = 0 }

// Update
type Msg =
    | AddOneToValue
    | MinusOneToValue

let update (msg: Msg) (model: Model) =

    match msg with
    | AddOneToValue -> { model with Value = model.Value + 1 }
    | MinusOneToValue -> { model with Value = model.Value - 1 }

// View
open Feliz
open Feliz.DaisyUI

let view (model: Model) (dispatch: Msg -> unit) =
    Daisy.card [
        card.border
        prop.className "m-10 inline-flex bg-base-200"
        prop.children [
            Daisy.cardBody [
                prop.className "flex flex-row items-center gap-4"

                prop.children [

                    Daisy.badge [ color.bgPrimary; badge.lg; prop.text model.Name ]

                    Html.text model.Value

                    Daisy.button.button [ prop.text "+"; prop.onClick (fun _ -> dispatch AddOneToValue) ]

                    Daisy.button.button [ prop.text "-"; prop.onClick (fun _ -> dispatch MinusOneToValue) ]

                ]

            ]
        ]
    ]