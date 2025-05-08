module AttributeWithSkills

open Skill

// Model
type AttributeWithSkills = {
    AttributeWithSkillsName: string
    Value: int
    SkillList: Skill List
}

let init (attributeWithSkillsName, skillListNames) = {
    AttributeWithSkillsName = attributeWithSkillsName
    Value = 0
    SkillList = List.map Skill.init skillListNames
}

// Update
type Msg =
    | ModifyName of string
    | AddOneToValue
    | MinusOneToValue
    | SkillMsgAtPosition of skillMsg: Skill.Msg * position: int

let injectModelIntoMsg model msg =
    match msg with
    | SkillMsgAtPosition(Skill.AddOneToValue _, positions) ->
        SkillMsgAtPosition(Skill.AddOneToValue(Some model.Value), positions)
    | SkillMsgAtPosition(Skill.MinusOneToValue _, positions) ->
        SkillMsgAtPosition(Skill.MinusOneToValue(Some model.Value), positions)
    | _ -> msg

let update (msg: Msg) (model: AttributeWithSkills) =
    match injectModelIntoMsg model msg with
    | ModifyName newName -> {
        model with
            AttributeWithSkillsName = newName
      }
    | AddOneToValue -> { model with Value = model.Value + 1 }
    | MinusOneToValue -> { model with Value = model.Value - 1 }
    | SkillMsgAtPosition(msg: Skill.Msg, position: int) -> {
        model with
            SkillList =
                model.SkillList
                |> List.mapi (fun index skill -> if index = position then Skill.update msg skill else skill)
      }

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
                    Html.text model.Value
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