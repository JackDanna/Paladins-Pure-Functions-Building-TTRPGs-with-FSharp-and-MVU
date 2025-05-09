module Attribute

open Attribute

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