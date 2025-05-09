module Skill

open Skill

// View
open Feliz
open Feliz.DaisyUI

let view (model: Skill) (dispatch: Msg -> unit) =
    [
        Daisy.input [
            color.bgPrimary
            prop.className "text-center text-xs"
            prop.value model.SkillName
            prop.onTextChange (fun newName -> dispatch (ModifyName newName))
        ]
        Html.text model.Value
        Daisy.button.button [ prop.text "+"; prop.onClick (fun _ -> dispatch (AddOneToValue None)) ]
        Daisy.button.button [ prop.text "-"; prop.onClick (fun _ -> dispatch (MinusOneToValue None)) ]
    ]
    |> List.map Html.td
    |> Html.tr