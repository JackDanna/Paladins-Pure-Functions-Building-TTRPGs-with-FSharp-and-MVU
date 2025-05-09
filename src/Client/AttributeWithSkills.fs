module AttributeWithSkills

open AttributeWithSkills

// View
open Feliz
open Feliz.DaisyUI

let view (model: AttributeWithSkills) (dispatch: Msg -> unit) =
    Daisy.card [
        card.border
        color.bgBase200
        prop.className "flex items-center m-4"
        Daisy.table [
            Html.thead [
                [
                    Daisy.input [
                        color.bgPrimary
                        prop.className "text-center text-white"
                        prop.value model.AttributeWithSkillsName
                        prop.onTextChange (fun newName -> dispatch (ModifyName newName))
                    ]
                    Html.text model.Level
                    Daisy.button.button [ prop.text "+"; prop.onClick (fun _ -> dispatch AddOneToValue) ]
                    Daisy.button.button [ prop.text "-"; prop.onClick (fun _ -> dispatch MinusOneToValue) ]
                ]
                |> List.map Html.th
                |> Html.tr
            ]

            model.SkillList
            |> List.mapi (fun index skill -> Skill.view skill (fun msg -> SkillMsgAtPosition(msg, index) |> dispatch))
            |> Html.tbody
        ]
        |> prop.children
    ]